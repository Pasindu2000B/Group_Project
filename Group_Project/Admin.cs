using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group_Project
{
    public class Admin
    {
        public int Id { get; set; }
        public string Name { get; set; }

        

        

        public string UserName { get; set; }
        public string Password { get; set; }

        public Admin() { }

        public Admin(int id, string userName, string password)
        {
            Id = id;
            UserName = userName;
            Password = password;
        }
    }
}
