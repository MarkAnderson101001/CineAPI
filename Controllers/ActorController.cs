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
        public async Task<ActionResult<List<DTOActor>>> Get([FromQuery] DTOPaginacion pags)
        {
            var queryable = context.TActor.AsQueryable();
            await HttpContext.InsertarPaginacion(queryable, pags.CantidadRegistrosPP);
            var entities = await queryable.Paginar(pags).ToListAsync();
            return mapper.Map<List<DTOActor>>(entities);
        }

        [HttpGet("{id:int}", Name = "GetActor")]
        public async Task<ActionResult<DTOActor>> Get(int id)
        {
            var entities = await context.TActor.FirstOrDefaultAsync(x => x.Id == id);
            if (entities == null) return NotFound();
            return mapper.Map<DTOActor>(entities);
        }


        //// POST ////
        [HttpPost]
        public async Task<ActionResult> Post([FromForm] DTOActorC dtoactorc)
        {
            var entity = mapper.Map<OActor>(dtoactorc);

            if(dtoactorc.FotoA != null)
            {
                using (var memorystream = new MemoryStream())
                {
                    await dtoactorc.FotoA.CopyToAsync(memorystream);
                    var content = memorystream.ToArray();
                    var extension = Path.GetExtension(dtoactorc.FotoA.FileName);
                    entity.FotoA = await almacenadorArchivos.GuardarArchivo(
                        content,
                        extension,
                        contenedor,
                        dtoactorc.FotoA.ContentType);
                }
            }
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
        public async Task<ActionResult> Put([FromForm] DTOActorC dtoactorc, int id)
        {
            //Verificamos que campos fueron modificados
            var actorDB = await context.TActor.FirstOrDefaultAsync(x => x.Id == id);
            if (actorDB == null) return NotFound();//En caso de no ser encontrado
            actorDB = mapper.Map(dtoactorc, actorDB);//mapea los valores modificados a la imagen de la BD
            //No queremos que mapper actualice la foto pq una es un formfile y otra un string
            if (dtoactorc.FotoA != null)
            {
                using (var memorystream = new MemoryStream())
                {
                    await dtoactorc.FotoA.CopyToAsync(memorystream);
                    var content = memorystream.ToArray();
                    var extension = Path.GetExtension(dtoactorc.FotoA.FileName);
                    actorDB.FotoA = await almacenadorArchivos.EditarArchivo(
                        content,
                        extension,
                        contenedor,
                        actorDB.FotoA,
                        dtoactorc.FotoA.ContentType);
                }
            }
            await context.SaveChangesAsync();
            return NoContent();
        }

        ///// PATCH ///// Actualizacion Parcial
        [HttpPatch("{id:int}")]
        public async Task<ActionResult> Patch(int id, [FromBody] JsonPatchDocument<DTOActorP> json)
        {
            //Error en el Json
            if (json == null) return BadRequest();
            var autorDB = await context.TActor.FirstOrDefaultAsync(x => x.Id == id);
            //El autor no existe
            if (autorDB == null) return NotFound();
            var entity = mapper.Map<DTOActorP>(autorDB);
            json.ApplyTo(entity, ModelState);//ModelState from Newtonsoft
            //No es valido
            if(!TryValidateModel(entity)) return BadRequest();
            mapper.Map(entity,autorDB);//from dto to entity
            await context.SaveChangesAsync();
            return NoContent();


        }

        //// DELETE ////
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            if (!(await context.TActor.AnyAsync(x => x.Id == id))) return NotFound();
            context.Remove(new OActor() { Id = id });
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}
