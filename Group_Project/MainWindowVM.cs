using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Windows;

namespace Group_Project
{
    public partial class MainWindowVM : ObservableObject
    {
        [ObservableProperty]
        public ObservableCollection<Admin> admins;
        [ObservableProperty]
        public Admin selectAdmin;
        [ObservableProperty]
        public string modName;
        [ObservableProperty]
        public int modCredit;

        [RelayCommand]
        public void ShowAdmins()
        {

            var db = new Student_Data_Context();
            var list = db.Admins.OrderByDescending(s => s.Id).ToList();
            admins = new ObservableCollection<Admin>(list);
           

        }

        public MainWindowVM() {
            ShowAdmins();
         
        }
        [RelayCommand]
        public void savemodule(Module mod)
        {
            var db=new Student_Data_Context();
            if(mod == null)
            {
                Module m=new Module();
                m.ModuleName= modName;
                m.Credit= modCredit;
                db.Add(m);
                db.SaveChanges();

            }
            else
            {
                mod.ModuleName = modName;
                mod.Credit= modCredit;
                db.Remove(mod);
                db.SaveChanges();
                db.Add(mod);
                db.SaveChanges();
            }
        }
        [RelayCommand]
        public void EditAdmin(Object obj)
        {
         if(obj is Admin admin)
            {
                var vm = new AdminRegistrationVM(admin);
                var window = new AdminRegistrationWindow(vm);
                window.Show();
            }

       

        }

        [RelayCommand]
        public void DeleteAdmin(object obj)
        {
            if(obj is Admin admin)
            {
                var db=new Student_Data_Context();
                db.Admins.Remove(admin);
                db.SaveChanges();
                admins.Remove(admin);
                MessageBox.Show("Succesfully Deleted");

            }
        }
    }
}
