using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlumnosApi.DAL.Interfaces;
using AlumnosApi.DTO;
using AlumnosApi.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AlumnosApi.Controller
{
    [ApiController]
    [Route("api/autores")]
    public class AutorController : ControllerBase
    {
        private readonly IGenericRepository<Autor> _repository;
        private readonly IMapper _mapper;

        public AutorController(IGenericRepository<Autor> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [Authorize(Roles = "ADMIN")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AutorDTO>>> GetAll()
        {
            var autores = await _repository.GetAll();
            var autoresDTO = _mapper.Map<IEnumerable<AutorDTO>>(autores);
            return Ok(autoresDTO);
        }

        [HttpGet("{autorId}")]
        public async Task<ActionResult> GetAutor(int autorId)
        {
            var autor = await _repository.GetById(autorId);

            if (autor == null)
                return NotFound();

            var autorDto = _mapper.Map<AutorDTO>(autor);

            return Ok(autorDto);
        }

        [HttpPost]
        public async Task<ActionResult> CreateAutor(CreateAutorDTO autorCreacionDTO)
        {
            var autor = _mapper.Map<Autor>(autorCreacionDTO);

            var resultado = await _repository.Create(autor);
            if (!resultado)
            {
                return NotFound();
            }
            var autorDto = _mapper.Map<AutorDTO>(autor);

            return CreatedAtAction(nameof(GetAutor), new { autorId = autor.Id }, autorDto);
        }

        [HttpPut("{autorId}")]
        public async Task<ActionResult<AutorDTO>> UpdateAutor(
            int autorId,
            [FromBody] CreateAutorDTO createAutorDTO
        )
        {
            var autor = await _repository.GetById(autorId);

            if (autor == null)
                return NotFound();

            var autorToUpdate = _mapper.Map(createAutorDTO, autor);

            var resultado = await _repository.Update(autorToUpdate);

            if (resultado)
                return NoContent();

            return BadRequest();
        }

        [HttpDelete("{autorId}")]
        public async Task<IActionResult> DeleteAutor(int autorId)
        {
            var autor = await _repository.GetById(autorId);

            if (autor == null)
                return NotFound();

            var result = await _repository.Delete(autorId);

            if (result)
                return NoContent();

            return BadRequest();
        }
    }
}
