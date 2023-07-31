using BlazorApp;
using BlazorApp.Server;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<IWeatherService, ServerWeatherService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.MapWhen(ctx => ctx.Request.Path.StartsWithSegments("/webassembly"), app1 =>
{
    app1.UseBlazorFrameworkFiles("/webassembly");
    app1.UseRouting();
    app1.UseEndpoints(endpoints =>
    {
        endpoints.MapFallbackToFile("/webassembly/{*path:nonfile}", "/webassembly/index.html");
    });
});

app.UseRouting();

app.MapGet("/weather", async (IWeatherService weatherService) => await weatherService.GetForecastAsync());
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
