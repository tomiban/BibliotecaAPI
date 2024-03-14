using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AlumnosApi.Models
{
    public class Libro
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = null!;
        public bool ParaPrestar { get; set; }
        public DateTime FechaLanzamiento { get; set; }
        public int AutorId { get; set; }
        public Autor Autor { get; set; } = null!;
         public int GeneroId { get; set; }
        public Genero Genero { get; set; } = null!; 
        [JsonIgnore]
        public HashSet<Comentario> Comentarios { get; set; } = new HashSet<Comentario>();
    }
}
