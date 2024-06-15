using EntitySearch.Core;
using EntitySearch.EfCore;
using EntitySearch.Example;
using EntitySearch.Example.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();


builder.Services.AddDbContextFactory<ExampleDbContext>(
        options => options.UseInMemoryDatabase("Example"));

builder.Services.AddEntitySearch().WithEfCore<ExampleDbContext>();

builder.Services.AddScoped<TodoSearch>();

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

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
