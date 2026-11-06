using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    /// <summary>
    /// Representa a un usuario del sistema.
    /// </summary>
    public class Usuario
    {
        public int Id { get; set; }
        public string Correo { get; set; }
        public string PasswordHash { get; set; }
    }
}
