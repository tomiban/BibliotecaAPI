using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlumnosApi.DTO;
using AlumnosApi.Models;
using AutoMapper;

namespace AlumnosApi.Utils
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<Autor, AutorDTO>()
                .ForMember(
                    d => d.FechaNacimiento,
                    opt => opt.MapFrom(o => o.FechaNacimiento.ToString("dd/MM/yyyy"))
                );

            CreateMap<CreateAutorDTO, Autor>()
                .ForMember(
                    d => d.FechaNacimiento,
                    opt => opt.MapFrom(o => DateTime.Parse(o.FechaNacimiento))
                );

            CreateMap<CreateComentarioDTO, Comentario>();
            CreateMap<Comentario, ComentarioDTO>();

            CreateMap<Libro, LibroDTO>()
                .ForMember(d => d.NombreAutor, o => o.MapFrom(src => src.Autor.Nombre))
                .ForMember(d => d.NombreGenero, o => o.MapFrom(src => src.Genero.Nombre))
                .ForMember(
                    d => d.FechaLanzamiento,
                    opt => opt.MapFrom(o => o.FechaLanzamiento.ToString("dd/MM/yyyy"))
                );

            CreateMap<CreateLibroDTO, Libro>()
                .ForMember(d => d.Id, o => o.Ignore())
                .ForMember(d => d.Autor, o => o.Ignore())
                .ForMember(d => d.Genero, o => o.Ignore());

            CreateMap<Libro, GeneroLibroDTO>()
                .ForMember(d => d.NombreAutor, o => o.MapFrom(src => src.Autor.Nombre))
                .ForMember(
                    d => d.FechaLanzamiento,
                    opt => opt.MapFrom(o => o.FechaLanzamiento.ToString("dd/MM/yyyy"))
                );

            CreateMap<Genero, GeneroDTO>();
            CreateMap<Usuario, UsuarioDTO>();
            CreateMap<CreateGeneroDTO, Genero>();
        }
    }
}
