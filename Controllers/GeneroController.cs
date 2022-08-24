using AutoMapper;
using Cine.Context;
using Cine.Domain.Objects;
using Cine.DTO.DTOGenero;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cine.Controllers
{
    [ApiController]
    [Route("api/genero")]
    public class GeneroController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public GeneroController(ApplicationDbContext _context, IMapper _mapper)
        {
            context = _context;
            mapper = _mapper;
        }
        ////// GET //////

        [HttpGet]
        public async Task<ActionResult<List<DTOGenero>>> Get()
        {
            var entidades = await context.TGenero.ToListAsync();
            return mapper.Map<List<DTOGenero>>(entidades);
        }

        [HttpGet("{id:int}", Name = "GetGeneroId")]
        public async Task<ActionResult<DTOGenero>> getActorId(int Id)
        {
            var result = await context.TActor.FirstOrDefaultAsync(x => x.Id == Id);
            if (result == null)
            {
                return NotFound();
            }
            var mapActor = mapper.Map<DTOGenero>(result);
            return mapActor;
        }
        ////// POST //////
        [HttpPost]
        public async Task<ActionResult> CreateAuthor([FromForm] DTOGeneroC _Genero)
        {
            var exist = await context.TGenero.AnyAsync(x => x.Genero == _Genero.Genero);
            if (exist) { return BadRequest($"existe un Genero con el nombre: {_Genero.Genero}"); }
            /////////////////////////////

            var map = mapper.Map<OGenero>(_Genero);
            context.Add(map);
            var result = await context.SaveChangesAsync();

            if (result > 0) { return Ok(); }

            /////////////////////////////

            return BadRequest("Ingrese correctamente los valores");
        }

        ////// PUT //////
        [HttpPut("{id:int}", Name = "ModifyGenero")]
        public async Task<ActionResult> ModifyActor([FromBody] DTOGeneroC _Genero, int id)
        {
            var exist = await context.TGenero.AnyAsync(x => x.Genero == _Genero.Genero);
            if (!exist) { return NotFound(); }
            /////////////////////////////

            var map = mapper.Map<OGenero>(_Genero);
             map.Id = id;

            context.Update(map);
            var result = await context.SaveChangesAsync();
            if (result > 0) { return Ok(); }

            ///////////////////////////////////////
            return BadRequest("No se logro hacer update");

        }

        ////// DELETE //////
        [HttpDelete("{id:int}", Name = "DeleteGenero")]
        public async Task<ActionResult> DeleteG(int id)
        {
            ////////////////////////////////////

            var exist = await context.TGenero.AnyAsync(x => x.Id == id);
            if (!exist) { return NotFound(); }
            ///////////////////////////////////

            context.Remove(new OActor() { Id = id });
            var result = await context.SaveChangesAsync();
            if (result > 0) { return Ok(); }

            return BadRequest("No se logro borrar el Genero");
        }
    }
}
