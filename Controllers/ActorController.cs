using AutoMapper;
using Cine.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cine.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActorController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public ActorController (ApplicationDbContext _context,IMapper _mapper)
        {
            context = _context;
            mapper  = _mapper;
        }
    }
}
