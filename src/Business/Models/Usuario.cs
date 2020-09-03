using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Models
{
    /// <summary>
    /// Usario
    /// </summary>
    public class Usuario
    {
        /// <summary>
        /// ID do usuario
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// E-mail do Usuario
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Senha do Usuario
        /// </summary>
        public string Password { get; set; }

        public Usuario()
        {
            Id = Guid.NewGuid();
        }
    }
}
