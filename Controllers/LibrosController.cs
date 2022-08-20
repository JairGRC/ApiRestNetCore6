﻿using AutoMapper;
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
            //var existAutor = await context.Autores.AnyAsync(x => x.Id == libro.AutorId);
            //if (!existAutor)
            //{
            //    return BadRequest($"No existe el autor del Id: {libro.AutorId}");
            //}
            var libro = mapper.Map<Libro>(libroCreacion);
            context.Add(libro);
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}
