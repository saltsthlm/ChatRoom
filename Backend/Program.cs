using NSwag.AspNetCore;
using Backend.Models;
using Microsoft.EntityFrameworkCore;
using Backend.Endpoints;

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

UserEndpoints.SetupEndpoints(app);

app.Run();
