using NSwag.AspNetCore;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ChatRoomDatabaseContext>(options => options.UseSqlite("ChatRoomAPI"));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApiDocument(config =>
{
    config.DocumentName = "ChatRoomAPI";
    config.Title = "ChatRoomAPI v1";
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

app.MapPost("/users", async (User user, ChatRoomDatabaseContext db) =>
{
    Console.WriteLine($"POST at {"/users"}: user");
    db.Add(user);
    await db.SaveChangesAsync();
    return Results.Created();
});

app.MapGet("/user/{id}", (int id, ChatRoomDatabaseContext db) =>
{
    Console.WriteLine($"GET at {"/users/" + id}");
    try
    {
        User user = db.Users.First(data => data!.Id == id);
        return Results.Ok(user);
    }
    catch (Exception)
    {
        return Results.NotFound();
    }
});

app.MapDelete("/users/{id}", async (int id, ChatRoomDatabaseContext db) =>
{
    Console.WriteLine($"DELETE at /users/{id}");
    try
    {
        User user = db.Users.First(data => data!.Id == id);
        db.Remove(user);
        await db.SaveChangesAsync();
        return Results.Accepted();
    }
    catch (Exception)
    {
        return Results.NotFound();
    }
});

app.Run();
