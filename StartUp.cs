using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;
using WebApiAutores.Controllers;
using WebApiAutores.Middlewars;
using WebApiAutores.Servicios;

namespace WebApiAutores
{
    public class StartUp
    {
        public StartUp(IConfiguration configuration)
        {
            //var autoresController = new AutoresController(new ApplicationDbContext(null),
            //    new ServicioA(new Logger())
            //    );
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {

            //Configuracion de Servicio para ignorar ciclos 
            services.AddControllers().AddJsonOptions(x =>
            x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("defaultConnection")));
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            //Implementando servicios
            //AddTransient => Nueva Instancia de la clase <A,B> servicio B
            //AddScoped => tiempo de vida de la clase servicio B aumenta, la misma Instancia (El mismo cliente)
            //AddSingleton => La misma instancia para distintos usuarios 
            services.AddTransient<IServicio, ServicioA>();
            //services.AddTransient<ServicioA>();

            services.AddTransient<ServicioTransient>();
            services.AddScoped<ServicioScope>();
            services.AddSingleton<ServicioSingleton>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApiAutores", Version = "v1" });
            });
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,ILogger<StartUp> logger)
        {
            //Middleware  app.Use...

            //app.UseMiddleware<LoguearRespuestaHTTPMiddleware>();
            app.UseLoguearRespuestaHTTP();
            app.Map("/ruta1", app =>
            {
                app.Run(async contextor =>
                {
                    await contextor.Response.WriteAsync("Estoy interceptando la tuberia");
                });

            });
            
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c=>c.SwaggerEndpoint("/swagger/v1/swagger.json","WebAPIAutores V1"));
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
