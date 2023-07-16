using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group_Project
{
    public class Student
    {
        [Key] public int Id { get; set; }
        public string Name { get; set; }

        public string UserName { get; set; }
        public string Password { get; set; }

      
        public double GPA { get; set; }

        public string ImageURL { get; set; }
       
       
       


       

        public Student()
        {
          
        }
        public Student(int id, string name, string userName, string password, double gPA)
        {
            Id = id;
            Name = name;
            UserName = userName;
            Password = password;
            GPA = gPA;
        }

    }
}
