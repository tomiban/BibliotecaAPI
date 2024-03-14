using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlumnosApi.DTO
{
    public class CreateLibroDTO
    {
        public string Titulo { get; set; } = null!;
        public bool ParaPrestar { get; set; }
        public string FechaLanzamiento { get; set; }
        public int AutorId { get; set; }
        public int GeneroId { get; set; }
    }
}
