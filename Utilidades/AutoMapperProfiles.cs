using AutoMapper;
using WebApiAutores.DTOs;
using WebApiAutores.Entidades;

namespace WebApiAutores.Utilidades
{
    public class AutoMapperProfiles:Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<AutorCreacionDTO, Autor>();
            CreateMap<Autor, AutorDTO>();
            CreateMap<Autor, AutorDTOConLibros>()
                .ForMember(autorDTO =>autorDTO.Libros,opciones=>opciones.MapFrom(MapAutorDTOLibros));
            CreateMap<LibroCreacionDTO, Libro>()
                .ForMember(libro => libro.AutoresLibros, opciones => opciones.MapFrom(MapAutoresLibros));
            CreateMap<Libro, LibroDTO>();
                CreateMap<Libro, LibroDTOConAutores>()
                .ForMember(libroDTO => libroDTO.Autores, opciones => opciones.MapFrom(MapLibroDTOAutores));

            CreateMap<ComentarioCreacionDTO, Comentario>();
            CreateMap<Comentario, ComentarioDTO>();
            CreateMap<LibroPatchDTO, Libro>().ReverseMap();

        }
        private List<LibroDTO> MapAutorDTOLibros(Autor autor, AutorDTO autorDTO)
        {
            var resultado = new List<LibroDTO>();
            if(autor.AutoresLibros is null)
            {
                return resultado;
            }
            autor.AutoresLibros.ForEach(x =>
            {
                resultado.Add(new LibroDTO()
                {
                    Id=x.LibroId,
                    Titulo=x.Libro.Titulo
                });
            });
            return resultado;
        }
        private List<AutorDTO> MapLibroDTOAutores(Libro libro,LibroDTO libroDTO)
        {
            var resultado = new List<AutorDTO>();
            if (libro.AutoresLibros == null) return resultado;
            libro.AutoresLibros.ForEach(x =>
            {
                resultado.Add(new AutorDTO()
                {
                    Id = x.AutorId,
                    Nombre = x.Autor.Nombre
                });
            });
            return resultado;
        }
        private List<AutorLibro> MapAutoresLibros(LibroCreacionDTO libroCreacionDTO,Libro libro)
        {
            var resultado = new List<AutorLibro>();
            if(libroCreacionDTO.AutoresIds == null)
            {
                return resultado;
            }
            libroCreacionDTO.AutoresIds.ForEach(x =>
            {
                resultado.Add(new AutorLibro() { AutorId = x });
            });
            return resultado;
        }
    }
}
