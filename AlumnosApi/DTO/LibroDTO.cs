using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlumnosApi.Models;

namespace AlumnosApi.DTO
{
    public class LibroDTO
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = null!;
        public bool ParaPrestar { get; set; }
        public string FechaLanzamiento { get; set; }
        public string NombreAutor { get; set; } = null!;
        public string NombreGenero { get; set; } = null!;
        public HashSet<ComentarioDTO> Comentarios { get; set; } = new HashSet<ComentarioDTO>();
    }
}
