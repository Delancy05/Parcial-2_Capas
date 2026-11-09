using System;

namespace Parcial_2_Capas
{
    /// <summary>Contrato que debe cumplir el formulario de login (Vista).</summary>
    public interface ILoginView
    {
        string Correo { get; }
        string Password { get; }
        void MostrarMensaje(string mensaje);
        void AbrirPrincipal(string correoUsuario);

        event EventHandler LoginClick;
        event EventHandler RegistrarClick;
    }
}
