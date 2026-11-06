using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    /// <summary>Contrato con la única responsabilidad de verificar un hash.</summary>
    public interface IVerificarHashService
    {
        bool Verificar(string textoPlano, string hashAlmacenado);
    }
}
