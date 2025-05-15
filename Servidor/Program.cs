using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Collections.Generic;
using System.Net.Http;

class MyTcpListener
{
    private TcpListener server; // TCP zerbitzaria sortzeko objektua
    private List<TcpClient> clients = new List<TcpClient>(); // Bezero aktiboen zerrenda
    private List<string> clientNames = new List<string>(); // Bezeroen izenen zerrenda
    private bool martxan = true; // Zerbitzaria martxan dagoen adierazten duen boolearra
    private const int MaxClients = 15; // Gehienez onartzen diren bezero kopurua
    private static readonly HttpClient httpClient = new HttpClient(); // API kontsultak egiteko HTTP bezeroa

    public MyTcpListener(IPAddress ip, int port)
    {
        // TCP zerbitzaria hasieratzen du IP helbidea eta ataka zehaztuz
        server = new TcpListener(ip, port);
    }

    public void Entzun()
    {
        try
        {
            server.Start(); // Zerbitzaria abiarazten da
            Console.WriteLine("Bezeroen konexioa itxaroten...");

            while (martxan)
            {
                // Bezero berri baten konexioa onartzen du
                TcpClient client = server.AcceptTcpClient();
                // Bezero bakoitzarekin harremana hari berri batean kudeatzen da
                Thread clientThread = new Thread(() => Komunikazioa(client));
                clientThread.Start();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Errorea zerbitzarian: {0}", e.Message);
        }
    }

    private void Komunikazioa(TcpClient client)
    {
        // Datuen komunikaziorako beharrezkoak diren objektuak
        NetworkStream str = client.GetStream();
        StreamReader sr = new StreamReader(str);
        StreamWriter sw = new StreamWriter(str) { AutoFlush = true };

        try
        {
            // Bezeroaren izena jasotzen du
            string clientName = sr.ReadLine();

            // Bezeroaren izena errepikatua bada, konexioa baztertzen du
            if (clientNames.Contains(clientName))
            {
                sw.WriteLine("Error: El nombre ya está en uso.");
                client.Close();
                return;
            }
            // Gehienezko konexio kopurua gaindituz gero, konexioa baztertzen du
            if (clients.Count >= MaxClients)
            {
                sw.WriteLine("Error: Máximo de conexiones alcanzado. Inténtelo más tarde.");
                client.Close();
                return;
            }

            // Bezeroa onartzen du eta zerrendetan gehitzen du
            clients.Add(client);
            clientNames.Add(clientName);
            Console.WriteLine($"Cliente {clientName} se ha unido. Conexiones actuales: {clients.Count}");
            sw.WriteLine($"Conectado-{clientName}");

            // Bezero guztiei erabiltzaile zerrenda eguneratua bidaltzen die
            EnviarListaDeUsuarios();

            string data;
            while ((data = sr.ReadLine()) != null)
            {
                if (data.StartsWith("DISC:"))
                {
                    // Bezero batek deskonektatzeko eskaera bidali du
                    string disconnectedUser = data.Split(':')[1];
                    Console.WriteLine($"Cliente {disconnectedUser} se desconectó.");
                    break;
                }
                else if (data.StartsWith("MSG:"))
                {
                    // Bezeroak bidalitako mezua guztiei bidaltzen zaie
                    Console.WriteLine(data);
                    EnviarMensajeATodos(data);
                }
                else if (data == "API:")
                {
                    // Bezeroak APItik datuak eskatu ditu
                    Console.WriteLine($"Cliente {clientName} solicitó citas.");
                    string citasJson = ObtenerCitasDeLaAPI();
                    sw.WriteLine("API:" + citasJson);
                    sw.Flush();
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Errorea bezeroarekin: {0}", e.Message);
        }
        finally
        {
            // Bezeroa zerrendetatik ezabatzen da
            lock (clients)
            {
                int index = clients.IndexOf(client);
                if (index != -1)
                {
                    clients.RemoveAt(index);
                    clientNames.RemoveAt(index);
                    EnviarListaDeUsuarios(); // Beste bezeroei erabiltzaile zerrenda eguneratua bidali
                }
            }
            client.Close();
            Console.WriteLine($"Conexiones actuales: {clients.Count}");
        }
    }

    /// <summary>
    /// API deia egiten du eta jasotako datuak itzultzen ditu.
    /// </summary>
    private string ObtenerCitasDeLaAPI()
    {
        try
        {
            HttpResponseMessage response = httpClient.GetAsync("http://localhost:8080/api/hitzorduak/hoy").Result;

            if (!response.IsSuccessStatusCode)
            {
                return "Error: No se pudo obtener citas de la API.";
            }

            string json = response.Content.ReadAsStringAsync().Result;

            if (string.IsNullOrWhiteSpace(json))
            {
                return "Gaur ez daude zitak";
            }

            var citas = JsonConvert.DeserializeObject<List<dynamic>>(json);

            if (citas == null || citas.Count == 0)
            {
                return "Gaur ez daude zitak";
            }

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
        catch (HttpRequestException)
        {
            return "Error: No se pudo conectar a la API";
        }
        catch (TaskCanceledException)
        {
            return "Error: No se pudo conectar a la API";
        }
        catch (Exception)
        {
            return "Error: No se pudo conectar a la API";
        }
    }



    /// <summary>
    /// Bezero guztiei erabiltzaile zerrenda eguneratua bidaltzen die.
    /// </summary>
    public void EnviarListaDeUsuarios()
    {
        string usuarios = "USUARIOS_ACTIVOS:" + string.Join(",", clientNames);
        foreach (var cliente in clients)
        {
            try
            {
                NetworkStream stream = cliente.GetStream();
                StreamWriter writer = new StreamWriter(stream) { AutoFlush = true };
                writer.WriteLine(usuarios);
            }
            catch { }
        }
    }

    /// <summary>
    /// Bezero guztiei mezua bidaltzen die.
    /// </summary>
    public void EnviarMensajeATodos(string mensaje)
    {
        foreach (var cliente in clients)
        {
            try
            {
                NetworkStream stream = cliente.GetStream();
                StreamWriter writer = new StreamWriter(stream) { AutoFlush = true };
                writer.WriteLine(mensaje);
            }
            catch { }
        }
    }

    /// <summary>
    /// Zerbitzaria gelditzen du eta bezero guztiak deskonektatzen ditu.
    /// </summary>
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

    /// <summary>
    /// Programaren abiarazlea.
    /// </summary>
    public static void Main(string[] args)
    {
        int port = 13000; // Ataka
        IPAddress localAddr = IPAddress.Parse("127.0.0.1"); // IP helbidea
        MyTcpListener server = new MyTcpListener(localAddr, port); // Zerbitzaria sortu
        server.Entzun(); // Zerbitzaria entzuten jarri
        server.Itxi(); // Itxi zerbitzaria

        Console.WriteLine("\nSakatu <ENTER> irteteko...");
        Console.Read();
    }
}
