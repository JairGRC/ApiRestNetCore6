using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiAutores.DTOs;
using WebApiAutores.Entidades;

namespace WebApiAutores.Controllers
{
    [ApiController]
    [Route("api/libros")]
    public class LibrosController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public LibrosController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<LibroDTO>> Get(int id)
        {

            var libro = await context.Libros
                .Include(libroBD=>libroBD.Comentarios)
                .FirstOrDefaultAsync(x => x.Id == id);
            return mapper.Map<LibroDTO>(libro);
        }
        [HttpPost]
        public async Task<ActionResult> Post(LibroCreacionDTO libroCreacion)
        {
            if (libroCreacion.AutoresIds == null)
            {
                return BadRequest("No se puede crear un libro sin autores");
            }
            var autoresIDs = await context.Autores
                .Where(autorBD => libroCreacion.AutoresIds.Contains(autorBD.Id)).Select(x => x.Id).ToListAsync();
            
            if (libroCreacion.AutoresIds.Count != autoresIDs.Count)
            {
                return BadRequest("No existe uno de los autores enviados ");
            }
            var libro = mapper.Map<Libro>(libroCreacion);
            if (libro.AutoresLibros != null)
            {
                for (int i = 0; i < libro.AutoresLibros.Count; i++)
                {
                    libro.AutoresLibros[i].Orden = i;
                }
            }
            context.Add(libro);
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}
