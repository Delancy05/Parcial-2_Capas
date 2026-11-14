using System;

namespace Parcial_2_Capas
{
    /// <summary>Contrato que debe cumplir el formulario de registro (Vista).</summary>
    public interface IRegistroView
    {
        string Correo { get; }
        string Password { get; }
        string ConfirmarPassword { get; }
        void MostrarMensaje(string mensaje);
        void LimpiarCampos();

        event EventHandler RegistrarClick;
    }
}