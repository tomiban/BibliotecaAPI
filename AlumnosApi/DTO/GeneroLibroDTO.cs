using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlumnosApi.DTO
{
    public class GeneroLibroDTO
    {
        public string Titulo { get; set; } = null!;
        public bool ParaPrestar { get; set; }
        public string FechaLanzamiento { get; set; }
        public string NombreAutor { get; set; } = null!;
    }
}
