using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Group_Project
{
    public class Student_Data_Context : DbContext
    {
        public string path = @"C:\Users\Asus\Desktop\New folder\Group_Project (5)\Group_Project\Group_Project.Tests\FirstDatabase.db";

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite($"Data Source = {path}");
        }

        public DbSet<Admin> Admins { get; set; }

        public DbSet<MainAdmin> MainAdmins { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<Result> Results { get; set; }

        

    }
    

    
}
