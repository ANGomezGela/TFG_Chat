using System;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace Cliente
{
    public partial class Chat : Form
    {
        private TcpClient client;
        private NetworkStream stream;
        private StreamReader reader;
        private StreamWriter writer;
        private string clientName;
        private bool isRunning = true;

        public Chat(TcpClient client, NetworkStream stream, StreamReader reader, StreamWriter writer, string clientName)
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;

            this.client = client;
            this.stream = stream;
            this.reader = reader;
            this.writer = writer;
            this.clientName = clientName;
            this.FormClosing += Chat_FormClosing;

            this.t_mensaje.KeyDown += new KeyEventHandler(this.t_mensaje_KeyDown);
            this.b_actualizar.Click += new EventHandler(this.b_actualizar_Click);

            Task.Run(RecibirMensajesServidor);
        }

        private void Chat_FormClosing(object sender, FormClosingEventArgs e)
        {
            EnviarDesconexion();
            CerrarConexion();
            Application.Exit();
        }

        private void b_desconectar_Click(object sender, EventArgs e)
        {
            EnviarDesconexion();
            CerrarConexion();
            Application.Restart();
        }

        private async void EnviarDesconexion()
        {
            if (client != null && client.Connected)
            {
                try
                {
                    await writer.WriteLineAsync($"DISC: {clientName}");
                    await writer.FlushAsync();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Errorea deskonektatzean: " + ex.Message);
                }
            }
        }

        private void CerrarConexion()
        {
            isRunning = false;
            reader?.Close();
            writer?.Close();
            stream?.Close();
            client?.Close();
        }

        private async Task RecibirMensajesServidor()
        {
            try
            {
                while (isRunning && client.Connected)
                {
                    string serverMessage = await reader.ReadLineAsync();
                    if (!isRunning) break;

                    if (!string.IsNullOrEmpty(serverMessage))
                    {
                        ProcesarMensaje(serverMessage);
                    }
                }
            }
            catch (Exception ex)
            {
                if (isRunning)
                {
                    MessageBox.Show("Errorea zerbitzariko datuak jasotzean: " + ex.Message);
                }
            }
        }

        private void ProcesarMensaje(string message)
        {
            if (message.StartsWith("USUARIOS_ACTIVOS:"))
            {
                string[] usuarios = message.Substring("USUARIOS_ACTIVOS:".Length).Split(',');
                ActualizarUsuariosActivos(usuarios);
            }
            else if (message.StartsWith("MSG:"))
            {
                string[] parts = message.Split(new[] { ':' }, 3);
                if (parts.Length == 3)
                {
                    MostrarMensaje(parts[1], parts[2]);
                }
            }
            else if (message.StartsWith("API:"))
            {
                string citasJson = message.Substring("API:".Length).Trim();

                // Si el mensaje indica un error explícito de conexión
                if (citasJson.StartsWith("ERROR:"))
                {
                    listBoxCitas.Invoke(new Action(() =>
                    {
                        listBoxCitas.Items.Clear();
                        listBoxCitas.Items.Add("Ezin da konektatu datu-basearekin"); // Mensaje en euskera
                    }));
                    return;
                }

                if (citasJson == "Gaur ez daude zitak")
                {
                    listBoxCitas.Invoke(new Action(() =>
                    {
                        listBoxCitas.Items.Clear();
                        listBoxCitas.Items.Add("Gaur ez daude zitak");
                    }));
                    return;
                }

                try
                {
                    var citas = JsonConvert.DeserializeObject<List<dynamic>>(citasJson);
                    listBoxCitas.Invoke(new Action(() =>
                    {
                        listBoxCitas.Items.Clear();
                        foreach (var cita in citas)
                        {
                            listBoxCitas.Items.Add($"{cita.izena} - {cita.hasieraOrdua} a {cita.amaieraOrdua}");
                        }
                    }));
                }
                catch (Exception)
                {
                    // Si hay un error al deserializar el JSON, mostramos un mensaje genérico
                    listBoxCitas.Invoke(new Action(() =>
                    {
                        listBoxCitas.Items.Clear();
                        listBoxCitas.Items.Add("Errorea zitak eskuratzean");
                    }));
                }
            }

        }
        private void ActualizarUsuariosActivos(string[] usuarios)
        {
            this.Invoke(new Action(() =>
            {
                for (int i = 0; i < 15; i++)
                {
                    var label = this.Controls.Find("l_activo" + (i + 1), true).FirstOrDefault() as Label;
                    if (label != null) label.Text = "";
                }

                for (int i = 0; i < usuarios.Length && i < 15; i++)
                {
                    var label = this.Controls.Find("l_activo" + (i + 1), true).FirstOrDefault() as Label;
                    if (label != null)
                    {
                        label.Text = (usuarios[i] == clientName) ? usuarios[i] + " (Yo)" : usuarios[i];
                    }
                }
            }));
        }

        private async void EnviarMensaje(string mensaje)
        {
            if (!string.IsNullOrWhiteSpace(mensaje))
            {
                try
                {
                    t_mensaje.Clear();
                    await writer.WriteLineAsync($"MSG:{clientName}:{mensaje}");
                    await writer.FlushAsync();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Errorea mezua bidaltzean: " + ex.Message);
                }
            }
        }

        private void MostrarMensaje(string sender, string message)
        {
            this.Invoke(new Action(() =>
            {
                bool esMio = sender == clientName;
                new Mensaje(sender, message, esMio).AgregarMensaje(p_central);
            }));
        }

        private void t_mensaje_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                EnviarMensaje(t_mensaje.Text);
            }
        }

        private async void b_actualizar_Click(object sender, EventArgs e)
        {
            try
            {
                await writer.WriteLineAsync("API:");
                await writer.FlushAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errorea eguneratzea eskatzean: " + ex.Message);
            }
        }

        private void b_mandar_Click(object sender, EventArgs e)
        {
            EnviarMensaje(t_mensaje.Text);
        }
    }
}
