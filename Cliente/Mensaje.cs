namespace Cliente
{
    public partial class Mensaje : UserControl
    {
        public Mensaje(string nombre, string mensaje, bool esMio)
        {
            InitializeComponent();
            lblNombre.Text = nombre;
            lblMensaje.Text = mensaje;
            lblHora.Text = DateTime.Now.ToString("HH:mm");

            // Kolorea eta lerrokatzea aldatzea
            if (esMio)
            {
                this.BackColor = Color.LightGreen;  // Mezua erabiltzailearena bada, berde argia jarri
                lblNombre.TextAlign = ContentAlignment.MiddleRight;
                lblMensaje.TextAlign = ContentAlignment.MiddleRight;
                lblHora.TextAlign = ContentAlignment.MiddleRight;
            }
            else
            {
                this.BackColor = Color.LightGray;  // Bestelako mezuak gris kolorekoa izango dira
                lblNombre.TextAlign = ContentAlignment.MiddleLeft;
                lblMensaje.TextAlign = ContentAlignment.MiddleLeft;
                lblHora.TextAlign = ContentAlignment.MiddleLeft;
            }

            // Mezuaren zabalera finkoa ezartzea
            this.Width = 350;  // Mezuaren zabalera definitzen da (aldatu daiteke beharrezkoa bada)

            // Mezuaren testua horizontalki ez zabaltzeko neurriak hartu
            lblMensaje.MaximumSize = new Size(this.Width, 0); // Zabalera mugatzea
            lblMensaje.AutoSize = false;  // Zabalera automatikoki ez da egokituko, baina altuera bai

            // Mezuaren edukia kontuan hartuta altuera doitzea
            lblMensaje.Height = lblMensaje.PreferredHeight; // Testuaren arabera altuera egokitu

            // Kontrol osoaren altuera doitzea
            this.Height = lblNombre.Height + lblMensaje.Height + lblHora.Height + 10; // Altueraren doikuntza marjinekin
        }

        public void AgregarMensaje(FlowLayoutPanel panel)
        {
            // Mezu bakoitzerako edukiontzi bat sortzea
            Panel contenedor = new Panel();
            contenedor.AutoSize = true;  // Edukiontzia automatikoki egokitzen da mezuaren neurrira
            contenedor.MaximumSize = new Size(panel.ClientSize.Width, 0);  // Edukiontziak panelaren neurria ez gainditzeko

            // Mezuen lerrokatzea ezartzeko marjinak
            if (this.BackColor == Color.LightGreen) // Erabiltzailearen mezua bada
            {
                contenedor.Margin = new Padding(panel.ClientSize.Width - this.Width, 0, 0, 0);  // Eskuinean lerrokatu
            }
            else // Beste erabiltzaile baten mezua bada
            {
                contenedor.Margin = new Padding(0, 0, panel.ClientSize.Width - this.Width, 0);  // Ezkerrean lerrokatu
            }

            // Mezua edukiontzira gehitzea
            contenedor.Controls.Add(this);

            // Edukiontzia FlowLayoutPanel-era gehitzea
            panel.Controls.Add(contenedor);

            // Panela behera mugitzea azken mezua beti ikusgai egon dadin
            panel.ScrollControlIntoView(this);

            // FlowLayoutPanel-eko eskualdatze horizontala desgaitzea
            panel.AutoScroll = true;
            panel.HorizontalScroll.Visible = false;  // Eskualdatze barra horizontala ezkutatu
        }
    }
}
