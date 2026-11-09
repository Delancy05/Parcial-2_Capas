using System;

namespace Parcial_2_Capas
{
    /// <summary>Contrato que debe cumplir el formulario del juego de Craps (Vista).</summary>
    public interface ICrapsView
    {
        void MostrarDados(int dado1, int dado2);
        void MostrarResultado(string mensaje);
        void MostrarPunto(string texto);
        void MostrarHistorial(string linea);

        event EventHandler LanzarClick;
        event EventHandler ReiniciarClick;
    }
}