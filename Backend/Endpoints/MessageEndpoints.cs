using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Models;

namespace Backend.Endpoints;

public static class MessageEndpoints
{
    public static void SetupEndpoints(WebApplication app)
    {

        app.MapPost("/messages", async (Message message, ChatRoomDatabaseContext db) =>
        {
            Console.WriteLine($"POST at /messages: {message}");
            db.Add(message);
            await db.SaveChangesAsync();
            return Results.Created();
        });

        app.MapGet("/messages/{id}", (int id, ChatRoomDatabaseContext db) =>
        {
            Console.WriteLine($"GET at /messages/{id}");
            try
            {
                var message = db.Messages.First(data => data!.Id == id);
                return Results.Ok(message);
            }
            catch (Exception)
            {
                return Results.NotFound();
            }
        });

        app.MapGet("/messages", (ChatRoomDatabaseContext db) =>
        {
            Console.WriteLine($"GET at /messages");
            return Results.Ok(db.Messages);
        });

        app.MapPut("/messages/{id}", async (int id, Message message, ChatRoomDatabaseContext db) =>
        {
            Console.WriteLine($"DELETE at /messages/{id}");

            try
            {
                db.Messages.Update(message);

                var dbMessage = db.Messages.First(data => data!.Id == id);

                dbMessage.Id = message.Id;
                dbMessage.Content = message.Content;
                dbMessage.SendDate = message.SendDate;

                await db.SaveChangesAsync();

                return Results.Ok(message);
            }
            catch (Exception)
            {
                return Results.NotFound();
            }
        });

        app.MapDelete("/messages/{id}", async (int id, ChatRoomDatabaseContext db) =>
        {
            Console.WriteLine($"DELETE at /messages/{id}");
            try
            {
                var message = db.Users.First(data => data!.Id == id);
                db.Remove(message);
                await db.SaveChangesAsync();
                return Results.Accepted();
            }
            catch (Exception)
            {
                return Results.NotFound();
            }
        });

        app.MapDelete("/messages", (ChatRoomDatabaseContext db) =>
        {
            Console.WriteLine($"DELETE at /users");

            db.Remove(db.Users);
            return Results.Ok();
        });
    }
}