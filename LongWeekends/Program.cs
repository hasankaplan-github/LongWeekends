using Haskap.DddBase.Domain.Common;
using Haskap.DddBase.Infra;
using LongWeekends;
using Microsoft.AspNetCore.Mvc;
using Modules.GlobalExceptionHandling.Presentation;

var builder = WebApplication.CreateBuilder(args);

Locale.DefinedLocales.TrTr = new Locale("tr-TR");
Locale.DefinedLocales.EnUs = new Locale("en-US");


// Add services to the container.
builder.Services.AddModules(builder.Configuration);
builder.Services.AddBaseInfra();

builder.Services.AddLocalization();

builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
});

builder.Services.AddExceptionHandler<DefaultExceptionHandler>();

builder.Services.AddHsts(options =>
{
    options.Preload = false;
    options.MaxAge = TimeSpan.FromMinutes(5);
});

var app = builder.Build();

app.UseExceptionHandler("/Home/Error");
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    //app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.UseRequestLocalization((string)Locale.DefinedLocales.TrTr.Value);

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
