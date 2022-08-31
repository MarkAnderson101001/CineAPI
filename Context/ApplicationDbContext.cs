using Cine.Domain.Objects;
using Cine.Domain.ObjectsR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Cine.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions Options) : base(Options)
        {}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PeliculaGenero>().HasKey(x => new { x.PeliculaID, x.GeneroID });
            modelBuilder.Entity<PeliculaActor> ().HasKey(x => new { x.PeliculaID, x.ActorID  });
            modelBuilder.Entity<PeliculaSala>  ().HasKey(x => new { x.PeliculaID, x.SalaID   });

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<OActor> TActor { get; set; }
        public DbSet<OGenero> TGenero { get; set; }
        public DbSet<OPelicula> TPelicula { get; set; }
        public DbSet<OReview> TReview { get; set; }
        public DbSet<OSala> TSala { get; set; }
        public DbSet<OUsuario> TUsuario { get; set; }

        //////////////////////////////////////////////////////////////////////////
        public DbSet<PeliculaActor>  TPeliculaActor  { get; set; }
        public DbSet<PeliculaGenero> TPeliculaGenero { get; set; } 
        public DbSet<PeliculaSala>   TPeliculaSala   { get; set; }

    } 
}


