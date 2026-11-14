using System;
using System.Windows.Forms;
using Datos;
using Negocio;

namespace Parcial_2_Capas
{
    /// <summary>Vista del registro de usuarios: solo muestra controles y avisa al presentador.</summary>
    public partial class FrmRegistro : Form, IRegistroView
    {
        public event EventHandler RegistrarClick;

        private TextBox txtCorreo, txtPassword, txtConfirmar;

        public string Correo { get { return txtCorreo.Text.Trim(); } }
        public string Password { get { return txtPassword.Text; } }
        public string ConfirmarPassword { get { return txtConfirmar.Text; } }

        public FrmRegistro()
        {
            Text = "Registrar usuario";
            ClientSize = new System.Drawing.Size(380, 280);
            ConstruirControles();

            new RegistroPresenter(this, new BcryptPasswordService(), new UsuarioRepositorio());
        }

        private void ConstruirControles()
        {
            Controls.Add(new Label { Text = "Correo:", Left = 30, Top = 30, Width = 120 });
            txtCorreo = new TextBox { Left = 150, Top = 27, Width = 190 };

            Controls.Add(new Label { Text = "Contraseña:", Left = 30, Top = 80, Width = 120 });
            txtPassword = new TextBox { Left = 150, Top = 77, Width = 190, PasswordChar = '*' };

            Controls.Add(new Label { Text = "Confirmar contraseña:", Left = 30, Top = 130, Width = 120 });
            txtConfirmar = new TextBox { Left = 150, Top = 127, Width = 190, PasswordChar = '*' };

            var btnRegistrar = new Button { Text = "Registrar", Left = 150, Top = 190, Width = 190, Height = 40 };
            btnRegistrar.Click += (s, e) => RegistrarClick?.Invoke(this, EventArgs.Empty);

            Controls.AddRange(new Control[] { txtCorreo, txtPassword, txtConfirmar, btnRegistrar });
        }

        public void MostrarMensaje(string mensaje)
        {
            MessageBox.Show(mensaje, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void LimpiarCampos()
        {
            txtCorreo.Clear();
            txtPassword.Clear();
            txtConfirmar.Clear();
        }
    }
}