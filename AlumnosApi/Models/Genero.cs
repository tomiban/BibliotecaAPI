using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlumnosApi.Models
{
    public class Genero
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public HashSet<Libro> Libros { get; set; } = new HashSet<Libro>();
    }
}
