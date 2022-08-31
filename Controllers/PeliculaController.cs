using AutoMapper;
using Cine.Context;
using Cine.Domain.Objects;
using Cine.DTO.DTO;
using Cine.DTO.DTOPelicula;
using Cine.Servicios;
using Cine.Utilerias;
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
        #region GET
        
        [HttpGet] // get all
        public async Task<ActionResult<DTOPeliculasIndex>> GetPeliculas()
        {
            var top = 2;
            var hoy = DateTime.Today;

            var proximoestreno = await context.TPelicula.Where(x => x.FechaEstrenoP > hoy).OrderBy(x => x.FechaEstrenoP).Take(top).ToListAsync();
            var encines        = await context.TPelicula.Where(x => x.Encine).Take(top).ToListAsync();

            var result            = new DTOPeliculasIndex();
            result.proximoestreno = mapper.Map<List<DTOPelicula>>(proximoestreno);
            result.encines        = mapper.Map<List<DTOPelicula>>(encines);

            return result;
        }

        [HttpGet("filtro")]
        public async Task<ActionResult<List<DTOPelicula>>> Filtrar([FromQuery] DTOPeliculaFiltro pelifiltro )
        {
            var hoy        = DateTime.Now;
            var peliculasQ = context.TPelicula.AsQueryable();
            if (!string.IsNullOrEmpty(pelifiltro.titulo)) { peliculasQ = peliculasQ.Where(x => x.NombreP.Contains(pelifiltro.titulo));}
            if (pelifiltro.encines)                       { peliculasQ = peliculasQ.Where(x => x.Encine); }
            if (pelifiltro.proximoestreno)                { peliculasQ = peliculasQ.Where(x => x.FechaEstrenoP > hoy);  }
            if (pelifiltro.GeneroID !=0)                  { peliculasQ = peliculasQ.Where(x => x.PeliculaGenero.Select(y => y.GeneroID).Contains(pelifiltro.GeneroID));}

            await HttpContext.InsertarPaginacion(peliculasQ, pelifiltro.CantidadPP);
            var pelis = await peliculasQ.Paginar(pelifiltro.paginacion).ToListAsync();

            return mapper.Map<List<DTOPelicula>>(pelis);
        }

        [HttpGet("{id:int}", Name = "GetPeliculaId")] // get id 
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

        #endregion

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
            if (_pelicula == null) { return BadRequest("se esperaba actor Patch"); }
            var exist = await context.TActor.FirstOrDefaultAsync(x => x.Id == id);
            if (exist == null) { return NotFound(); }

            //////
            var pelidb = exist;
            var mapPeli = mapper.Map<DTOPeliculaP>(pelidb);
            _pelicula.ApplyTo(mapPeli, ModelState);

            var validopeli = TryValidateModel(mapPeli);
            if (!validopeli) { return BadRequest(ModelState); }

            
            mapper.Map(mapPeli, pelidb);
            
            //////
            var result = await context.SaveChangesAsync();
            if (result > 0) { return Ok(); }
            //////
            return BadRequest("no se lograron editar los datos");
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