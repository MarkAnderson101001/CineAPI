using AutoMapper;
using Cine.Context;
using Cine.Domain.Objects;
using Cine.DTO.DTOActor;
using Cine.Utilerias;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cine.Controllers
{
    [ApiController]
    [Route("api/actor")]
    public class ActorController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public ActorController(ApplicationDbContext _context, IMapper _mapper)
        {
            context = _context;
            mapper = _mapper;
        }
        ////// GET //////
        
        [HttpGet]
        public async Task<ActionResult<List<DTOActor>>> Get()
        {
            var entidades = await context.TActor.ToListAsync();
            return mapper.Map<List<DTOActor>>(entidades);
        }

        [HttpGet("{id:int}", Name = "GetActorId")]
        public async Task<ActionResult<DTOActor>> getActorId(int Id)
        {
            var result = await context.TActor.FirstOrDefaultAsync(x => x.Id == Id);
            if (result == null)
            {
                return NotFound();
            }
            var mapActor = mapper.Map<DTOActor>(result);
            return mapActor;
        }
        ////// POST //////
        [HttpPost]
        public async Task<ActionResult> CreateAuthor([FromForm] DTOActorC _actor)
        {
            var exist = await context.TActor.AnyAsync(x => x.NombreA == _actor.NombreA);
            if (exist) { return BadRequest($"existe un author con el nombre: {_actor.NombreA}"); }
            /////////////////////////////

            var mapActor = mapper.Map<OActor>(_actor);
            context.Add(mapActor);
            var result = await context.SaveChangesAsync();

            if (result > 0) { return Ok(); }

            /////////////////////////////

            return BadRequest("Ingrese correctamente los valores");
        }

        ////// PUT //////
        [HttpPut("{id:int}", Name = "ModifyActor")]
        public async Task<ActionResult> ModifyActor([FromBody] DTOActorC _actor, int id)
        {
            var exist = await context.TActor.AnyAsync(x => x.NombreA == _actor.NombreA);
            if (!exist) { return NotFound(); }
            /////////////////////////////

            var mapActor = mapper.Map<OActor>(_actor);
            mapActor.Id = id;

            context.Update(mapActor);
            var result = await context.SaveChangesAsync();
            if (result > 0) { return Ok(); }

            ///////////////////////////////////////
            return BadRequest("No se logro hacer update");

        }

        ////// DELETE //////
        [HttpDelete("{id:int}", Name = "DeleteActor")]
        public async Task<ActionResult> DeleteA(int id)
        {
            ////////////////////////////////////

            var exist = await context.TActor.AnyAsync(x => x.Id == id);
            if (!exist) { return NotFound(); }
            ///////////////////////////////////

            context.Remove(new OActor() { Id = id });
            var result = await context.SaveChangesAsync();
            if (result > 0) { return Ok(); }

            return BadRequest("No se logro borrar el Autor");
        }

    }
}
