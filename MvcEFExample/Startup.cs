using Microsoft.EntityFrameworkCore;
using MvcEFExample.Models;
using System.Configuration;
using MvcEFExample.Connections;

namespace MvcEFExample
{
    public class Startup
    {
        public IConfiguration configRoot
        {
            get;
        }
        public Startup(IConfiguration configuration)
        {
            configRoot = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = configRoot.GetConnectionString("InventoryDatabase");//Configuration.GetConnectionString("InventoryDatabase");
            services.AddDbContextPool<InventoryContext>(option => option.UseSqlServer(connectionString));
            services.AddRazorPages();
        }
        public void Configure(WebApplication app, IWebHostEnvironment env)
        {
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            //app.MapRazorPages();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Products}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
            app.Run();
        }
    }
}
