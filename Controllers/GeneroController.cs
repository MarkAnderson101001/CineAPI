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
        //// GET ////
        [HttpGet]
        public async Task<ActionResult<List<DTOGenero>>> Get()
        {
            var entities = await context.TGenero.ToListAsync();
            return mapper.Map<List<DTOGenero>>(entities);
        }

        [HttpGet("{id:int}", Name = "GetGenero")]
        public async Task<ActionResult<DTOGenero>> Get(int id)
        {
            var entities = await context.TGenero.FirstOrDefaultAsync(x => x.Id == id);
            if (entities == null) return NotFound();
            return mapper.Map<DTOGenero>(entities);
        }


        //// POST ////
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] DTOGeneroC dtogeneroc )
        {
            var entity = mapper.Map<OGenero>(dtogeneroc);
            context.Add(entity);
            await context.SaveChangesAsync();
            var generodto = mapper.Map<DTOGenero>(entity);
            return new CreatedAtRouteResult("GetGenero", new
            {
                id = generodto.Id 
            }, generodto
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
