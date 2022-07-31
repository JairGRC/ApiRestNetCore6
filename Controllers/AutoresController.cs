using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiAutores.Entidades;

namespace WebApiAutores.Controllers
{
    [ApiController]
    [Route("api/autores")]
    public class AutoresController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<Autor>>Get()
        {
            return new List<Autor>()
            {
                new Autor(){ Id = 1, Nombre = "Jair"},
                new Autor(){Id=2,Nombre="Jose"}
            };
        }
    }
}
