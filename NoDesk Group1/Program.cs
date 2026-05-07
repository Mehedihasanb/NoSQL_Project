using MongoDB.Driver;
using NoDesk_Group1.Repositories.Interfaces;

namespace NoDeskGroup1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Load .env from the solution/project folder (so Mongo__ConnectionString is available)
            DotNetEnv.Env.TraversePath().Load();

            var builder = WebApplication.CreateBuilder(args);

            // Mongo client (singleton) reads connection string from .env as "Mongo:ConnectionString"
            builder.Services.AddSingleton<IMongoClient>(sp =>
            {
                var conn = builder.Configuration["Mongo:ConnectionString"]; // mapped from Mongo__ConnectionString
                if (string.IsNullOrWhiteSpace(conn))
                    throw new InvalidOperationException("Mongo:ConnectionString is missing from .env");
                return new MongoClient(conn);
            });

            // Database (scoped) reads DB name from appsettings.json at Mongo:Database
            builder.Services.AddScoped(sp =>
            {
                var client = sp.GetRequiredService<IMongoClient>();
                var dbName = builder.Configuration["Mongo:Database"]; // e.g., NoDeskDB
                if (string.IsNullOrWhiteSpace(dbName))
                    throw new InvalidOperationException("Mongo:Database is missing from appsettings.json");
                return client.GetDatabase(dbName);
            });

            builder.Services.AddControllersWithViews();

            builder.Services.AddScoped<IEmployeeRepository, NoDeskGroup1.Repositories.EmployeeRepository>();
            builder.Services.AddScoped<ITicketRepository, NoDeskGroup1.Repositories.TicketRepository>();


            var app = builder.Build();
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.Run();
        }
    }
}
