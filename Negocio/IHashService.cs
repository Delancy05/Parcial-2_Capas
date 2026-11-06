using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    /// <summary>Contrato con la única responsabilidad de generar el hash.</summary>
    public interface IHashService
    {
        string GenerarHash(string textoPlano);
    }
}
