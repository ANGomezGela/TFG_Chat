using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ChatProject.NetWorking;


namespace ChatProject
{
    public partial class ChatGeneral : Form
    {
        private TCPClient cliente;
        private string usuario;

        public ChatGeneral(string izena, string servidorIP, int puerto)
        {
            InitializeComponent();
            cliente = new TCPClient();
            usuario = izena;

            Konexioa(servidorIP, puerto);
        }
        private void Konexioa(string servidorIP, int puerto)
        {
            try
            {
                cliente.Konektatu(servidorIP, puerto, usuario);
                lstMensajes.Items.Add("Conectado como: " + usuario);
                lstUsuarios.Items.Add(usuario);  // Agregar usuario a la lista de conectados

                Task.Run(() => Komunikazioa());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al conectar: " + ex.Message);
                this.Close();
            }
        }
        private void Komunikazioa()
        {
            while (true)
            {
                string mensaje = cliente.ErakutsiErantzuna();
                if (!string.IsNullOrEmpty(mensaje))
                {
                    lstMensajes.Invoke(new MethodInvoker(delegate
                    {
                        lstMensajes.Items.Add(mensaje);
                    }));

                    if (mensaje.Contains("se ha conectado"))
                    {
                        string nuevoUsuario = mensaje.Split(' ')[0];
                        if (!lstUsuarios.Items.Contains(nuevoUsuario))
                        {
                            lstUsuarios.Invoke(new MethodInvoker(delegate
                            {
                                lstUsuarios.Items.Add(nuevoUsuario);
                            }));
                        }
                    }
                }
            }
        }
        private void ChatGeneral_Load(object sender, EventArgs e)
        {

        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            cliente.Itxi();
            lstMensajes.Items.Add("Conexión cerrada.");
            btnEnviar.Enabled = false;
            btnCerrar.Enabled = false;
        }

        private void btnEnviar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtMensaje.Text))
            {
                try
                {
                    // Enviar el mensaje al servidor
                    cliente.BidaliDatuak(txtMensaje.Text);

                    // Agregar el mensaje a la lista de mensajes en la interfaz
                    lstMensajes.Items.Add("Tú: " + txtMensaje.Text);

                    // Limpiar el campo de texto después de enviar el mensaje
                    txtMensaje.Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al enviar mensaje: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Introduce un mensaje antes de enviar.");
            }
        }

    }
}
