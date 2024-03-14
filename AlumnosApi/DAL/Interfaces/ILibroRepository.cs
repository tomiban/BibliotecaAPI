using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlumnosApi.Models;

namespace AlumnosApi.DAL.Interfaces
{
    public interface ILibroRepository : IGenericRepository<Libro>
    {
        public Task<Libro> GetByIdWithRelation(int id);
        public Task<IEnumerable<Libro>> GetAllWithRelation();
    }
}