using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AlumnosApi.DAL.DataContext;
using AlumnosApi.DAL.Interfaces;
using AlumnosApi.DTO;
using AlumnosApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace AlumnosApi.DAL.Implementaciones
{
    public class UsuarioRepository : GenericRepository<Usuario>, IUsuarioRepository
    {
        private readonly ApplicationDbContext _context;
        private string _secretKey;

        public UsuarioRepository(ApplicationDbContext context, IConfiguration config)
            : base(context)
        {
            _context = context;
            _secretKey = config.GetValue<string>("ApiSettings:SECRET_KEY");
        }

        public async Task<bool> IsUnique(string usuario)
        {
            var usuarioDb = await _context.Usuarios.FirstOrDefaultAsync(u =>
                u.NombreUsuario == usuario
            );
            if (usuarioDb == null)
                return true;
            return false;
        }

        public async Task<UsuarioLoginRespuestaDTO> Login(UsuarioLoginDTO usuario)
        {
            var passwordEncriptado = ObtenerMD5(usuario.Password);

            //CAPA DE SERVICIO
            var usuarioEncontrado = await _context.Usuarios.FirstOrDefaultAsync(u =>
                u.NombreUsuario.ToLower() == usuario.NombreUsuario
                && u.Password == passwordEncriptado
            );

            if (usuarioEncontrado == null)
            {
                return new UsuarioLoginRespuestaDTO() { Token = "", Usuario = null! };
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim(ClaimTypes.Name, usuarioEncontrado.NombreUsuario.ToString()),
                        new Claim(ClaimTypes.Role, usuarioEncontrado.Role),
                    }
                ),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            UsuarioLoginRespuestaDTO usuarioLoginRespuestaDTO = new UsuarioLoginRespuestaDTO()
            {
                Token = tokenHandler.WriteToken(token),
                Usuario = usuarioEncontrado
            };

            return usuarioLoginRespuestaDTO;
        }

        public async Task<Usuario> Registro(UsuarioRegistroDTO usuario)
        {
            var passwordEncriptado = ObtenerMD5(usuario.Password);
            var usuarioNuevo = new Usuario()
            {
                NombreUsuario = usuario.NombreUsuario.ToLower(),
                Nombre = usuario.Nombre,
                Password = passwordEncriptado,
                Role = usuario.Role
            };
            await _context.Usuarios.AddAsync(usuarioNuevo);
            await _context.SaveChangesAsync();
            return usuarioNuevo;
        }

        public static string ObtenerMD5(string valor)
        {
            MD5CryptoServiceProvider x = new MD5CryptoServiceProvider();
            byte[] data = Encoding.UTF8.GetBytes(valor);
            data = x.ComputeHash(data);
            string respuesta = "";
            for (int i = 0; i < data.Length; i++)
            {
                respuesta += data[i].ToString("x2").ToLower();
            }
            return respuesta;
        }
    }
}
