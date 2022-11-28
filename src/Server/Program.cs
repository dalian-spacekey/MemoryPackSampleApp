using MemoryPack.AspNetCoreMvcFormatter;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using SampleApp.Server.Database;
using SampleApp.Server.Database.Entities;
using SampleApp.Server.Services;
using SampleApp.Shared;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options =>
{
    options.InputFormatters.Insert(0, new MemoryPackInputFormatter());
    options.OutputFormatters.Insert(0, new MemoryPackOutputFormatter(true));
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("MyPolicy", policy =>
    {
        policy
            .WithOrigins("http://127.0.0.1:5500", "http://localhost:3000")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials()
            .Build();
    });
});

builder.Services.AddDbContextFactory<SampleDbContext>((_, options) =>
{
    var path = Path.Join(Path.GetDirectoryName(Assembly.GetEntryAssembly()!.Location), "Data\\sample.db");
    options.UseSqlite($"Data source={path}");
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("MyPolicy");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.UseMiddleware<StopwatchMiddleware>();

var factory = app.Services.GetRequiredService<IDbContextFactory<SampleDbContext>>();
await using var db = await factory.CreateDbContextAsync();
await db.Database.MigrateAsync();

await db.ImportAsync();

app.Run();
