using AutoMapper;
using Cine.Context;
using Cine.Domain.Objects;
using Cine.DTO.DTOPelicula;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cine.Controllers
{
    [ApiController]
    [Route("api/pelicula")]
    public class PeliculaController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public PeliculaController(ApplicationDbContext _context, IMapper _mapper)
        {
            context = _context;
            mapper = _mapper;
        }
        ////// GET //////

        [HttpGet]
        public async Task<ActionResult<List<DTOPelicula>>> Get()
        {
            var entidades = await context.TPelicula.ToListAsync();
            return mapper.Map<List<DTOPelicula>>(entidades);
        }

        [HttpGet("{id:int}", Name = "GetPeliculaId")]
        public async Task<ActionResult<DTOPelicula>> getActorId(int Id)
        {
            var result = await context.TActor.FirstOrDefaultAsync(x => x.Id == Id);
            if (result == null)
            {
                return NotFound();
            }
            var mapActor = mapper.Map<DTOPelicula>(result);
            return mapActor;
        }
        ////// POST //////
        [HttpPost]
        public async Task<ActionResult> CreateAuthor([FromForm] DTOPeliculaC _Pelicula)
        {
            var exist = await context.TPelicula.AnyAsync(x => x.NombreP == _Pelicula.NombreP);
            if (exist) { return BadRequest($"existe una Pelicula con el nombre: {_Pelicula.NombreP}"); }
            /////////////////////////////

            var map = mapper.Map<OPelicula>(_Pelicula);
            context.Add(map);
            var result = await context.SaveChangesAsync();

            if (result > 0) { return Ok(); }

            /////////////////////////////

            return BadRequest("Ingrese correctamente los valores");
        }

        ////// PUT //////
        [HttpPut("{id:int}", Name = "ModifyPelicula")]
        public async Task<ActionResult> ModifyActor([FromBody] DTOPeliculaC _Pelicula, int id)
        {
            var exist = await context.TPelicula.AnyAsync(x => x.NombreP == _Pelicula.NombreP);
            if (!exist) { return NotFound(); }
            /////////////////////////////

            var map = mapper.Map<OPelicula>(_Pelicula);
            map.Id = id;

            context.Update(map);
            var result = await context.SaveChangesAsync();
            if (result > 0) { return Ok(); }

            ///////////////////////////////////////
            return BadRequest("No se logro hacer update");

        }

        ////// DELETE //////
        [HttpDelete("{id:int}", Name = "DeletePelicula")]
        public async Task<ActionResult> DeleteG(int id)
        {
            ////////////////////////////////////

            var exist = await context.TPelicula.AnyAsync(x => x.Id == id);
            if (!exist) { return NotFound(); }
            ///////////////////////////////////

            context.Remove(new OActor() { Id = id });
            var result = await context.SaveChangesAsync();
            if (result > 0) { return Ok(); }

            return BadRequest("No se logro borrar la  Pelicula");
        }
    }
}
