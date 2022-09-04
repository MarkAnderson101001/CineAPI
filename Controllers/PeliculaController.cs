using AutoMapper;
using Cine.Context;
using Cine.Domain.Objects;
using Cine.DTO.DTOPelicula;
using Cine.DTO.DTOZ;
using Cine.Servicios;
using Cine.Utilerias;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq.Dynamic.Core;

namespace Cine.Controllers
{
    [ApiController]
    [Route("api/pelicula")]
    public class PeliculaController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly IAlmacenadorArchivos almacenadorarchivos;
        private readonly ILogger<PeliculaController> logger;
        private readonly string contenedor = "peliculas";

        public PeliculaController(ApplicationDbContext _context, IMapper _mapper, IAlmacenadorArchivos _almacenadorarchivos, ILogger<PeliculaController> _logger)
        {
            context             = _context;
            mapper              = _mapper;
            almacenadorarchivos = _almacenadorarchivos;
            logger              = _logger;
        }
        ////// GET //////
        [HttpGet(Name = "GetPeliculas")]
        public async Task<ActionResult<List<DTOPelicula>>> Get([FromQuery] DTOPaginacion pags)
        {
            var queryable = context.TPelicula.AsQueryable();
            await HttpContext.InsertarPaginacion(queryable, pags.CantidadRegistrosPP);
            var entities = await queryable.Paginar(pags).ToListAsync();
            return mapper.Map<List<DTOPelicula>>(entities);
        }

        [HttpGet("{id:int}", Name = "GetPelicula")]
        public async Task<ActionResult<DTOPelicula>> Get(int id)
        {
            var entity = await context.TPelicula.FirstOrDefaultAsync(x => x.Id == id);
            if (entity == null) return NotFound();
            return mapper.Map<DTOPelicula>(entity);
        }

        ////// POST //////
        #region POST
        [HttpPost(Name = "CrearPelicula")]
        public async Task<ActionResult> CrearPelicula([FromForm] DTOPeliculaC _Pelicula)
        {
            var exist = await context.TPelicula.AnyAsync(x => x.NombreP == _Pelicula.NombreP);
            if (exist) { return BadRequest($"existe una Pelicula con el nombre: {_Pelicula.NombreP}"); }
            /////////////////////////////

            var map = mapper.Map<OPelicula>(_Pelicula);

            if (_Pelicula.FotoP != null)
            {
                using (var memorystream = new MemoryStream())
                {
                    await _Pelicula.FotoP.CopyToAsync(memorystream);
                    var contenido = memorystream.ToArray();
                    var extension = Path.GetExtension(_Pelicula.FotoP.FileName);
                    var content   = _Pelicula.FotoP.ContentType.ToString();
                    map.FotoP     = await almacenadorarchivos.GuardarArchivo(contenido, extension, contenedor, content);

                }
            }
            AsignarOrdenActores(map);
            context.Add(map);
            var result = await context.SaveChangesAsync();

            if (result > 0) { return Ok(); }

            /////////////////////////////

            return BadRequest("Ingrese correctamente los valores");
        }
        #endregion

        ////// PUT //////
        #region PUT
        [HttpPut("{id:int}", Name = "ModifyPelicula")]
        public async Task<ActionResult> ModifyPelicula([FromForm] DTOPeliculaC _Pelicula, int id)
        {
            var exist = await context.TPelicula.Include(x => x.PeliculaActor)
                                               .Include(x=> x.PeliculaGenero)
                                               .FirstOrDefaultAsync(x => x.NombreP == _Pelicula.NombreP);

            if (exist == null) { return NotFound(); }
            /////////////////////////////
            var map = exist;
            map     = mapper.Map(_Pelicula, map);

            if (_Pelicula.FotoP != null)
            {
                using (var memorystream = new MemoryStream())
                {
                    await _Pelicula.FotoP.CopyToAsync(memorystream);
                    var contenido = memorystream.ToArray();
                    var extension = Path.GetExtension(_Pelicula.FotoP.FileName);
                    var content   = _Pelicula.FotoP.ContentType.ToString();
                    var fotoruta  = map.FotoP.ToString();
                    map.FotoP     = await almacenadorarchivos.EditarArchivo(contenido, extension, contenedor, fotoruta, content);

                }
            }
            AsignarOrdenActores(map);
            context.Update(map);
            var result = await context.SaveChangesAsync();
            if (result > 0) { return Ok(); }

            ///////////////////////////////////////
            return BadRequest("No se logro hacer update");

        }
        #endregion

        //////  PATCH //////
        #region PATCH
        [HttpPatch("{id:int}", Name = "PatchPelicula")]
        public async Task<ActionResult> PatchActor([FromBody] JsonPatchDocument<DTOPeliculaP> _pelicula, int id)
        {
            if (_pelicula == null) { return BadRequest(); }
            var exist = await context.TPelicula.FirstOrDefaultAsync(x => x.Id == id);
            if (exist == null) { return NotFound(); }

            //////
            var mapPeli = mapper.Map<DTOPeliculaP>(exist);
            _pelicula.ApplyTo(mapPeli, ModelState);

            if (!TryValidateModel(mapPeli)) { return BadRequest(ModelState); }

            
            mapper.Map(mapPeli, exist);
            
            //////
            await context.SaveChangesAsync();
            //////
            return NoContent();
        }
        #endregion

        ////// DELETE //////
        #region DELETE
        [HttpDelete("{id:int}", Name = "DeletePelicula")]
        public async Task<ActionResult> DeletePelicula(int id)
        {
            ////////////////////////////////////

            var exist = await context.TPelicula.AnyAsync(x => x.Id == id);
            if (!exist) { return NotFound(); }
            ///////////////////////////////////

            context.Remove(new OPelicula() { Id = id });
            var result = await context.SaveChangesAsync();
            if (result > 0) { return Ok(); }

            return BadRequest("No se logro borrar la  Pelicula");
        }
        #endregion

        ///////////////////////////////////////////////////////////////////////////////////////////////
        #region METODOS
        private void AsignarOrdenActores(OPelicula pelicula)
        {
            if (pelicula.PeliculaActor != null)
            {

                for (int i = 0; i < pelicula.PeliculaActor.Count; i++)
                {
                    pelicula.PeliculaActor[i].Orden = i;
                }

            }

        }
        #endregion

    }
}