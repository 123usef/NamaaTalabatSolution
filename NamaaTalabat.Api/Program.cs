
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NamaaTalabat.Api.Errors;
using NamaaTalabat.Api.Helpers;
using NamaaTalabat.Api.Middlewares;
using NamaaTalabat.Core.Entities;
using NamaaTalabat.Core.Entities.Identity;
using NamaaTalabat.Core.Repositories.Contract;
using NamaaTalabat.Repository;
using NamaaTalabat.Repository.Data;
using NamaaTalabat.Repository.IdentityData;
using NamaaTalabat.Repository.IdentityData.DataSeed;

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
           
            builder.Services.AddDbContext<AppIdenittyDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection")));
           
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options=>
            {
                options.Lockout.MaxFailedAccessAttempts = 5;
            })
                .AddEntityFrameworkStores<AppIdenittyDbContext>();

            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            builder.Services.AddAutoMapper(typeof(MappingProfiles));


            builder.Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (actionContext) =>
                {
                    var errors = actionContext.ModelState.Where(p => p.Value.Errors.Count() > 0)
                                            .SelectMany(p => p.Value.Errors)
                                             .Select(e => e.ErrorMessage)
                                             .ToList();

                    var validationResponse = new ValidationErrorResponse()
                    {
                        Errors = errors
                    };
                    return new BadRequestObjectResult(validationResponse);

                };
                }
            );



        //builder.Services.Configure<IConfiguration>();
        //builder.Services.AddAutoMapper(M => M.AddProfile<MappingProfiles>());
        #region Old but not gold
        //builder.Services.AddScoped<IGenericRepository<Product> , GenericRepository<Product>>();
        //builder.Services.AddScoped<IGenericRepository<ProductBrand> , GenericRepository<ProductBrand>>();
        //builder.Services.AddScoped<IGenericRepository<ProductCategory> , GenericRepository<ProductCategory>>();
        //builder.Services.AddScoped < typeof(IGenericRepository<>) , typeof(GenericRepository<>) > ();
        //builder.Services.AddScoped<typeof (IGenericRepository<>) , typeof ( GenericRepository<>) > (); 
        #endregion

        var app = builder.Build();
            var scope = app.Services.CreateScope();
            var service = scope.ServiceProvider;
            using var _DbContext = service.GetRequiredService<StoreContext>();
            using var _IdentityContext = service.GetRequiredService<AppIdenittyDbContext>();
            var _userManager = service.GetRequiredService<UserManager<ApplicationUser>>();
            var logger = service.GetRequiredService<ILoggerFactory>();

            try
            {

                await _DbContext.Database.MigrateAsync();
                DataSeedContext.SeedDatabase(_DbContext); 

                await _IdentityContext.Database.MigrateAsync();
                IdentitySeeding.SeedIdentity(_userManager);

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
            app.UseMiddleware<ServerExceptionMiddleware>();
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseStatusCodePagesWithRedirects("/notfound/{0}");

            app.UseHttpsRedirection();
          
            app.UseAuthorization();



            app.MapControllers();

            app.Run();
        }
    }
}
