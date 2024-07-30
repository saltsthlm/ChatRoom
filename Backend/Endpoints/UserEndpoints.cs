using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Models;

namespace Backend.Endpoints;

public static class UserEndpoints
{
    public static void SetupEndpoints(WebApplication app)
    {

        app.MapPost("/users", async (User user, ChatRoomDatabaseContext db) =>
        {
            Console.WriteLine($"POST at /users: {user}");
            db.Add(user);
            await db.SaveChangesAsync();
            return Results.Created();
        });

        app.MapGet("/users/{id}", (int id, ChatRoomDatabaseContext db) =>
        {
            Console.WriteLine($"GET at /users/{id}");
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

        app.MapGet("/users", (ChatRoomDatabaseContext db) =>
        {
            Console.WriteLine($"GET at /users");
            return Results.Ok(db.Messages);
        });

        app.MapPut("/users/{id}", async (int id, User user, ChatRoomDatabaseContext db) =>
        {
            Console.WriteLine($"DELETE at /messages/{id}");

            try
            {
                db.Users.Update(user);

                var dbUser = db.Users.First(data => data!.Id == id);

                dbUser.Id = user.Id;
                dbUser.Alias = user.Alias;

                await db.SaveChangesAsync();

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

        app.MapDelete("/users", (ChatRoomDatabaseContext db) =>
        {
            Console.WriteLine($"DELETE at /users");

            db.Remove(db.Users);
            return Results.Ok();
        });
    }
}