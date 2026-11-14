using System;
using System.Text.RegularExpressions;
using Datos;
using Entidades;
using Negocio;

namespace Parcial_2_Capas
{
    /// <summary>Presentador del registro: valida los datos y guarda el usuario con su hash.</summary>
    public class RegistroPresenter
    {
        private readonly IRegistroView _vista;
        private readonly IHashService _hash;
        private readonly IUsuarioRepositorio _repo;

        public RegistroPresenter(IRegistroView vista, IHashService hash, IUsuarioRepositorio repo)
        {
            _vista = vista;
            _hash = hash;
            _repo = repo;
            _vista.RegistrarClick += (s, e) => Registrar();
        }

        private void Registrar()
        {
            if (string.IsNullOrWhiteSpace(_vista.Correo) || string.IsNullOrWhiteSpace(_vista.Password))
            {
                _vista.MostrarMensaje("Debe completar todos los campos.");
                return;
            }
            if (!Regex.IsMatch(_vista.Correo, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                _vista.MostrarMensaje("El correo no tiene un formato válido.");
                return;
            }
            if (_vista.Password.Length < 6)
            {
                _vista.MostrarMensaje("La contraseña debe tener al menos 6 caracteres.");
                return;
            }
            if (_vista.Password != _vista.ConfirmarPassword)
            {
                _vista.MostrarMensaje("Las contraseñas no coinciden.");
                return;
            }

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

                if (_repo.Registrar(nuevo))
                {
                    _vista.MostrarMensaje("Usuario registrado correctamente.");
                    _vista.LimpiarCampos();
                }
                else
                {
                    _vista.MostrarMensaje("No se pudo registrar el usuario.");
                }
            }
            catch (Exception ex)
            {
                _vista.MostrarMensaje("Error de conexión: " + ex.Message);
            }
        }
    }
}