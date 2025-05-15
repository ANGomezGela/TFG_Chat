using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

class MyTcpListener
{
    private TcpListener server;
    private List<TcpClient> clients = new List<TcpClient>(); // Bezeroen zerrenda
    private List<string> clientNames = new List<string>(); // Bezeroen izenen zerrenda
    private bool martxan = true; // Zerbitzaria martxan dagoen ala ez adierazten du
    private const int MaxClients = 15; // Gehienezko bezero kopurua
    private static readonly HttpClient httpClient = new HttpClient(); // HTTP bezeroa API deietarako

    public MyTcpListener(IPAddress ip, int port)
    {
        server = new TcpListener(ip, port); // TCP entzulea sortu IP eta port batekin
    }

    public async Task EntzunAsync()
    {
        try
        {
            server.Start(); // Zerbitzaria abiarazi
            Console.WriteLine("Bezeroen konexioa itxaroten..."); // Kontsolan mezua erakutsi

            while (martxan) // Zerbitzaria martxan dagoen bitartean
            {
                TcpClient client = await server.AcceptTcpClientAsync(); // Bezero berri baten konexioa onartu
                _ = Task.Run(() => KomunikazioaAsync(client)); // Bezeroarekin komunikazioa hasi
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Errorea zerbitzarian: {0}", e.Message); // Errorea erakutsi
        }
    }

    private async Task KomunikazioaAsync(TcpClient client)
    {
        using (NetworkStream str = client.GetStream())
        using (StreamReader sr = new StreamReader(str))
        using (StreamWriter sw = new StreamWriter(str) { AutoFlush = true })
        {
            try
            {
                string clientName = await sr.ReadLineAsync(); // Bezeroaren izena jaso

                if (clientNames.Contains(clientName)) // Izena dagoeneko erabilita badago
                {
                    await sw.WriteLineAsync("Error: El nombre ya está en uso.");
                    client.Close(); // Konexioa itxi
                    return;
                }

                if (clients.Count >= MaxClients) // Bezero kopurua maximoa bada
                {
                    await sw.WriteLineAsync("Error: Máximo de conexiones alcanzado. Inténtelo más tarde.");
                    client.Close();
                    return;
                }

                lock (clients)
                {
                    clients.Add(client); // Bezeroa gehitu
                    clientNames.Add(clientName); // Izena gehitu
                }

                Console.WriteLine($"Cliente {clientName} se ha unido. Conexiones actuales: {clients.Count}");
                await sw.WriteLineAsync($"Conectado-{clientName}"); // Bezeroari konexioa baieztatu
                await EnviarListaDeUsuariosAsync(); // Beste bezeroei zerrenda eguneratua bidali

                string data;
                while ((data = await sr.ReadLineAsync()) != null) // Bezeroaren mezuak jaso
                {
                    if (data.StartsWith("DISC:")) // Deskonektatze mezua bada
                    {
                        string disconnectedUser = data.Split(':')[1];
                        Console.WriteLine($"Cliente {disconnectedUser} se desconectó.");
                        break;
                    }
                    else if (data.StartsWith("MSG:")) // Mezua denean
                    {
                        Console.WriteLine(data);
                        await EnviarMensajeATodosAsync(data); // Mezua guztiei bidali
                    }
                    else if (data == "API:") // API mezua eskatzen bada
                    {
                        Console.WriteLine($"Cliente {clientName} solicitó citas.");
                        string citasJson = await ObtenerCitasDeLaAPIAsync(); // API-tik datuak jaso
                        await sw.WriteLineAsync("API:" + citasJson); // Bezeroari bidali
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Errorea bezeroarekin: {0}", e.Message); // Errorea komunikazioan
            }
            finally
            {
                lock (clients)
                {
                    int index = clients.IndexOf(client); // Bezeroaren posizioa aurkitu
                    if (index != -1)
                    {
                        clients.RemoveAt(index); // Zerrendatik kendu
                        clientNames.RemoveAt(index);
                    }
                }
                await EnviarListaDeUsuariosAsync(); // Bezero zerrenda eguneratu
                client.Close(); // Konexioa itxi
                Console.WriteLine($"Conexiones actuales: {clients.Count}");
            }
        }
    }

    private async Task<string> ObtenerCitasDeLaAPIAsync()
    {
        try
        {
            HttpResponseMessage response = await httpClient.GetAsync("http://localhost:8080/api/hitzorduak/hoy");

            if (!response.IsSuccessStatusCode) // Erantzuna okerra bada
                return "Error: No se pudo obtener citas de la API.";

            string json = await response.Content.ReadAsStringAsync();

            if (string.IsNullOrWhiteSpace(json)) // JSON hutsik badago
                return "Gaur ez daude zitak"; // Euskarazko mezua

            var citas = JsonConvert.DeserializeObject<List<dynamic>>(json);

            if (citas == null || citas.Count == 0)
                return "Gaur ez daude zitak"; // Euskarazko mezua

            var citasFiltradas = new List<object>();
            foreach (var cita in citas)
            {
                citasFiltradas.Add(new
                {
                    izena = (string)cita.izena, // Citaren izena
                    hasieraOrdua = (string)cita.hasieraOrdua, // Hasiera ordua
                    amaieraOrdua = (string)cita.amaieraOrdua // Amaiera ordua
                });
            }

            return JsonConvert.SerializeObject(citasFiltradas); // JSON bihurtu eta itzuli
        }
        catch
        {
            return "Error: No se pudo conectar a la API";
        }
    }

    public async Task EnviarListaDeUsuariosAsync()
    {
        string usuarios = "USUARIOS_ACTIVOS:" + string.Join(",", clientNames); // Aktibo dauden erabiltzaileak
        List<Task> tareas = new List<Task>();

        foreach (var cliente in clients)
        {
            tareas.Add(Task.Run(async () =>
            {
                try
                {
                    NetworkStream stream = cliente.GetStream();
                    StreamWriter writer = new StreamWriter(stream) { AutoFlush = true };
                    await writer.WriteLineAsync(usuarios); // Mezuak bidali
                }
                catch { }
            }));
        }

        await Task.WhenAll(tareas); // Guztiak amaitu arte itxaron
    }

    public async Task EnviarMensajeATodosAsync(string mensaje)
    {
        List<Task> tareas = new List<Task>();

        foreach (var cliente in clients)
        {
            tareas.Add(Task.Run(async () =>
            {
                try
                {
                    NetworkStream stream = cliente.GetStream();
                    StreamWriter writer = new StreamWriter(stream) { AutoFlush = true };
                    await writer.WriteLineAsync(mensaje); // Mezua bezeroari bidali
                }
                catch { }
            }));
        }

        await Task.WhenAll(tareas); // Guztiak amaitu arte itxaron
    }

    public void Itxi()
    {
        martxan = false; // Zerbitzaria gelditu
        foreach (var client in clients)
        {
            client.Close(); // Bezero guztiak itxi
        }
        server.Stop(); // Zerbitzaria itxi
        Console.WriteLine("Zerbitzaria itxi da."); // Kontsolan mezua
    }

    public static async Task Main(string[] args)
    {
        int port = 13000;
        IPAddress localAddr = IPAddress.Parse("127.0.0.1");
        MyTcpListener server = new MyTcpListener(localAddr, port); // Zerbitzaria konfiguratu

        await server.EntzunAsync(); // Entzun konexioak
        server.Itxi(); // Itxi zerbitzaria

        Console.WriteLine("\nSakatu <ENTER> irteteko..."); // Erabiltzaileari itxiera baieztatzeko
        Console.Read(); // Itxaroten ENTER sakatzeko
    }
}
