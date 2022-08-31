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
        ////// GET //////
        #region GET
        [HttpGet]
        public async Task<ActionResult<List<DTOActor>>> GetActor([FromQuery] DTOPaginacion _paginacion)
        {
            var gqueryable = context.TActor.AsQueryable();
            await HttpContext.InsertarPaginacion(gqueryable, _paginacion.CantidadRegistrosPP);
            /////////////////////////////////////////////////////////////////////////////////////////////
            var entidades = await gqueryable.Paginar(_paginacion).ToListAsync();


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
        #endregion

        ////// POST //////
        #region POST
        [HttpPost]
        public async Task<ActionResult> CreateAuthor([FromForm] DTOActorC _actor)
        {
            var exist = await context.TActor.AnyAsync(x => x.NombreA == _actor.NombreA);
            if (exist) { return BadRequest($"existe un author con el nombre: {_actor.NombreA}"); }
            /////////////////////////////

            var mapActor = mapper.Map<OActor>(_actor);

            if (_actor.FotoA != null)
            {
                using (var memorystream = new MemoryStream())
                {
                    await _actor.FotoA.CopyToAsync(memorystream);
                    var contenido = memorystream.ToArray();
                    var extension = Path.GetExtension(_actor.FotoA.FileName);
                    var content = _actor.FotoA.ContentType.ToString();
                    mapActor.FotoA = await almacenadorArchivos.GuardarArchivo(contenido, extension, contenedor, content);

                }
            }

            context.Add(mapActor);
            var result = await context.SaveChangesAsync();

            if (result > 0) { return Ok(); }

            /////////////////////////////

            return BadRequest("Ingrese correctamente los valores");
        }
        #endregion


        ////// PUT //////
        #region PUT
        [HttpPut("{id:int}", Name = "ModifyActor")]
        public async Task<ActionResult> ModifyActor([FromForm] DTOActorC _actor, int id)
        {
            var exist = await context.TActor.FirstOrDefaultAsync(x => x.NombreA == _actor.NombreA);
            if (exist == null) { return NotFound(); }
            /////////////////////////////
            var mapActor = exist;
            mapActor = mapper.Map(_actor, mapActor);

            if (_actor.FotoA != null)
            {
                using (var memorystream = new MemoryStream())
                {
                    await _actor.FotoA.CopyToAsync(memorystream);
                    var contenido = memorystream.ToArray();
                    var extension = Path.GetExtension(_actor.FotoA.FileName);
                    var content = _actor.FotoA.ContentType.ToString();
                    var fotostring = _actor.FotoA.ToString();
                    mapActor.FotoA = await almacenadorArchivos.EditarArchivo(contenido, extension, contenedor, fotostring, content);

                }
            }

            var result = await context.SaveChangesAsync();
            if (result > 0) { return Ok(); }

            ///////////////////////////////////////
            return BadRequest("No se logro hacer update");

        }
        #endregion

        //////  PATCH //////

        #region PATCH
        [HttpPatch("{id:int}", Name = "PatchActor")]
        public async Task<ActionResult> PatchActor([FromBody] JsonPatchDocument<DTOActorP> _actor, int id)
        {
            if (_actor == null) { return BadRequest("se esperaba actor Patch"); }
            var exist = await context.TActor.FirstOrDefaultAsync(x => x.Id == id);
            if (exist == null) { return NotFound(); }

            //////
            var actordb = exist;
            var mapActor = mapper.Map<DTOActorP>(actordb);
            _actor.ApplyTo(mapActor, ModelState);

            var validoactor = TryValidateModel(mapActor);
            if (!validoactor) { return BadRequest(ModelState); }

            mapper.Map(mapActor, actordb);
            //////
            var result = await context.SaveChangesAsync();
            if (result > 0) { return Ok(); }
            //////
            return BadRequest("no se lograron editar los datos");
        }
        #endregion

        ////// DELETE //////
        #region DELETE
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
        #endregion


    }
}
