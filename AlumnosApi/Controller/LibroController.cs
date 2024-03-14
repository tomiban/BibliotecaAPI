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
    [Route("api/libros")]
    public class LibroController : ControllerBase
    {
        private readonly IGenericRepository<Libro> _repository;
        private readonly ILibroRepository _libroRepository;
        private readonly IMapper _mapper;

        public LibroController(
            IGenericRepository<Libro> libroRepository,
            IMapper mapper,
            ILibroRepository libroRepo
        )
        {
            _mapper = mapper;
            _repository = libroRepository;
            _libroRepository = libroRepo;
        }

       // [ResponseCache(Duration = 10)]
        [ResponseCache(CacheProfileName = "PorDefecto")]
        // [ResponseCache(Location = ResponseCacheLocation.None,  NoStore = true)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Libro>>> GetAllLibros()
        {
            var libros = await _libroRepository.GetAllWithRelation();
            var librosDTO = _mapper.Map<IEnumerable<LibroDTO>>(libros);
            return Ok(librosDTO);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LibroDTO>> GetLibro(int id)
        {
            var libro = await _libroRepository.GetByIdWithRelation(id);
            if (libro == null)
                return NotFound();

            var libroDto = _mapper.Map<LibroDTO>(libro);
            return Ok(libroDto);
        }

        [HttpPost]
        public async Task<ActionResult<LibroDTO>> CreateLibro(CreateLibroDTO createLibroDTO)
        {
            var libro = _mapper.Map<Libro>(createLibroDTO);

            await _repository.Create(libro);

            var libroDto = _mapper.Map<LibroDTO>(libro);

            return CreatedAtAction(nameof(GetLibro), new { id = libro.Id }, libroDto);
        }

        [HttpPut("{libroId}")]
        public async Task<ActionResult<LibroDTO>> UpdateLibro(
            int libroId,
            [FromBody] CreateLibroDTO createLibroDTO
        )
        {
            var libro = await _repository.GetById(libroId);

            if (libro == null)
                return NotFound();

            var libroToUpdate = _mapper.Map(createLibroDTO, libro);
            var resultado = await _repository.Update(libroToUpdate);

            if (resultado)
                return NoContent();

            return BadRequest();
        }

        [HttpDelete("{libroId}")]
        public async Task<IActionResult> DeleteLibro(int libroId)
        {
            var libro = await _repository.GetById(libroId);

            if (libro == null)
                return NotFound();

            var result = await _repository.Delete(libroId);

            if (result)
                return NoContent();

            return BadRequest();
        }
    }
}
