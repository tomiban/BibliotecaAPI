using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlumnosApi.DTO
{
    public class CreateComentarioDTO
    {
        public string? Contenido { get; set; }
        public bool Recomendar { get; set; }    
        public int LibroId { get; set; }
    }
}
