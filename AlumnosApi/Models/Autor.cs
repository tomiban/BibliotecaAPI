using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlumnosApi.Models
{
    public class Autor
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = String.Empty;
        public DateTime FechaNacimiento { get; set; }
        public HashSet<Libro> Libros { get; set; } = new HashSet<Libro>();
    }
}
