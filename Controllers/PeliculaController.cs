using AutoMapper;
using Cine.Context;
using Cine.Domain.Objects;
using Cine.DTO.DTOPelicula;
using Cine.Servicios;
using Microsoft.AspNetCore.JsonPatch;
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
        private readonly IAlmacenadorArchivos almacenadorarchivos;
        private readonly string contenedor = "peliculas";

        public PeliculaController(ApplicationDbContext _context, IMapper _mapper, IAlmacenadorArchivos _almacenadorarchivos)
        {
            context             = _context;
            mapper              = _mapper;
            almacenadorarchivos = _almacenadorarchivos;
        }
        ////// GET //////

        [HttpGet]
        public async Task<ActionResult<List<DTOPelicula>>> GetPeliculas()
        {
            var entidades = await context.TPelicula.ToListAsync();
            return mapper.Map<List<DTOPelicula>>(entidades);
        }

        [HttpGet("{id:int}", Name = "GetPeliculaId")]
        public async Task<ActionResult<DTOPelicula>> GetPeliculaId(int Id)
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
        [HttpPost(Name="CrearPelicula")]
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
            context.Add(map);
            var result = await context.SaveChangesAsync();

            if (result > 0) { return Ok(); }

            /////////////////////////////

            return BadRequest("Ingrese correctamente los valores");
        }

        ////// PUT //////
        [HttpPut("{id:int}", Name = "ModifyPelicula")]
        public async Task<ActionResult> ModifyPelicula([FromForm] DTOPeliculaC _Pelicula, int id)
        {
            var exist = await context.TPelicula.FirstOrDefaultAsync(x => x.NombreP == _Pelicula.NombreP);
            if (exist == null) { return NotFound(); }
            /////////////////////////////
            var map = exist;
            map = mapper.Map(_Pelicula, map);

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

            context.Update(map);
            var result = await context.SaveChangesAsync();
            if (result > 0) { return Ok(); }

            ///////////////////////////////////////
            return BadRequest("No se logro hacer update");

        }
        
        //////  PATCH //////
        [HttpPatch("{id:int}", Name = "PatchPelicula")]
        public async Task<ActionResult> PatchActor([FromBody] JsonPatchDocument<DTOPeliculaP> _pelicula, int id)
        {
            if (_pelicula == null) { return BadRequest("se esperaba actor Patch"); }
            var exist      = await context.TActor.FirstOrDefaultAsync(x => x.Id == id);
            if (exist     == null) { return NotFound(); }

            //////
            var pelidb  = exist;
            var mapPeli = mapper.Map<DTOPeliculaP>(pelidb);
            _pelicula.ApplyTo(mapPeli, ModelState);

            var  validopeli = TryValidateModel(mapPeli);
            if (!validopeli) { return BadRequest(ModelState); }

            mapper.Map(mapPeli, pelidb);
            //////
            var result = await context.SaveChangesAsync();
            if (result > 0) { return Ok(); }
            //////
            return BadRequest("no se lograron editar los datos");
        }

        ////// DELETE //////
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
    }
}
