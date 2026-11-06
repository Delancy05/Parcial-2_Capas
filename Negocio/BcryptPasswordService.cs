using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    /// <summary>Implementa ambos contratos usando BCrypt.</summary>
    public class BcryptPasswordService : IHashService, IVerificarHashService
    {
        public string GenerarHash(string textoPlano)
        {
            return BCrypt.Net.BCrypt.HashPassword(textoPlano);
        }

        public bool Verificar(string textoPlano, string hashAlmacenado)
        {
            return BCrypt.Net.BCrypt.Verify(textoPlano, hashAlmacenado);
        }
    }
}