using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CommunityToolkit.Mvvm.Collections;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows;
using System.Reflection;

namespace Group_Project
{
    public partial class Module_Register_View_Model : ObservableObject
    {
        [ObservableProperty]
        public string moduleName;
        [ObservableProperty]
        public int credit;
        [ObservableProperty]
        public Module m;

        public Module_Register_View_Model()
        {
            
        }
        public Module_Register_View_Model(Module module)
        {
            moduleName = module.ModuleName;
            credit = module.Credit;
            m= module;
        }
        [RelayCommand]
        public void Save()
        {
            if(m==null)
            {
                Module module = new Module();
                module.ModuleName= moduleName;
                module.Credit= credit;
                module.ModuleResult = "-";

                
                var db = new Student_Data_Context();
                db.Modules.Add(module);
                db.SaveChanges();
                MessageBox.Show("Succesfully Added");
            }
            else
            {
                m.ModuleName= moduleName;
                m.Credit= credit;
                var db = new Student_Data_Context();
                foreach(Module mo in db.Modules)
                {
                    if( m.Credit == mo.Credit && m.ModuleName==mo.ModuleName)
                    {
                        db.Modules.Remove(mo); db.SaveChanges();
                    }
                }
                db.Modules.Add(m);
                db.SaveChanges();

            }
        }
    }
}
