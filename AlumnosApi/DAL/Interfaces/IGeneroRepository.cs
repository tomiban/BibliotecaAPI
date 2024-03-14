using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlumnosApi.Models;

namespace AlumnosApi.DAL.Interfaces
{
    public interface IGeneroRepository
    {
        public Task<Genero> GetByIdWithRelation(int id);
        public Task<IEnumerable<Genero>> GetAllWithRelation();
    }
}
