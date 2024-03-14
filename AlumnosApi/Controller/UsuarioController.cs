using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AlumnosApi.DAL.Interfaces;
using AlumnosApi.DTO;
using AlumnosApi.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AlumnosApi.Controller
{
    [ApiController]
    [Route("api/usuarios")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepository _repository;
        private readonly IMapper _mapper;
        private ApiResponse _response;

        public UsuarioController(IUsuarioRepository repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
            this._response = new ApiResponse();
         
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioDTO>>> GetUsuarios()
        {
            var usuarios = await _repository.GetAll();
            var usuariosDTO = _mapper.Map<IEnumerable<UsuarioDTO>>(usuarios);
            return Ok(usuariosDTO);
        }

        [HttpGet("{usuarioId}")]
        public async Task<ActionResult<UsuarioDTO>> GetUsuario(int usuarioId)
        {
            var usuario = await _repository.GetById(usuarioId);
            if (usuario == null)
                return NotFound();
            var usuarioDTO = _mapper.Map<UsuarioDTO>(usuario);
            return Ok(usuarioDTO);
        }

        [HttpPost("registro")]
        public async Task<ActionResult<UsuarioDTO>> Register(UsuarioRegistroDTO usuarioRegistroDTO)
        {
            var validacionNombre = await _repository.IsUnique(usuarioRegistroDTO.NombreUsuario);
            if(!validacionNombre)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add("El nombre de usuario ya existe");
                return BadRequest(_response);
            }
            var usuario = await _repository.Registro(usuarioRegistroDTO);
             if(usuario == null)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add("Error en el registro");
                return BadRequest(_response);
            }
            var usuarioDTO = _mapper.Map<UsuarioDTO>(usuario);
            _response.StatusCode = HttpStatusCode.OK;
            _response.IsSuccess = true;
            _response.Result = usuarioDTO;

            return Ok(_response);
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(UsuarioLoginDTO usuarioLoginDTO)
        {
            var respuestaLogin = await _repository.Login(usuarioLoginDTO);
            if(respuestaLogin.Usuario == null || string.IsNullOrEmpty(respuestaLogin.Token))
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add("El nombre de usuario o el password es incorrecto");
                return BadRequest(_response);
            }
            _response.StatusCode = HttpStatusCode.OK;
            _response.IsSuccess = true;
            _response.Result = respuestaLogin;
            return Ok(_response);
        }
    }
}
