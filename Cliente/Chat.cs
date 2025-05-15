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
        private bool isRunning = true; // Honi esker, hariak jarraitu behar duen ala ez kontrolatzen dugu

        public Chat(TcpClient client, NetworkStream stream, StreamReader reader, StreamWriter writer, string clientName)
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized; // Leihoa maximizatuta hasten da

            this.client = client;
            this.stream = stream;
            this.reader = reader;
            this.writer = writer;
            this.clientName = clientName;
            this.FormClosing += Chat_FormClosing;

            this.t_mensaje.KeyDown += new System.Windows.Forms.KeyEventHandler(this.t_mensaje_KeyDown);
            this.b_actualizar.Click += new System.EventHandler(this.b_actualizar_Click);

            // Zerbitzaritik mezuak jasotzeko hari bat hasten da
            Task.Run(() => RecibirMensajesServidor());
        }

        private void Chat_FormClosing(object sender, FormClosingEventArgs e)
        {
            EnviarDesconexion(); // Zerbitzarira deskonexio mezua bidaltzen du
            CerrarConexion(); // Konexioa itxi
            Application.Exit(); // Aplikazioa guztiz ixten du
        }

        private void b_desconectar_Click(object sender, EventArgs e)
        {
            EnviarDesconexion();
            CerrarConexion();
            Application.Restart(); // Aplikazioa berrabiarazi (login-era itzultzeko)
        }

        /// <summary>
        /// Zerbitzarira deskonexio mezua bidaltzen du.
        /// </summary>
        private void EnviarDesconexion()
        {
            if (client != null && client.Connected)
            {
                try
                {
                    writer.WriteLine($"DISC: {clientName}");
                    writer.Flush();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Errorea deskonektatzean: " + ex.Message);
                }
            }
        }

        /// <summary>
        /// Konexioa eta baliabide guztiak ixten ditu.
        /// </summary>
        private void CerrarConexion()
        {
            isRunning = false;
            reader?.Close();
            writer?.Close();
            stream?.Close();
            client?.Close();
        }

        /// <summary>
        /// Zerbitzaritik mezuak jasotzen ditu eta prozesatzen ditu.
        /// </summary>
        private void RecibirMensajesServidor()
        {
            try
            {
                while (isRunning && client.Connected)
                {
                    string serverMessage = reader.ReadLine();
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

        /// <summary>
        /// Zerbitzaritik jasotako mezuak prozesatzen ditu.
        /// </summary>
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
                if (citasJson == "Gaur ez daude zitak")
                {
                    listBoxCitas.Invoke(new Action(() =>
                    {
                        listBoxCitas.Items.Clear();
                        listBoxCitas.Items.Add("Gaur ez daude zitak");
                    }));
                    return;
                }

                List<dynamic> citas = JsonConvert.DeserializeObject<List<dynamic>>(citasJson);
                listBoxCitas.Invoke(new Action(() =>
                {
                    listBoxCitas.Items.Clear();
                    foreach (var cita in citas)
                    {
                        listBoxCitas.Items.Add($"{cita.izena} - {cita.hasieraOrdua} a {cita.amaieraOrdua}");
                    }
                }));
            }
        }

        /// <summary>
        /// Konektatutako erabiltzaileen zerrenda eguneratzen du.
        /// </summary>
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

        /// <summary>
        /// Mezu bat bidaltzen du zerbitzarira.
        /// </summary>
        private void EnviarMensaje(string mensaje)
        {
            if (!string.IsNullOrWhiteSpace(mensaje))
            {
                try
                {
                    t_mensaje.Clear();
                    writer.WriteLine($"MSG:{clientName}:{mensaje}");
                    writer.Flush();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Errorea mezua bidaltzean: " + ex.Message);
                }
            }
        }

        /// <summary>
        /// Mezu bat erakusten du interfazean.
        /// </summary>
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

        private void b_actualizar_Click(object sender, EventArgs e)
        {
            try
            {
                writer.WriteLine("API:");
                writer.Flush();
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
