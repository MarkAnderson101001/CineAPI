using Cine.Domain.Objects;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Cine.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions Options) : base(Options)
        {
        }

        public DbSet<OActor>    TActor    { get; set; }
        public DbSet<OGenero>   TGenero   { get; set; }
        public DbSet<OPelicula> TPelicula { get; set; }
        public DbSet<OReview>   TReview   { get; set; }
        public DbSet<OSala>     TSala     { get; set; }
        public DbSet<OUsuario>  TUsuario  { get; set; }
    }
}
