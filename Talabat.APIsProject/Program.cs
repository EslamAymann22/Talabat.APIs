using Microsoft.EntityFrameworkCore;
using Talabat.Core.Entities;
using Talabat.Repository.Data;

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
            //builder.Services.AddScoped(IGenericRepository<T>)
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

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
