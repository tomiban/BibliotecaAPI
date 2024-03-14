using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlumnosApi.DTO
{
    public class AutorDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = String.Empty;
        public string FechaNacimiento { get; set; } = String.Empty;
    }
}
