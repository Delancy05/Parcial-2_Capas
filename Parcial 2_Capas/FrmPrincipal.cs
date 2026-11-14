using System;
using System.Drawing;
using System.Windows.Forms;

namespace Parcial_2_Capas
{
    /// <summary>Formulario MDI con menú lateral, hora y usuario en sesión.</summary>
    public partial class FrmPrincipal : Form
    {
        private readonly string _usuario;
        private Label lblHora;
        private Timer timer;

        public FrmPrincipal(string usuario)
        {
            InitializeComponent();
            _usuario = usuario;
            ConstruirMenu();
        }

        private void ConstruirMenu()
        {
            // Panel lateral izquierdo
            var panel = new Panel
            {
                Dock = DockStyle.Left,
                Width = 200,
                BackColor = Color.FromArgb(0, 102, 204)
            };

            // Botones del menú (se agregan de abajo hacia arriba por el Dock)
            var btnSalir = CrearBoton("Salir");
            btnSalir.Click += (s, e) => Application.Exit();

            var btnCerrarSesion = CrearBoton("Cerrar sesión");
            btnCerrarSesion.Click += (s, e) => Application.Restart();

            var btnRegistrar = CrearBoton("Registrar usuario");
            btnRegistrar.Click += (s, e) => AbrirHijo(new FrmRegistro());

            // Usuario en sesión, parte inferior del menú
            var lblUsuario = new Label
            {
                Dock = DockStyle.Bottom,
                Height = 60,
                ForeColor = Color.White,
                TextAlign = ContentAlignment.MiddleCenter,
                Text = "Sesión:" + Environment.NewLine + _usuario
            };

            // Hora actual
            lblHora = new Label
            {
                Dock = DockStyle.Top,
                Height = 50,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                Text = DateTime.Now.ToString("HH:mm:ss")
            };

            panel.Controls.Add(btnSalir);
            panel.Controls.Add(btnCerrarSesion);
            panel.Controls.Add(btnRegistrar);
            panel.Controls.Add(lblHora);
            panel.Controls.Add(lblUsuario);
            Controls.Add(panel);

            timer = new Timer { Interval = 1000 };
            timer.Tick += (s, e) => lblHora.Text = DateTime.Now.ToString("HH:mm:ss");
            timer.Start();
        }

        private Button CrearBoton(string texto)
        {
            return new Button
            {
                Text = texto,
                Dock = DockStyle.Top,
                Height = 50,
                FlatStyle = FlatStyle.Flat,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 11)
            };
        }

        private void AbrirHijo(Form hijo)
        {
            foreach (Form f in MdiChildren) f.Close();
            hijo.MdiParent = this;
            hijo.Show();
        }
    }
}