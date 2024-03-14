using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlumnosApi.DAL.Interfaces;
using AlumnosApi.DTO;
using AlumnosApi.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AlumnosApi.Controller
{
    [ApiController]
    [Route("api/comentarios")]
    public class ComentarioController : ControllerBase
    {
        private readonly IGenericRepository<Comentario> _repository;
        private readonly IMapper _mapper;

        public ComentarioController(IGenericRepository<Comentario> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GeneroDTO>>> GetAllGenero()
        {
            var comentario = await _repository.GetAll();
            var comentarioDto = _mapper.Map<IEnumerable<ComentarioDTO>>(comentario);
            return Ok(comentarioDto);
        }

        [HttpGet("{comentarioId}")]
        public async Task<ActionResult<ComentarioDTO>> GetComentario(int comentarioId)
        {
            var comentario = await _repository.GetById(comentarioId);
            if (comentario == null)
                return NotFound();

            var comentarioDTO = _mapper.Map<ComentarioDTO>(comentario);
            return Ok(comentarioDTO);
        }

        [HttpPost]
        public async Task<ActionResult> CreateComentario(CreateComentarioDTO createComentarioDTO)
        {
            var comentario = _mapper.Map<Comentario>(createComentarioDTO);
            await _repository.Create(comentario);
            var comentarioDto = _mapper.Map<ComentarioDTO>(comentario);
            return CreatedAtAction(nameof(GetComentario), new {comentarioId = comentario.Id}, comentarioDto);
        }

        [HttpPut("{comentarioId}")]
        public async Task<ActionResult> UpdateComentario(
            int comentarioId,
            CreateComentarioDTO createComentarioDto
        )
        {
            var comentario = await _repository.GetById(comentarioId);
            if (comentario == null)
                return NotFound("");
            var comentarioToUpdate = _mapper.Map(createComentarioDto, comentario);
            await _repository.Update(comentarioToUpdate);
            return NoContent();
        }

        [HttpDelete("{comentarioId}")]
        public async Task<ActionResult> DeleteComentario(int comentarioId)
        {
            var comentario = await _repository.GetById(comentarioId);
            if (comentario == null)
                return NotFound();
            await _repository.Delete(comentarioId);
            return NoContent();
        }
    }
}
