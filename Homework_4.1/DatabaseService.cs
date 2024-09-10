using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_4._1
{
    public class DatabaseService
    {
        DbContextOptions<ApplicationContext> options;
        public void EnsurePopulated()
        {

            var builder = new ConfigurationBuilder();
            // установка пути к текущему каталогу
            builder.SetBasePath(Directory.GetCurrentDirectory());
            // получаем конфигурацию из файла appsettings.json
            builder.AddJsonFile("appsettings.json");
            // создаем конфигурацию
            var config = builder.Build();
            // получаем строку подключения
            string connectionString = config.GetConnectionString("DefaultConnection");


            var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
            options = optionsBuilder.UseSqlServer(connectionString).Options;

            using (ApplicationContext db = new ApplicationContext(options))
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();

                List<User> users = new List<User>
                {
                    new User{Email = "alex@gmail.com", UserSettings = new UserSettings { Country = "Usa", City = "Chicago"}},
                    new User{Email = "mary@gmail.com", UserSettings = new UserSettings { Country = "Brazil", City = "Salvador"}}
                };
                db.Users.AddRange(users);
                db.SaveChanges();
            }

        }

        public void GetUserSettings(int id)
        {
            using (ApplicationContext db = new ApplicationContext(options))
            {
                User currentUser = db.Users.Include(e => e.UserSettings).FirstOrDefault(e => e.Id == id);
            }
        }

        public void RemoveUser(int id)
        {
            using (ApplicationContext db = new ApplicationContext(options))
            {
                User currentUser = db.Users.Include(e => e.UserSettings).FirstOrDefault(e => e.Id == id);
                db.Users.Remove(currentUser);
                db.SaveChanges();
            }
        }

    }
}

