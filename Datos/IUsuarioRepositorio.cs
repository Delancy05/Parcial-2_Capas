using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    /// <summary>Contrato de acceso a datos de usuarios.</summary>
    public interface IUsuarioRepositorio
    {
        Usuario ObtenerPorCorreo(string correo);
        bool Registrar(Usuario usuario);
    }
}