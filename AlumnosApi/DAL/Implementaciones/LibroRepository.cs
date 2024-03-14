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
    public class LibroRepository : GenericRepository<Libro>, ILibroRepository
    {
        private readonly ApplicationDbContext _context;

        public LibroRepository(ApplicationDbContext context)
            : base(context)
        {
            _context = context;
        }

        public async Task<Libro> GetByIdWithRelation(int id)
        {
            var query = await _context
                .Libros.Include(a => a.Autor)
                .Include(g => g.Genero)
                .Include(c => c.Comentarios)
                .FirstOrDefaultAsync(l => l.Id == id);
            return query;
        }

        public async Task<IEnumerable<Libro>> GetAllWithRelation()
        {
            var libros = await _context
                .Libros.Include(a => a.Autor)
                .Include(g => g.Genero)
                .Include(c => c.Comentarios)
                .ToListAsync();
            return libros;
        }
    }
}
