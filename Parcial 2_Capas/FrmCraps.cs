using System;
using System.Drawing;
using System.Windows.Forms;

namespace Parcial_2_Capas
{
    /// <summary>Vista del juego de Craps: solo muestra datos y avisa al presentador.</summary>
    public partial class FrmCraps : Form, ICrapsView
    {
        public event EventHandler LanzarClick;
        public event EventHandler ReiniciarClick;

        private Label lblDado1, lblDado2, lblPunto, lblResultado;
        private ListBox lstHistorial;

        public FrmCraps()
        {
            InitializeComponent();
            Text = "Juego de Craps";
            Size = new Size(520, 480);
            ConstruirControles();
            new CrapsPresenter(this);
        }

        private void ConstruirControles()
        {
            lblDado1 = CrearDado(40);
            lblDado2 = CrearDado(160);

            lblPunto = new Label { Left = 290, Top = 40, Width = 200, Font = new Font("Segoe UI", 12), Text = "Su punto es: -" };
            lblResultado = new Label { Left = 40, Top = 150, Width = 440, Height = 50, Font = new Font("Segoe UI", 12, FontStyle.Bold), Text = "Presione Lanzar." };

            var btnLanzar = new Button { Text = "Lanzar dados", Left = 40, Top = 210, Width = 140, Height = 40 };
            btnLanzar.Click += (s, e) => LanzarClick?.Invoke(this, EventArgs.Empty);

            var btnReiniciar = new Button { Text = "Reiniciar", Left = 200, Top = 210, Width = 140, Height = 40 };
            btnReiniciar.Click += (s, e) => ReiniciarClick?.Invoke(this, EventArgs.Empty);

            lstHistorial = new ListBox { Left = 40, Top = 270, Width = 440, Height = 140 };

            Controls.AddRange(new Control[] { lblDado1, lblDado2, lblPunto, lblResultado, btnLanzar, btnReiniciar, lstHistorial });
        }

        private Label CrearDado(int izquierda)
        {
            return new Label
            {
                Left = izquierda,
                Top = 30,
                Width = 100,
                Height = 100,
                BorderStyle = BorderStyle.FixedSingle,
                Font = new Font("Segoe UI", 40, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                Text = "?"
            };
        }

        public void MostrarDados(int dado1, int dado2)
        {
            lblDado1.Text = dado1.ToString();
            lblDado2.Text = dado2.ToString();
        }

        public void MostrarResultado(string mensaje) { lblResultado.Text = mensaje; }
        public void MostrarPunto(string texto) { lblPunto.Text = texto; }
        public void MostrarHistorial(string linea) { lstHistorial.Items.Insert(0, linea); }
    }
}