using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AlumnosApi.DTO
{
    public class UsuarioRegistroDTO
    {
        [Required(ErrorMessage = "El usuario es obligatorio")]
        public string NombreUsuario { get; set; } = null!;

        public string Nombre { get; set; } = null!;

        [Required(ErrorMessage = "La contrasela es obligatoria")]
        public string Password { get; set; } = null!;
        public string Role { get; set; } = null!;
    }
}
