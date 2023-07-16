using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group_Project
{
    public class Result
    {
       [Key] public int key { get; set; }

        public string Modulename { get; set; }    
        public string value { get; set; }
        public int studentId { get; set; }
        public Result() { }
    }

}
