using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlumnosApi.Models;

namespace AlumnosApi.DTO
{
    public class UsuarioLoginRespuestaDTO
    {
        public Usuario Usuario { get; set; }
        public string  Token { get; set; }
    }
}