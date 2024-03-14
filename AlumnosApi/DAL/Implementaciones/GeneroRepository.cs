using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlumnosApi.DAL.DataContext;
using AlumnosApi.DAL.Interfaces;
using AlumnosApi.Models;
using Microsoft.EntityFrameworkCore;

namespace AlumnosApi.DAL.Implementaciones
{
    public class GeneroRepository : IGeneroRepository
    {
        private readonly ApplicationDbContext _context;

        public GeneroRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Genero>> GetAllWithRelation()
        {
            var query = await _context
                .Generos.Include(g => g.Libros)
                .ThenInclude(l => l.Autor) // Incluye información del autor
                .ToListAsync();
            return query;
        }

        public async Task<Genero> GetByIdWithRelation(int id)
        {
            var query = await _context
                .Generos.Include(g => g.Libros)
                .ThenInclude(l => l.Autor)
                .FirstOrDefaultAsync(g => g.Id == id);

            return query;
        }
    }
}
