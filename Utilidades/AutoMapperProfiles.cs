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
            CreateMap<Autor,AutorDTO>();
            CreateMap<LibroCreacionDTO, Libro>()
                .ForMember(libro => libro.AutoresLibros, opciones => opciones.MapFrom(MapAutoresLibros));
            CreateMap<Libro, LibroDTO>();
            CreateMap<ComentarioCreacionDTO, Comentario>();
            CreateMap<Comentario, ComentarioDTO>();
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
