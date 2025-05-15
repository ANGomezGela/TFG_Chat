using System;
using System.IO;
using System.Net.Sockets;
using System.Windows.Forms;

namespace Cliente
{
    public partial class Login : Form
    {
        public event EventHandler<LoginEventArgs> LoginSuccess;  // Login arrakastatsua adierazteko ekitaldia

        private TcpClient client;
        private NetworkStream stream;
        private StreamReader reader;
        private StreamWriter writer;

        public Login()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized; // Leihoa pantaila osora handitu

            // Gertaera gehitu testu-eremuko (t_izena) tekla sakatzean
            t_izena.KeyDown += new KeyEventHandler(t_izena_KeyDown);
        }
        private void t_izena_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                b_login_Click(sender, e); // Enter sakatzean botoiaren kodea deitu
            }
        }
        private void b_login_Click(object sender, EventArgs e)
        {
            string clientName = t_izena.Text.Trim(); // Erabiltzaile-izena lortu

            if (string.IsNullOrEmpty(clientName))
            {
                MessageBox.Show("Mesedez, sartu izen bat."); // Erabiltzailea hutsik badago, mezu bat erakutsi
                return;
            }

            try
            {
                client = new TcpClient("127.0.0.1", 13000); // Zerbitzarira konektatu
                stream = client.GetStream(); // Datu-fluxua sortu
                reader = new StreamReader(stream);
                writer = new StreamWriter(stream) { AutoFlush = true };

                writer.WriteLine(clientName); // Erabiltzaile-izena bidali zerbitzarira
                string serverResponse = reader.ReadLine(); // Zerbitzariaren erantzuna irakurri

                if (serverResponse.StartsWith("Conectado-")) // Konektatuta badago
                {
                    string[] parts = serverResponse.Split('-');
                    string serverName = parts[1];

                    this.Hide(); // Inprimakia ezkutatu, ez itxi
                    LoginSuccess?.Invoke(this, new LoginEventArgs(client, stream, reader, writer, serverName));
                }
                else
                {
                    MessageBox.Show(serverResponse); // Errore-mezua erakutsi
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Errorea zerbitzarira konektatzean: {ex.Message}"); // Konektatzean errorea gertatzen bada, mezua erakutsi
            }
        }
    }

    public class LoginEventArgs : EventArgs
    {
        public TcpClient Client { get; } // TCP bezeroa
        public NetworkStream Stream { get; } // Sareko datu-fluxua
        public StreamReader Reader { get; } // Datuak irakurtzeko objektua
        public StreamWriter Writer { get; } // Datuak idazteko objektua
        public string UserName { get; } // Erabiltzailearen izena

        public LoginEventArgs(TcpClient client, NetworkStream stream, StreamReader reader, StreamWriter writer, string userName)
        {
            Client = client;
            Stream = stream;
            Reader = reader;
            Writer = writer;
            UserName = userName;
        }
    }
}
