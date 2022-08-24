using Cine.Context;
using Cine.Servicios;
using Cine.Servicios.Azure;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace Cine
{
    public class Program
    {
        
        public static void Main(string[] args)
        {
            

            var builder = WebApplication.CreateBuilder(args);

            // Context
            
            builder.Services.AddControllers();
            builder.Services.AddDbContext<ApplicationDbContext>(options => {
                options.UseSqlServer(builder.Configuration.GetConnectionString("defaultConnection"));
            });
            //AutoMapper
            builder.Services.AddAutoMapper(typeof(Program));
            // Azure storage
            builder.Services.AddTransient<IAlmacenadorArchivos, AlmacenadorArchivosAZure>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.

          
            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}