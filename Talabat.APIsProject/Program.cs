using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using Talabat.APIsProject.Errors;
using Talabat.APIsProject.Extensions;
using Talabat.APIsProject.Helper;
using Talabat.APIsProject.Middlewares;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Repositories;
using Talabat.Repository;
using Talabat.Repository.Data;
using Talabat.Repository.Identity;

namespace Talabat.APIsProject
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<StoreContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddDbContext<AppIdentityDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"))
            );
            builder.Services.AddSingleton<IConnectionMultiplexer>(option =>
            {
                var Connection = builder.Configuration.GetConnectionString("RedisConnection");
                return ConnectionMultiplexer.Connect(Connection);
            });
            //builder.Services.AddScoped(IGenericRepository<Product>, GenericRepository<Product>));
            builder.Services.AddApplicationServices();
            builder.Services.AddAuthentication(); // UserManager SignInManager RoleManager
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMiddleware<ExceptionsMiddleWares>();
                app.UseSwaggerMiddleWares();
            }
            app.UseStaticFiles();

            //app.UseStatusCodePagesWithRedirects("/errors/{0}");
            app.UseStatusCodePagesWithReExecute("/errors/{0}");

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            #region UpdateDataBase 

            using var Scope = app.Services.CreateScope();
            var Services = Scope.ServiceProvider;
            var LoggerFactory = Services.GetRequiredService<ILoggerFactory>();
            try
            {

                var DbContext = Services.GetRequiredService<StoreContext>();
                await DbContext.Database.MigrateAsync();
                //if (DbContext.Set<Product>().Count() == 0 ||
                //    DbContext.Set<ProductBrand>().Count() == 0 ||
                //    DbContext.Set<ProductType>().Count() == 0)

                var IdentityDbContext = Services.GetRequiredService<IdentityDbContext>();
                await IdentityDbContext.Database.MigrateAsync();

                var userManager = Services.GetRequiredService<UserManager<AppUser>>();
                await AppIdentityDbContextSeed.SeedUserAsync(userManager);
                await StoreContextSeed.SeedAsync(DbContext);
            }
            catch (Exception ex)
            {
                var Logger = LoggerFactory.CreateLogger<Program>();
                Logger.LogError(ex, "Error During make update database in program");
            }

            #endregion



            app.Run();
        }
    }
}
