using System;
using System.Text.RegularExpressions;
using Datos;
using Entidades;
using Negocio;

namespace Parcial_2_Capas
{
    /// <summary>Presentador del login: valida, consulta y decide qué mostrar.</summary>
    public class LoginPresenter
    {
        private readonly ILoginView _vista;
        private readonly IHashService _hash;
        private readonly IVerificarHashService _verificador;
        private readonly IUsuarioRepositorio _repo;

        public LoginPresenter(ILoginView vista, IHashService hash,
            IVerificarHashService verificador, IUsuarioRepositorio repo)
        {
            _vista = vista;
            _hash = hash;
            _verificador = verificador;
            _repo = repo;

            _vista.LoginClick += (s, e) => Login();
            _vista.RegistrarClick += (s, e) => Registrar();
        }

        private bool Validar()
        {
            if (string.IsNullOrWhiteSpace(_vista.Correo) || string.IsNullOrWhiteSpace(_vista.Password))
            {
                _vista.MostrarMensaje("Debe completar el correo y la contraseña.");
                return false;
            }
            if (!Regex.IsMatch(_vista.Correo, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                _vista.MostrarMensaje("El correo no tiene un formato válido.");
                return false;
            }
            if (_vista.Password.Length < 6)
            {
                _vista.MostrarMensaje("La contraseña debe tener al menos 6 caracteres.");
                return false;
            }
            return true;
        }

        private void Login()
        {
            if (!Validar()) return;
            try
            {
                Usuario u = _repo.ObtenerPorCorreo(_vista.Correo);
                if (u != null && _verificador.Verificar(_vista.Password, u.PasswordHash))
                    _vista.AbrirPrincipal(u.Correo);
                else
                    _vista.MostrarMensaje("Correo o contraseña incorrectos.");
            }
            catch (Exception ex)
            {
                _vista.MostrarMensaje("Error de conexión: " + ex.Message);
            }
        }

        private void Registrar()
        {
            if (!Validar()) return;
            try
            {
                if (_repo.ObtenerPorCorreo(_vista.Correo) != null)
                {
                    _vista.MostrarMensaje("Ese correo ya está registrado.");
                    return;
                }
                var nuevo = new Usuario
                {
                    Correo = _vista.Correo,
                    PasswordHash = _hash.GenerarHash(_vista.Password)
                };
                _vista.MostrarMensaje(_repo.Registrar(nuevo)
                    ? "Usuario registrado. Ya puede iniciar sesión."
                    : "No se pudo registrar el usuario.");
            }
            catch (Exception ex)
            {
                _vista.MostrarMensaje("Error de conexión: " + ex.Message);
            }
        }
    }
}
