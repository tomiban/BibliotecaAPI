using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlumnosApi.DTO;
using AlumnosApi.Models;

namespace AlumnosApi.DAL.Interfaces
{
    public interface IUsuarioRepository : IGenericRepository<Usuario>
    {
        Task<bool> IsUnique(string usuario);
        Task<Usuario> Registro(UsuarioRegistroDTO usuario);
        Task<UsuarioLoginRespuestaDTO> Login(UsuarioLoginDTO usuario);

    }
}