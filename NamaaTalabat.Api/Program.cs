
using Microsoft.EntityFrameworkCore;
using NamaaTalabat.Core.Entities;
using NamaaTalabat.Core.Repositories.Contract;
using NamaaTalabat.Repository;
using NamaaTalabat.Repository.Data;

namespace NamaaTalabat.Api
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            //var _DbContext = new StoreContext();
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<StoreContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
            );

            builder.Services.AddScoped<IGenericRepository<Product> , GenericRepository<Product>>();
            builder.Services.AddScoped<IGenericRepository<ProductBrand> , GenericRepository<ProductBrand>>();
            builder.Services.AddScoped<IGenericRepository<ProductCategory> , GenericRepository<ProductCategory>>();

            //builder.Services.AddScoped < typeof(IGenericRepository<>) , typeof(GenericRepository<>) > ();
            //builder.Services.AddScoped<typeof (IGenericRepository<>) , typeof ( GenericRepository<>) > ();
            var app = builder.Build();
            var scope = app.Services.CreateScope();
            var service = scope.ServiceProvider;
            using var _DbContext = service.GetRequiredService<StoreContext>();
            var logger = service.GetRequiredService<ILoggerFactory>();

            try
            {

                await _DbContext.Database.MigrateAsync();
                DataSeedContext.SeedDatabase(_DbContext); 

            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex);
               var log =  logger.CreateLogger<Program>();
                log.LogError(ex, "an error happend during migration");

            }
            #region Dispose
            //finally
            //{
            //    _DbContext.Dispose();
            //} 
            #endregion


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
