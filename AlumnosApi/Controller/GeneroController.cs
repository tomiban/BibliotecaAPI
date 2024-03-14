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
    [Route("api/generos")]
    public class GeneroController : ControllerBase
    {
        private readonly IGenericRepository<Genero> _repository;
        private readonly IGeneroRepository _generoRepository;
        private readonly IMapper _mapper;

        public GeneroController(
            IGenericRepository<Genero> repository,
            IGeneroRepository genero,
            IMapper mapper
        )
        {
            _repository = repository;
            _generoRepository = genero;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Genero>>> GetAllGeneros()
        {
            var generos = await _generoRepository.GetAllWithRelation();
            var generosDto = _mapper.Map<IEnumerable<GeneroDTO>>(generos);
            return Ok(generosDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Genero>> GetGenero(int id)
        {
            var genero = await _generoRepository.GetByIdWithRelation(id);
        
            if (genero == null)
                return NotFound();
            var generoDto = _mapper.Map<GeneroDTO>(genero);
            return Ok(generoDto);
        }

        [HttpPost]
        public async Task<ActionResult> CreateGenero(CreateGeneroDTO createGeneroDTO)
        {
            var genero = _mapper.Map<Genero>(createGeneroDTO);
            await _repository.Create(genero);
            var generoDto = _mapper.Map<GeneroDTO>(genero);
            return CreatedAtAction(nameof(GetGenero), new { id = genero.Id }, generoDto);
        }

        [HttpPut("{generoId}")]
        public async Task<ActionResult<GeneroDTO>> UpdateGenero(
            int generoId,
            [FromBody] CreateGeneroDTO createGeneroDTO
        )
        {
            var genero = await _repository.GetById(generoId);

            if (genero == null)
                return NotFound();

            var generoToUpdate = _mapper.Map(createGeneroDTO, genero);

            var resultado = await _repository.Update(generoToUpdate);

            if (resultado)
                return NoContent();

            return BadRequest();
        }

        [HttpDelete("{generoId}")]
        public async Task<IActionResult> DeleteGenero(int generoId)
        {
            var genero = await _repository.GetById(generoId);

            if (genero == null)
                return NotFound();

            var result = await _repository.Delete(generoId);

            if (result)
                return NoContent();

            return BadRequest();
        }
    }
}
