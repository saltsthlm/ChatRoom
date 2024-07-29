using NSwag.AspNetCore;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<CustomDatabaseContext>(options => options.UseSqlite("CustomAPI"));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApiDocument(config =>
{
    config.DocumentName = "CustomAPI";
    config.Title = "CustomAPI v1";
    config.Version = "v1";
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUi(config =>
    {
        config.DocumentTitle = "CustomAPI";
        config.Path = "/swagger";
        config.DocumentPath = "/swagger/{documentName}/swagger.json";
        config.DocExpansion = "list";
    });
}

app.UseCors("AllowAll");

app.MapPost("/custom-data-type", async (CustomDataType data, CustomDatabaseContext db) =>
{
    Console.WriteLine("Posting data");
    db.Add(data);
    await db.SaveChangesAsync();
    return Results.Created();
});

app.MapGet("/custom-data-type/{id}", (int id, CustomDatabaseContext db) =>
{
    try
    {
        CustomDataType entity = db.CustomData.First(data => data!.Id == id);
        return Results.Ok(entity);
    }
    catch (Exception)
    {
        return Results.NotFound();
    }
});

app.MapPut("/custom-data-type/{id}", async (int id, CustomDataType data, CustomDatabaseContext db) =>
{
    var dataFromDb = db.CustomData.First(data => data.Id == id);
    db.Update(dataFromDb);
    //update properties on dataFromDb
    await db.SaveChangesAsync();
    return Results.Accepted();
});

app.MapDelete("/custom-data-type/{id}", async (int id, CustomDatabaseContext db) =>
{
    try
    {
        CustomDataType entity = db.CustomData.First(data => data!.Id == id);
        db.Remove(entity);
        await db.SaveChangesAsync();
        return Results.Accepted();
    }
    catch (Exception)
    {
        return Results.NotFound();
    }
});

app.Run();
