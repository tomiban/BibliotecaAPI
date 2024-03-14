using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlumnosApi.DTO
{
    public class CreateAutorDTO
    {
        public string Nombre { get; set; } = String.Empty;
        public string FechaNacimiento { get; set; } = String.Empty;
    }
}
