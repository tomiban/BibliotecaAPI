using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlumnosApi.Models;

namespace AlumnosApi.DTO
{
    public class GeneroDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public HashSet<GeneroLibroDTO> Libros { get; set; } = new HashSet<GeneroLibroDTO>();
    }
}
