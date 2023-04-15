using Microsoft.AspNetCore.Mvc;
using SecuritySystem.Pages;

namespace SecuritySystem;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        // Add services to the container.
        builder.Services.AddRazorPages();
        // we need to set singletons so the app can share class data

        builder.Services.AddDistributedMemoryCache();
        builder.Services.AddSession(options =>
        {
            options.IdleTimeout = TimeSpan.FromMinutes(10);
            options.Cookie.HttpOnly = true;
            options.Cookie.IsEssential = true;
        });
        builder.Services.Configure<CookieTempDataProviderOptions>(options =>
        {
            options.Cookie.IsEssential = true;
        });
        builder.Services.AddControllersWithViews().AddSessionStateTempDataProvider();
        var app = builder.Build();
        // test austin
        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

  

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();
        app.MapControllers();
        app.UseAuthorization();
        app.UseSession();
        app.MapRazorPages();

        app.Run();
    }

    

}
