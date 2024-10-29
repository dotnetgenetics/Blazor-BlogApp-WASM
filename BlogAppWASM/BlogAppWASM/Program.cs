using BlogAppWASM.Components;
using BlogAppWASM.Data;
using BlogAppWASM.Services;
using BlogAppWASM.Shared;
using Microsoft.EntityFrameworkCore;

namespace BlogAppWASM
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<BlogDbContext>(options =>
                options.UseSqlite("Data Source=blazingblog.db"));
            builder.Services.AddSingleton<BlogStateService>();
            builder.Services.AddSingleton<IBlogDbContextFactory, BlogDbContextFactory>();
            builder.Services.AddScoped<IBlogRepository, BlogRepository>();

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents()
                .AddInteractiveWebAssemblyComponents();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseAntiforgery();

            //handle API calls
            app.MapBlogApi();

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode()
                .AddInteractiveWebAssemblyRenderMode()
                .AddAdditionalAssemblies(typeof(Client._Imports).Assembly);

            app.Run();
        }
    }
}
