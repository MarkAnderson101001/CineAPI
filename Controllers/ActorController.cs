using AutoMapper;
using Cine.Context;
using Cine.Domain.Objects;
using Cine.DTO.DTOActor;
using Cine.DTO.DTOZ;
using Cine.Servicios;
using Cine.Utilerias;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
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
        private readonly IAlmacenadorArchivos almacenadorArchivos;
        private readonly string contenedor = "actores";

        public ActorController(ApplicationDbContext _context, IMapper _mapper, IAlmacenadorArchivos _almacenadorArchivos)
        {
            context = _context;
            mapper  = _mapper;
            almacenadorArchivos = _almacenadorArchivos;
        }
        //// GET ////
        [HttpGet]
        public async Task<ActionResult<List<DTOActor>>> Get()
        {
            var entities = await context.TGenero.ToListAsync();
            return mapper.Map<List<DTOActor>>(entities);
        }

        [HttpGet("{id:int}", Name = "GetActor")]
        public async Task<ActionResult<DTOActor>> Get(int id)
        {
            var entities = await context.TGenero.FirstOrDefaultAsync(x => x.Id == id);
            if (entities == null) return NotFound();
            return mapper.Map<DTOActor>(entities);
        }


        //// POST ////
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] DTOActorC dtoactorc)
        {
            var entity = mapper.Map<OActor>(dtoactorc);
            context.Add(entity);
            await context.SaveChangesAsync();
            var actordto = mapper.Map<DTOActor>(entity);
            return new CreatedAtRouteResult("GetActor", new
            {
                id = actordto.Id
            }, actordto
            );
        }

        ///// PUT ////
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put([FromBody] DTOGeneroC dtogeneroc, int id)
        {
            var entity = mapper.Map<OGenero>(dtogeneroc);
            entity.Id = id;
            context.Entry(entity).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }

        ///// PATCH /////
        #region PATCH
        #endregion

        //// DELETE ////
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            if (!(await context.TGenero.AnyAsync(x => x.Id == id))) return NotFound();
            context.Remove(new OGenero() { Id = id });
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}
