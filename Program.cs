using Microsoft.EntityFrameworkCore;
using Store_Manager.Components;
using Store_Manager.Data;
using Store_Manager.Data.Entities;
using Store_Manager.Services.ChainService;
using Store_Manager.Services.StoreService;
using System.Text.RegularExpressions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
        x => x.MigrationsHistoryTable("__EFMigrationsHistory", "dbo")));

builder.Services.AddScoped<IChainService, ChainService>();
builder.Services.AddScoped<IStoreService, StoreService>();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// Seed data
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    //var fGroup = new Chain
    //{
    //    Id = Guid.NewGuid(),
    //    Name = "fo-Group",
    //    CreatedOn = DateTime.UtcNow
    //};
    //db.Chains.Add(fGroup);
    //db.SaveChanges();

    if (!db.Chains.Any() && !db.Stores.Any())
    {
        var foGroup = new Chain
        {
            Id = Guid.NewGuid(),
            Name = "Fo-Group",
            CreatedOn = DateTime.UtcNow
        };

        db.Chains.Add(foGroup);

        db.Stores.AddRange(
            new Store
            {
                Id = Guid.NewGuid(),
                Number = 1,
                Name = "Opti View",
                CreatedOn = DateTime.UtcNow
            },
            new Store
            {
                Id = Guid.NewGuid(),
                Number = 2,
                Name = "FocalPoint",
                CreatedOn = DateTime.UtcNow,
                Chain = foGroup
            },
            new Store
            {
                Id = Guid.NewGuid(),
                Number = 3,
                Name = "Focus Optics",
                CreatedOn = DateTime.UtcNow,
                Chain = foGroup
            },
            new Store
            {
                Id = Guid.NewGuid(),
                Number = 4,
                Name = "ClearSight",
                CreatedOn = DateTime.UtcNow
            }
        );

        db.SaveChanges();
    }
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
