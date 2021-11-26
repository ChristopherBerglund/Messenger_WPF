using Microsoft.EntityFrameworkCore;
using MyMailer_WPF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMailer_WPF.Data
{
    public class MContext : DbContext
    {
        public DbSet<User> users { get; set; }
        public DbSet<Mail> mails { get; set; }

        private const string connectionString = "server=localhost;userid=root;password=Hejsan123!;database=mailer;";
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }
    }
}
