namespace Cliente
{
    partial class Chat
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            tableLayoutPanelPrincipal = new TableLayoutPanel();
            tableLayoutPanelIzquierdaGeneral = new TableLayoutPanel();
            b_desconectar = new Button();
            logo_Hilea = new Panel();
            Activos = new Label();
            tableLayoutPanelActivos = new TableLayoutPanel();
            l_activo15 = new Label();
            l_activo14 = new Label();
            l_activo13 = new Label();
            l_activo12 = new Label();
            l_activo11 = new Label();
            l_activo10 = new Label();
            l_activo9 = new Label();
            l_activo8 = new Label();
            l_activo7 = new Label();
            l_activo6 = new Label();
            l_activo5 = new Label();
            l_activo4 = new Label();
            l_activo3 = new Label();
            l_activo2 = new Label();
            l_activo1 = new Label();
            tableLayoutPanelMedioGeneral = new TableLayoutPanel();
            p_titulo = new Panel();
            titulo = new Label();
            tableLayoutPanelMedioAbajo = new TableLayoutPanel();
            b_mandar = new Button();
            t_mensaje = new TextBox();
            p_central = new FlowLayoutPanel();
            tableLayoutPanelDerechaGeneral = new TableLayoutPanel();
            logo_Santurtzi = new Panel();
            l_citas = new Label();
            b_actualizar = new Button();
            listBoxCitas = new ListBox();
            tableLayoutPanelPrincipal.SuspendLayout();
            tableLayoutPanelIzquierdaGeneral.SuspendLayout();
            tableLayoutPanelActivos.SuspendLayout();
            tableLayoutPanelMedioGeneral.SuspendLayout();
            p_titulo.SuspendLayout();
            tableLayoutPanelMedioAbajo.SuspendLayout();
            tableLayoutPanelDerechaGeneral.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanelPrincipal
            // 
            tableLayoutPanelPrincipal.BackColor = Color.RoyalBlue;
            tableLayoutPanelPrincipal.BackgroundImageLayout = ImageLayout.Zoom;
            tableLayoutPanelPrincipal.ColumnCount = 3;
            tableLayoutPanelPrincipal.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15F));
            tableLayoutPanelPrincipal.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70F));
            tableLayoutPanelPrincipal.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15F));
            tableLayoutPanelPrincipal.Controls.Add(tableLayoutPanelIzquierdaGeneral, 0, 0);
            tableLayoutPanelPrincipal.Controls.Add(tableLayoutPanelMedioGeneral, 1, 0);
            tableLayoutPanelPrincipal.Controls.Add(tableLayoutPanelDerechaGeneral, 2, 0);
            tableLayoutPanelPrincipal.Dock = DockStyle.Fill;
            tableLayoutPanelPrincipal.Location = new Point(0, 0);
            tableLayoutPanelPrincipal.Name = "tableLayoutPanelPrincipal";
            tableLayoutPanelPrincipal.RowCount = 1;
            tableLayoutPanelPrincipal.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanelPrincipal.Size = new Size(1864, 1050);
            tableLayoutPanelPrincipal.TabIndex = 0;
            // 
            // tableLayoutPanelIzquierdaGeneral
            // 
            tableLayoutPanelIzquierdaGeneral.BackColor = Color.RoyalBlue;
            tableLayoutPanelIzquierdaGeneral.ColumnCount = 1;
            tableLayoutPanelIzquierdaGeneral.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanelIzquierdaGeneral.Controls.Add(b_desconectar, 0, 3);
            tableLayoutPanelIzquierdaGeneral.Controls.Add(logo_Hilea, 0, 0);
            tableLayoutPanelIzquierdaGeneral.Controls.Add(Activos, 0, 1);
            tableLayoutPanelIzquierdaGeneral.Controls.Add(tableLayoutPanelActivos, 0, 2);
            tableLayoutPanelIzquierdaGeneral.Dock = DockStyle.Fill;
            tableLayoutPanelIzquierdaGeneral.Location = new Point(3, 3);
            tableLayoutPanelIzquierdaGeneral.Name = "tableLayoutPanelIzquierdaGeneral";
            tableLayoutPanelIzquierdaGeneral.RowCount = 4;
            tableLayoutPanelIzquierdaGeneral.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tableLayoutPanelIzquierdaGeneral.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            tableLayoutPanelIzquierdaGeneral.RowStyles.Add(new RowStyle(SizeType.Percent, 60F));
            tableLayoutPanelIzquierdaGeneral.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            tableLayoutPanelIzquierdaGeneral.Size = new Size(273, 1044);
            tableLayoutPanelIzquierdaGeneral.TabIndex = 0;
            // 
            // b_desconectar
            // 
            b_desconectar.BackColor = Color.PowderBlue;
            b_desconectar.Cursor = Cursors.Hand;
            b_desconectar.Dock = DockStyle.Fill;
            b_desconectar.Font = new Font("Verdana", 12F, FontStyle.Bold);
            b_desconectar.Location = new Point(3, 941);
            b_desconectar.Name = "b_desconectar";
            b_desconectar.Size = new Size(267, 100);
            b_desconectar.TabIndex = 3;
            b_desconectar.Text = "DESKONEKTATU";
            b_desconectar.UseVisualStyleBackColor = false;
            b_desconectar.Click += b_desconectar_Click;
            // 
            // logo_Hilea
            // 
            logo_Hilea.BackColor = Color.Transparent;
            logo_Hilea.BackgroundImage = Properties.Resources.IMP_Logotipoa_oscuro;
            logo_Hilea.BackgroundImageLayout = ImageLayout.Zoom;
            logo_Hilea.Dock = DockStyle.Fill;
            logo_Hilea.Location = new Point(3, 3);
            logo_Hilea.Name = "logo_Hilea";
            logo_Hilea.Size = new Size(267, 202);
            logo_Hilea.TabIndex = 0;
            // 
            // Activos
            // 
            Activos.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            Activos.AutoSize = true;
            Activos.Font = new Font("Verdana", 16F, FontStyle.Bold);
            Activos.Location = new Point(3, 208);
            Activos.Name = "Activos";
            Activos.Size = new Size(267, 104);
            Activos.TabIndex = 1;
            Activos.Text = "AKTIBOAK:";
            Activos.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanelActivos
            // 
            tableLayoutPanelActivos.AutoScroll = true;
            tableLayoutPanelActivos.ColumnCount = 2;
            tableLayoutPanelActivos.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 8F));
            tableLayoutPanelActivos.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 92F));
            tableLayoutPanelActivos.Controls.Add(l_activo15, 1, 14);
            tableLayoutPanelActivos.Controls.Add(l_activo14, 1, 13);
            tableLayoutPanelActivos.Controls.Add(l_activo13, 1, 12);
            tableLayoutPanelActivos.Controls.Add(l_activo12, 1, 11);
            tableLayoutPanelActivos.Controls.Add(l_activo11, 1, 10);
            tableLayoutPanelActivos.Controls.Add(l_activo10, 1, 9);
            tableLayoutPanelActivos.Controls.Add(l_activo9, 1, 8);
            tableLayoutPanelActivos.Controls.Add(l_activo8, 1, 7);
            tableLayoutPanelActivos.Controls.Add(l_activo7, 1, 6);
            tableLayoutPanelActivos.Controls.Add(l_activo6, 1, 5);
            tableLayoutPanelActivos.Controls.Add(l_activo5, 1, 4);
            tableLayoutPanelActivos.Controls.Add(l_activo4, 1, 3);
            tableLayoutPanelActivos.Controls.Add(l_activo3, 1, 2);
            tableLayoutPanelActivos.Controls.Add(l_activo2, 1, 1);
            tableLayoutPanelActivos.Controls.Add(l_activo1, 1, 0);
            tableLayoutPanelActivos.Dock = DockStyle.Fill;
            tableLayoutPanelActivos.Location = new Point(3, 315);
            tableLayoutPanelActivos.Name = "tableLayoutPanelActivos";
            tableLayoutPanelActivos.RowCount = 15;
            tableLayoutPanelActivos.RowStyles.Add(new RowStyle(SizeType.Percent, 6.666668F));
            tableLayoutPanelActivos.RowStyles.Add(new RowStyle(SizeType.Percent, 6.66666746F));
            tableLayoutPanelActivos.RowStyles.Add(new RowStyle(SizeType.Percent, 6.66666746F));
            tableLayoutPanelActivos.RowStyles.Add(new RowStyle(SizeType.Percent, 6.66666746F));
            tableLayoutPanelActivos.RowStyles.Add(new RowStyle(SizeType.Percent, 6.66666746F));
            tableLayoutPanelActivos.RowStyles.Add(new RowStyle(SizeType.Percent, 6.66666746F));
            tableLayoutPanelActivos.RowStyles.Add(new RowStyle(SizeType.Percent, 6.66666746F));
            tableLayoutPanelActivos.RowStyles.Add(new RowStyle(SizeType.Percent, 6.66666746F));
            tableLayoutPanelActivos.RowStyles.Add(new RowStyle(SizeType.Percent, 6.66666746F));
            tableLayoutPanelActivos.RowStyles.Add(new RowStyle(SizeType.Percent, 6.66666746F));
            tableLayoutPanelActivos.RowStyles.Add(new RowStyle(SizeType.Percent, 6.66666746F));
            tableLayoutPanelActivos.RowStyles.Add(new RowStyle(SizeType.Percent, 6.66666746F));
            tableLayoutPanelActivos.RowStyles.Add(new RowStyle(SizeType.Percent, 6.66666746F));
            tableLayoutPanelActivos.RowStyles.Add(new RowStyle(SizeType.Percent, 6.66666746F));
            tableLayoutPanelActivos.RowStyles.Add(new RowStyle(SizeType.Percent, 6.66666746F));
            tableLayoutPanelActivos.Size = new Size(267, 620);
            tableLayoutPanelActivos.TabIndex = 4;
            // 
            // l_activo15
            // 
            l_activo15.Anchor = AnchorStyles.Left;
            l_activo15.AutoSize = true;
            l_activo15.Font = new Font("Verdana", 11F, FontStyle.Regular, GraphicsUnit.Point, 0);
            l_activo15.Location = new Point(24, 584);
            l_activo15.Name = "l_activo15";
            l_activo15.Size = new Size(0, 26);
            l_activo15.TabIndex = 14;
            // 
            // l_activo14
            // 
            l_activo14.Anchor = AnchorStyles.Left;
            l_activo14.AutoSize = true;
            l_activo14.Font = new Font("Verdana", 11F, FontStyle.Regular, GraphicsUnit.Point, 0);
            l_activo14.Location = new Point(24, 540);
            l_activo14.Name = "l_activo14";
            l_activo14.Size = new Size(0, 26);
            l_activo14.TabIndex = 13;
            // 
            // l_activo13
            // 
            l_activo13.Anchor = AnchorStyles.Left;
            l_activo13.AutoSize = true;
            l_activo13.Location = new Point(24, 500);
            l_activo13.Name = "l_activo13";
            l_activo13.Size = new Size(0, 25);
            l_activo13.TabIndex = 12;
            // 
            // l_activo12
            // 
            l_activo12.Anchor = AnchorStyles.Left;
            l_activo12.AutoSize = true;
            l_activo12.Font = new Font("Verdana", 11F, FontStyle.Regular, GraphicsUnit.Point, 0);
            l_activo12.Location = new Point(24, 458);
            l_activo12.Name = "l_activo12";
            l_activo12.Size = new Size(0, 26);
            l_activo12.TabIndex = 11;
            // 
            // l_activo11
            // 
            l_activo11.Anchor = AnchorStyles.Left;
            l_activo11.AutoSize = true;
            l_activo11.Font = new Font("Verdana", 11F, FontStyle.Regular, GraphicsUnit.Point, 0);
            l_activo11.Location = new Point(24, 417);
            l_activo11.Name = "l_activo11";
            l_activo11.Size = new Size(0, 26);
            l_activo11.TabIndex = 10;
            // 
            // l_activo10
            // 
            l_activo10.Anchor = AnchorStyles.Left;
            l_activo10.AutoSize = true;
            l_activo10.Font = new Font("Verdana", 11F, FontStyle.Regular, GraphicsUnit.Point, 0);
            l_activo10.Location = new Point(24, 376);
            l_activo10.Name = "l_activo10";
            l_activo10.Size = new Size(0, 26);
            l_activo10.TabIndex = 9;
            // 
            // l_activo9
            // 
            l_activo9.Anchor = AnchorStyles.Left;
            l_activo9.AutoSize = true;
            l_activo9.Font = new Font("Verdana", 11F, FontStyle.Regular, GraphicsUnit.Point, 0);
            l_activo9.Location = new Point(24, 335);
            l_activo9.Name = "l_activo9";
            l_activo9.Size = new Size(0, 26);
            l_activo9.TabIndex = 8;
            // 
            // l_activo8
            // 
            l_activo8.Anchor = AnchorStyles.Left;
            l_activo8.AutoSize = true;
            l_activo8.Font = new Font("Verdana", 11F, FontStyle.Regular, GraphicsUnit.Point, 0);
            l_activo8.Location = new Point(24, 294);
            l_activo8.Name = "l_activo8";
            l_activo8.Size = new Size(0, 26);
            l_activo8.TabIndex = 7;
            // 
            // l_activo7
            // 
            l_activo7.Anchor = AnchorStyles.Left;
            l_activo7.AutoSize = true;
            l_activo7.Font = new Font("Verdana", 11F, FontStyle.Regular, GraphicsUnit.Point, 0);
            l_activo7.Location = new Point(24, 253);
            l_activo7.Name = "l_activo7";
            l_activo7.Size = new Size(0, 26);
            l_activo7.TabIndex = 6;
            // 
            // l_activo6
            // 
            l_activo6.Anchor = AnchorStyles.Left;
            l_activo6.AutoSize = true;
            l_activo6.Font = new Font("Verdana", 11F, FontStyle.Regular, GraphicsUnit.Point, 0);
            l_activo6.Location = new Point(24, 212);
            l_activo6.Name = "l_activo6";
            l_activo6.Size = new Size(0, 26);
            l_activo6.TabIndex = 5;
            // 
            // l_activo5
            // 
            l_activo5.Anchor = AnchorStyles.Left;
            l_activo5.AutoSize = true;
            l_activo5.Font = new Font("Verdana", 11F, FontStyle.Regular, GraphicsUnit.Point, 0);
            l_activo5.Location = new Point(24, 171);
            l_activo5.Name = "l_activo5";
            l_activo5.Size = new Size(0, 26);
            l_activo5.TabIndex = 4;
            // 
            // l_activo4
            // 
            l_activo4.Anchor = AnchorStyles.Left;
            l_activo4.AutoSize = true;
            l_activo4.Font = new Font("Verdana", 11F, FontStyle.Regular, GraphicsUnit.Point, 0);
            l_activo4.Location = new Point(24, 130);
            l_activo4.Name = "l_activo4";
            l_activo4.Size = new Size(0, 26);
            l_activo4.TabIndex = 3;
            // 
            // l_activo3
            // 
            l_activo3.Anchor = AnchorStyles.Left;
            l_activo3.AutoSize = true;
            l_activo3.Font = new Font("Verdana", 11F, FontStyle.Regular, GraphicsUnit.Point, 0);
            l_activo3.Location = new Point(24, 89);
            l_activo3.Name = "l_activo3";
            l_activo3.Size = new Size(0, 26);
            l_activo3.TabIndex = 2;
            // 
            // l_activo2
            // 
            l_activo2.Anchor = AnchorStyles.Left;
            l_activo2.AutoSize = true;
            l_activo2.Font = new Font("Verdana", 11F, FontStyle.Regular, GraphicsUnit.Point, 0);
            l_activo2.Location = new Point(24, 48);
            l_activo2.Name = "l_activo2";
            l_activo2.Size = new Size(0, 26);
            l_activo2.TabIndex = 1;
            // 
            // l_activo1
            // 
            l_activo1.Anchor = AnchorStyles.Left;
            l_activo1.AutoSize = true;
            l_activo1.Font = new Font("Verdana", 11F, FontStyle.Regular, GraphicsUnit.Point, 0);
            l_activo1.Location = new Point(24, 7);
            l_activo1.Name = "l_activo1";
            l_activo1.Size = new Size(0, 26);
            l_activo1.TabIndex = 0;
            // 
            // tableLayoutPanelMedioGeneral
            // 
            tableLayoutPanelMedioGeneral.BackColor = Color.RoyalBlue;
            tableLayoutPanelMedioGeneral.ColumnCount = 1;
            tableLayoutPanelMedioGeneral.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanelMedioGeneral.Controls.Add(p_titulo, 0, 0);
            tableLayoutPanelMedioGeneral.Controls.Add(tableLayoutPanelMedioAbajo, 0, 2);
            tableLayoutPanelMedioGeneral.Controls.Add(p_central, 0, 1);
            tableLayoutPanelMedioGeneral.Dock = DockStyle.Fill;
            tableLayoutPanelMedioGeneral.Location = new Point(282, 3);
            tableLayoutPanelMedioGeneral.Name = "tableLayoutPanelMedioGeneral";
            tableLayoutPanelMedioGeneral.RowCount = 3;
            tableLayoutPanelMedioGeneral.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            tableLayoutPanelMedioGeneral.RowStyles.Add(new RowStyle(SizeType.Percent, 85F));
            tableLayoutPanelMedioGeneral.RowStyles.Add(new RowStyle(SizeType.Percent, 5F));
            tableLayoutPanelMedioGeneral.Size = new Size(1298, 1044);
            tableLayoutPanelMedioGeneral.TabIndex = 3;
            // 
            // p_titulo
            // 
            p_titulo.BackColor = Color.RoyalBlue;
            p_titulo.Controls.Add(titulo);
            p_titulo.Dock = DockStyle.Fill;
            p_titulo.Location = new Point(3, 3);
            p_titulo.Name = "p_titulo";
            p_titulo.Size = new Size(1292, 98);
            p_titulo.TabIndex = 0;
            // 
            // titulo
            // 
            titulo.Dock = DockStyle.Fill;
            titulo.Font = new Font("Verdana", 28F, FontStyle.Bold, GraphicsUnit.Point, 0);
            titulo.Location = new Point(0, 0);
            titulo.Name = "titulo";
            titulo.Size = new Size(1292, 98);
            titulo.TabIndex = 2;
            titulo.Text = "TXATA ILE-APAINDEGIA";
            titulo.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanelMedioAbajo
            // 
            tableLayoutPanelMedioAbajo.ColumnCount = 2;
            tableLayoutPanelMedioAbajo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 90F));
            tableLayoutPanelMedioAbajo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10F));
            tableLayoutPanelMedioAbajo.Controls.Add(b_mandar, 1, 0);
            tableLayoutPanelMedioAbajo.Controls.Add(t_mensaje, 0, 0);
            tableLayoutPanelMedioAbajo.Dock = DockStyle.Fill;
            tableLayoutPanelMedioAbajo.Location = new Point(3, 994);
            tableLayoutPanelMedioAbajo.Name = "tableLayoutPanelMedioAbajo";
            tableLayoutPanelMedioAbajo.RowCount = 1;
            tableLayoutPanelMedioAbajo.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanelMedioAbajo.Size = new Size(1292, 47);
            tableLayoutPanelMedioAbajo.TabIndex = 2;
            // 
            // b_mandar
            // 
            b_mandar.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            b_mandar.BackColor = Color.Transparent;
            b_mandar.BackgroundImage = Properties.Resources.flecha_mandar;
            b_mandar.BackgroundImageLayout = ImageLayout.Zoom;
            b_mandar.Location = new Point(1165, 3);
            b_mandar.Name = "b_mandar";
            b_mandar.Size = new Size(124, 34);
            b_mandar.TabIndex = 1;
            b_mandar.UseVisualStyleBackColor = false;
            b_mandar.Click += b_mandar_Click;
            // 
            // t_mensaje
            // 
            t_mensaje.Dock = DockStyle.Fill;
            t_mensaje.Location = new Point(3, 3);
            t_mensaje.Name = "t_mensaje";
            t_mensaje.Size = new Size(1156, 31);
            t_mensaje.TabIndex = 0;
            // 
            // p_central
            // 
            p_central.AutoScroll = true;
            p_central.BackColor = Color.LightSkyBlue;
            p_central.Dock = DockStyle.Fill;
            p_central.FlowDirection = FlowDirection.TopDown;
            p_central.Location = new Point(3, 107);
            p_central.Name = "p_central";
            p_central.Size = new Size(1292, 881);
            p_central.TabIndex = 3;
            p_central.WrapContents = false;
            // 
            // tableLayoutPanelDerechaGeneral
            // 
            tableLayoutPanelDerechaGeneral.ColumnCount = 1;
            tableLayoutPanelDerechaGeneral.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanelDerechaGeneral.Controls.Add(logo_Santurtzi, 0, 0);
            tableLayoutPanelDerechaGeneral.Controls.Add(l_citas, 0, 1);
            tableLayoutPanelDerechaGeneral.Controls.Add(b_actualizar, 0, 3);
            tableLayoutPanelDerechaGeneral.Controls.Add(listBoxCitas, 0, 2);
            tableLayoutPanelDerechaGeneral.Dock = DockStyle.Fill;
            tableLayoutPanelDerechaGeneral.Location = new Point(1586, 3);
            tableLayoutPanelDerechaGeneral.Name = "tableLayoutPanelDerechaGeneral";
            tableLayoutPanelDerechaGeneral.RowCount = 4;
            tableLayoutPanelDerechaGeneral.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tableLayoutPanelDerechaGeneral.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            tableLayoutPanelDerechaGeneral.RowStyles.Add(new RowStyle(SizeType.Percent, 60F));
            tableLayoutPanelDerechaGeneral.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            tableLayoutPanelDerechaGeneral.Size = new Size(275, 1044);
            tableLayoutPanelDerechaGeneral.TabIndex = 4;
            // 
            // logo_Santurtzi
            // 
            logo_Santurtzi.BackColor = Color.Transparent;
            logo_Santurtzi.BackgroundImage = Properties.Resources.Logo_vertical;
            logo_Santurtzi.BackgroundImageLayout = ImageLayout.Zoom;
            logo_Santurtzi.Dock = DockStyle.Fill;
            logo_Santurtzi.Location = new Point(3, 3);
            logo_Santurtzi.Name = "logo_Santurtzi";
            logo_Santurtzi.Size = new Size(269, 202);
            logo_Santurtzi.TabIndex = 3;
            // 
            // l_citas
            // 
            l_citas.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            l_citas.AutoSize = true;
            l_citas.Font = new Font("Verdana", 16F, FontStyle.Bold);
            l_citas.Location = new Point(3, 208);
            l_citas.Name = "l_citas";
            l_citas.Size = new Size(269, 104);
            l_citas.TabIndex = 2;
            l_citas.Text = "HITZORDUAK:";
            l_citas.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // b_actualizar
            // 
            b_actualizar.BackColor = Color.PowderBlue;
            b_actualizar.Cursor = Cursors.Hand;
            b_actualizar.Dock = DockStyle.Fill;
            b_actualizar.Font = new Font("Verdana", 12F, FontStyle.Bold);
            b_actualizar.Location = new Point(3, 941);
            b_actualizar.Name = "b_actualizar";
            b_actualizar.Size = new Size(269, 100);
            b_actualizar.TabIndex = 4;
            b_actualizar.Text = "EGUNERATU";
            b_actualizar.UseVisualStyleBackColor = false;
            b_actualizar.Click += b_actualizar_Click;
            // 
            // listBoxCitas
            // 
            listBoxCitas.BackColor = Color.RoyalBlue;
            listBoxCitas.Dock = DockStyle.Fill;
            listBoxCitas.FormattingEnabled = true;
            listBoxCitas.ItemHeight = 25;
            listBoxCitas.Location = new Point(3, 315);
            listBoxCitas.Name = "listBoxCitas";
            listBoxCitas.Size = new Size(269, 620);
            listBoxCitas.TabIndex = 5;
            // 
            // Chat
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1864, 1050);
            Controls.Add(tableLayoutPanelPrincipal);
            Name = "Chat";
            Text = "Fp Santurtzi ile-apaindegi txata";
            tableLayoutPanelPrincipal.ResumeLayout(false);
            tableLayoutPanelIzquierdaGeneral.ResumeLayout(false);
            tableLayoutPanelIzquierdaGeneral.PerformLayout();
            tableLayoutPanelActivos.ResumeLayout(false);
            tableLayoutPanelActivos.PerformLayout();
            tableLayoutPanelMedioGeneral.ResumeLayout(false);
            p_titulo.ResumeLayout(false);
            tableLayoutPanelMedioAbajo.ResumeLayout(false);
            tableLayoutPanelMedioAbajo.PerformLayout();
            tableLayoutPanelDerechaGeneral.ResumeLayout(false);
            tableLayoutPanelDerechaGeneral.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanelPrincipal;
        private TableLayoutPanel tableLayoutPanelIzquierdaGeneral;
        private Panel logo_Hilea;
        private Label Activos;
        private TableLayoutPanel tableLayoutPanelMedioGeneral;
        private Panel p_titulo;
        private Label titulo;
        private TableLayoutPanel tableLayoutPanelDerechaGeneral;
        private Panel logo_Santurtzi;
        private Label l_citas;
        private Button b_actualizar;
        private TableLayoutPanel tableLayoutPanelMedioAbajo;
        private TextBox t_mensaje;
        private Button b_mandar;
        private Button b_desconectar;
        private TableLayoutPanel tableLayoutPanelActivos;
        private Label l_activo15;
        private Label l_activo14;
        private Label l_activo13;
        private Label l_activo12;
        private Label l_activo11;
        private Label l_activo10;
        private Label l_activo9;
        private Label l_activo8;
        private Label l_activo7;
        private Label l_activo6;
        private Label l_activo5;
        private Label l_activo4;
        private Label l_activo3;
        private Label l_activo2;
        private Label l_activo1;
        private FlowLayoutPanel p_central;
        private ListBox listBoxCitas;
    }
}