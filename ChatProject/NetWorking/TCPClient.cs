using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ChatProject.NetWorking
{
    public class TCPClient
    {
        // Klasearen atributuak.

        // Bezero socket-a.
        TcpClient client = null;

        // Stream bat bit puntuen arteko datu-fluxu bat da, eta tenporalki buffer batean biltegiratzen dira (fitxategi bat balitz bezala).
        // Idazketa-eragiketak (jatorrizko puntuaren aldetik) eta irakurketa-eragiketak (jatorrizko puntuan) tartekatzeko aukera ematen du.
        NetworkStream str = null;

        // StreamReader eta StreamWriter objektuak datuak era eroso baten bidaltzen usten digu, Kontsolatik idazten egongo bagenu bezala.
        StreamReader sr = null;
        StreamWriter sw = null;

        /**
         * Eraikitzailea.
         */
        public TCPClient()
        {

        }

        /**
         * Konektatu emandako ip helbide eta portu-zenbakia daukan zerbitzarira.
         */
        public void Konektatu(string server, int port, string usuario)
        {
            try
            {
                // Bezero socket-a sortu. Hemen konexioa irekitzen da ere bai.
                client = new TcpClient(server, port);
                // Stream-a ateratzen dugu.
                str = client.GetStream();
                // StreamReader eta StreamWriter objektuak datuak era eroso baten bidaltzen usten digu, Kontsolatik idazten egongo bagenu bezala.
                sr = new StreamReader(str);
                sw = new StreamWriter(str);

                sw.WriteLine(usuario);
                sw.Flush();

            }
            catch (Exception e)
            {
                Console.WriteLine("Socket edo buffer-a sortzen errorea: {0}", e);
            }
        }

        /**
         * Bidali zerbitzariari kontsolan irakurritako esaldia letra larrietara bihur dezan.
         */
        public void BidaliDatuak(string mensaje)
        {
            try
            {
                sw.WriteLine(mensaje);
                sw.Flush();  
            }
            catch (Exception e)
            {
                Console.WriteLine("Errorea mezua bidaltzerakoan: {0}", e.Message);
            }
        }

        /**
         * Irakurri zerbitzariak bidalitako erantzuna eta erakutsi kontsolatik.
         */
        public string ErakutsiErantzuna()
        {
            try
            {
                return sr.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine("Errorea datuak jasotzean: {0}", e.Message);
                return null;
            }
        }

        /**
         * Itxi konexio danak.
         */
        public void Itxi()
        {
            try
            {
                sr.Close();
                sw.Close();
                str.Close();
                client.Close();
                Console.WriteLine("Konexioak itxi dira.");
            }
            catch (Exception e)
            {
                Console.WriteLine("Konexioak ezin izan dira itxi: {0}", e);
            }
        }

        /**
         * Main metodoa, programa hemen hasten da.
         */
        public static int Main(string[] args)
        {
            // Zerbitzariarekin komunikatzeko behar diren datuak: IP helbidea eta portu-zenbakia.
            string zerbitzariIPa = "127.0.0.1";
            int port = 13000;
            // Guk definitutako klasearen objektua sortu.
            TCPClient bezeroAplikazioa = new TCPClient();
            // Konektatu zerbitzarira.
            bezeroAplikazioa.Konektatu(zerbitzariIPa, port);
            // Bidali datuak zerbitzarira.
            bezeroAplikazioa.BidaliDatuak();
            // Jasotako erantzuna kudeatu.
            bezeroAplikazioa.ErakutsiErantzuna();
            // Itxi konexio danak.
            bezeroAplikazioa.Itxi();

            Console.WriteLine("\nSakatu <ENTER> bukatzeko...");
            Console.Read();
            return 0;
        }
    }
}
