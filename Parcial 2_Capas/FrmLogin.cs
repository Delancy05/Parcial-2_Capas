using Datos;
using Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Parcial_2_Capas
{
    public partial class FrmLogin : Form, ILoginView
    {
        public event EventHandler LoginClick;
        public event EventHandler RegistrarClick;

        public string Correo { get { return txtCorreo.Text.Trim(); } }
        public string Password { get { return txtPassword.Text; } }

        public FrmLogin()
        {
            InitializeComponent();

            var servicio = new BcryptPasswordService();
            new LoginPresenter(this, servicio, servicio, new UsuarioRepositorio());

            btnLogin.Click += (s, e) => LoginClick?.Invoke(this, EventArgs.Empty);
            btnRegistrar.Click += (s, e) => RegistrarClick?.Invoke(this, EventArgs.Empty);
        }

        public void MostrarMensaje(string mensaje)
        {
            MessageBox.Show(mensaje, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void AbrirPrincipal(string correoUsuario)
        {
            var principal = new FrmPrincipal(correoUsuario);
            principal.FormClosed += (s, e) => Close();
            Hide();
            principal.Show();
        }
    }
}
