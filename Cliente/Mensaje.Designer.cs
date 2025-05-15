namespace Cliente
{
    partial class Mensaje
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblNombre;
        private Label lblMensaje;
        private Label lblHora;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            lblNombre = new Label();
            lblMensaje = new Label();
            lblHora = new Label();

            SuspendLayout();

            // Nombre del remitente
            lblNombre.AutoSize = true;
            lblNombre.Font = new Font("Arial", 10, FontStyle.Bold);
            lblNombre.Location = new Point(5, 5);
            lblNombre.ForeColor = Color.Black;

            // Mensaje
            lblMensaje.AutoSize = true;
            lblMensaje.Font = new Font("Arial", 10);
            lblMensaje.Location = new Point(5, 25);
            lblMensaje.MaximumSize = new Size(300, 0); // Limita el ancho, pero crece en altura automáticamente

            // Hora
            lblHora.AutoSize = true;
            lblHora.Font = new Font("Arial", 8);
            lblHora.ForeColor = Color.Gray;
            lblHora.Location = new Point(250, 5);
            lblHora.Text = "a";

            // Agregar controles
            Controls.Add(lblNombre);
            Controls.Add(lblMensaje);
            Controls.Add(lblHora);

            // Configurar el UserControl
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Padding = new Padding(10);
            Margin = new Padding(5);

            



            ResumeLayout(false);
            PerformLayout();
        }
    }
}
