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
    private List<TcpClient> clients = new List<TcpClient>();
    private List<string> clientNames = new List<string>();
    private bool martxan = true;
    private const int MaxClients = 15;
    private static readonly HttpClient httpClient = new HttpClient();

    public MyTcpListener(IPAddress ip, int port)
    {
        server = new TcpListener(ip, port);
    }

    public async Task EntzunAsync()
    {
        try
        {
            server.Start();
            Console.WriteLine("Bezeroen konexioa itxaroten...");

            while (martxan)
            {
                TcpClient client = await server.AcceptTcpClientAsync();
                _ = Task.Run(() => KomunikazioaAsync(client));
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Errorea zerbitzarian: {0}", e.Message);
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
                string clientName = await sr.ReadLineAsync();

                if (clientNames.Contains(clientName))
                {
                    await sw.WriteLineAsync("Error: El nombre ya está en uso.");
                    client.Close();
                    return;
                }

                if (clients.Count >= MaxClients)
                {
                    await sw.WriteLineAsync("Error: Máximo de conexiones alcanzado. Inténtelo más tarde.");
                    client.Close();
                    return;
                }

                lock (clients)
                {
                    clients.Add(client);
                    clientNames.Add(clientName);
                }

                Console.WriteLine($"Cliente {clientName} se ha unido. Conexiones actuales: {clients.Count}");
                await sw.WriteLineAsync($"Conectado-{clientName}");
                await EnviarListaDeUsuariosAsync();

                string data;
                while ((data = await sr.ReadLineAsync()) != null)
                {
                    if (data.StartsWith("DISC:"))
                    {
                        string disconnectedUser = data.Split(':')[1];
                        Console.WriteLine($"Cliente {disconnectedUser} se desconectó.");
                        break;
                    }
                    else if (data.StartsWith("MSG:"))
                    {
                        Console.WriteLine(data);
                        await EnviarMensajeATodosAsync(data);
                    }
                    else if (data == "API:")
                    {
                        Console.WriteLine($"Cliente {clientName} solicitó citas.");
                        string citasJson = await ObtenerCitasDeLaAPIAsync();
                        await sw.WriteLineAsync("API:" + citasJson);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Errorea bezeroarekin: {0}", e.Message);
            }
            finally
            {
                lock (clients)
                {
                    int index = clients.IndexOf(client);
                    if (index != -1)
                    {
                        clients.RemoveAt(index);
                        clientNames.RemoveAt(index);
                    }
                }
                await EnviarListaDeUsuariosAsync();
                client.Close();
                Console.WriteLine($"Conexiones actuales: {clients.Count}");
            }
        }
    }

    private async Task<string> ObtenerCitasDeLaAPIAsync()
    {
        try
        {
            HttpResponseMessage response = await httpClient.GetAsync("http://localhost:8080/api/hitzorduak/hoy");

            if (!response.IsSuccessStatusCode)
                return "Error: No se pudo obtener citas de la API.";

            string json = await response.Content.ReadAsStringAsync();

            if (string.IsNullOrWhiteSpace(json))
                return "Gaur ez daude zitak";

            var citas = JsonConvert.DeserializeObject<List<dynamic>>(json);

            if (citas == null || citas.Count == 0)
                return "Gaur ez daude zitak";

            var citasFiltradas = new List<object>();
            foreach (var cita in citas)
            {
                citasFiltradas.Add(new
                {
                    izena = (string)cita.izena,
                    hasieraOrdua = (string)cita.hasieraOrdua,
                    amaieraOrdua = (string)cita.amaieraOrdua
                });
            }

            return JsonConvert.SerializeObject(citasFiltradas);
        }
        catch
        {
            return "Error: No se pudo conectar a la API";
        }
    }

    public async Task EnviarListaDeUsuariosAsync()
    {
        string usuarios = "USUARIOS_ACTIVOS:" + string.Join(",", clientNames);
        List<Task> tareas = new List<Task>();

        foreach (var cliente in clients)
        {
            tareas.Add(Task.Run(async () =>
            {
                try
                {
                    NetworkStream stream = cliente.GetStream();
                    StreamWriter writer = new StreamWriter(stream) { AutoFlush = true };
                    await writer.WriteLineAsync(usuarios);
                }
                catch { }
            }));
        }

        await Task.WhenAll(tareas);
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
                    await writer.WriteLineAsync(mensaje);
                }
                catch { }
            }));
        }

        await Task.WhenAll(tareas);
    }

    public void Itxi()
    {
        martxan = false;
        foreach (var client in clients)
        {
            client.Close();
        }
        server.Stop();
        Console.WriteLine("Zerbitzaria itxi da.");
    }

    public static async Task Main(string[] args)
    {
        int port = 13000;
        IPAddress localAddr = IPAddress.Parse("127.0.0.1");
        MyTcpListener server = new MyTcpListener(localAddr, port);

        await server.EntzunAsync();
        server.Itxi();

        Console.WriteLine("\nSakatu <ENTER> irteteko...");
        Console.Read();
    }
}
