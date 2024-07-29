using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Backend.Models
{
    public class ChatRoomDatabaseContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }

        public string DbPath { get; }
        private string _databaseFileName = "custom.db";

        public ChatRoomDatabaseContext(DbContextOptions<ChatRoomDatabaseContext> options)
        : base(options)
        {
            DbPath = System.IO.Path.Join(Environment.CurrentDirectory, "Data", _databaseFileName);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");
    }
}