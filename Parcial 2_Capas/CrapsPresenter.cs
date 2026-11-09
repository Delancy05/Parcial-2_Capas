using System;

namespace Parcial_2_Capas
{
    /// <summary>Presentador de Craps: contiene las reglas del juego.</summary>
    public class CrapsPresenter
    {
        private readonly ICrapsView _vista;
        private readonly Random _azar = new Random();
        private int _punto = 0; // 0 = todavía no hay punto

        public CrapsPresenter(ICrapsView vista)
        {
            _vista = vista;
            _vista.LanzarClick += (s, e) => Lanzar();
            _vista.ReiniciarClick += (s, e) => Reiniciar();
        }

        private void Lanzar()
        {
            int d1 = _azar.Next(1, 7);
            int d2 = _azar.Next(1, 7);
            int suma = d1 + d2;
            _vista.MostrarDados(d1, d2);

            if (_punto == 0)
            {
                if (suma == 7 || suma == 11)
                    Terminar("Sacó " + suma + ": ¡GANÓ!");
                else if (suma == 2 || suma == 3 || suma == 12)
                    Terminar("Sacó " + suma + ": PERDIÓ (craps).");
                else
                {
                    _punto = suma;
                    _vista.MostrarPunto("Su punto es: " + _punto);
                    _vista.MostrarResultado("Sacó " + suma + ". Siga lanzando.");
                    _vista.MostrarHistorial("Lanzamiento: " + suma + " (se establece el punto)");
                }
            }
            else
            {
                if (suma == _punto)
                    Terminar("Sacó " + suma + ": hizo su punto. ¡GANÓ!");
                else if (suma == 7)
                    Terminar("Sacó 7: PERDIÓ.");
                else
                {
                    _vista.MostrarResultado("Sacó " + suma + ". Siga lanzando.");
                    _vista.MostrarHistorial("Lanzamiento: " + suma);
                }
            }
        }

        private void Terminar(string mensaje)
        {
            _vista.MostrarResultado(mensaje);
            _vista.MostrarHistorial(mensaje);
            _vista.MostrarPunto("Su punto es: -");
            _punto = 0;
        }

        private void Reiniciar()
        {
            _punto = 0;
            _vista.MostrarPunto("Su punto es: -");
            _vista.MostrarResultado("Nueva partida. Presione Lanzar.");
        }
    }
}