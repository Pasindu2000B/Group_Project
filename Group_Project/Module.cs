using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group_Project
{
    public class Module
    {
        [Key] public int Id { get; set; }
        public string ModuleName { get; set; }

        public int Credit { get; set; }

        public string ModuleResult { get; set; }

        public Module() { }

        public Module(int id, string moduleName, string moduleResult, int credit)
        {
            Id = id;
            ModuleName = moduleName;
            ModuleResult = moduleResult;
            Credit = credit;
        }
    }
}
