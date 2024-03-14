using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlumnosApi.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string NombreUsuario { get; set; } = String.Empty;
        public string Nombre { get; set; } = String.Empty;
        public string Password { get; set; } = String.Empty;
        public string Role { get; set; } = String.Empty;
    }
}
