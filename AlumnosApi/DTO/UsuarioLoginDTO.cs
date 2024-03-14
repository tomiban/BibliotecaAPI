using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AlumnosApi.DTO
{
    public class UsuarioLoginDTO
    {
        [Required(ErrorMessage = "El usuario es obligatorio")]
        public string NombreUsuario { get; set; } = null!;

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        public string Password { get; set; } = null!;
    }
}
