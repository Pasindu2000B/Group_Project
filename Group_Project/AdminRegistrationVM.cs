using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;

namespace Group_Project
{
    public partial class AdminRegistrationVM : ObservableObject
    {
        [ObservableProperty]
        public int id;
        [ObservableProperty]
        public string name;
        [ObservableProperty]
        public string user_name;
        [ObservableProperty]
        public string password;
        [ObservableProperty]
        public string module;
        [ObservableProperty]
        public Admin s;
        [ObservableProperty]
        public ObservableCollection<Module> modules;

        public AdminRegistrationVM() {
           
           var db=new Student_Data_Context();
            var list=db.Modules.ToList();
            modules= new ObservableCollection<Module>(list);

                if (s != null)
                {
                id = s.Id;
                name = s.Name;
                user_name = s.UserName;
                password = s.Password;
                module = s.Module;
                 
                }
             

            



        }
        public AdminRegistrationVM(Admin admin)
        {
            
            var db = new Student_Data_Context();
            var list = db.Modules.ToList();
            if (admin != null)
            {
                id = admin.Id;
                name = admin.Name;
                user_name = admin.UserName;
                password = admin.Password;
                module = admin.Module;
            }
            else
            {
                MessageBox.Show("hukhn");
                return;
            }

        }
        public void CloseWindow()
        {
            // get the current window
            Window window = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive);

            // close the window
            window.Close();
        }



        [RelayCommand]
        public void Save()
        {
            bool fine = true;
            var dx = new Student_Data_Context();
            foreach (var stu in dx.Admins)
            {
                if (user_name == stu.UserName)
                {
                    fine = false;
                    break;
                }
            }
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(user_name) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(module) )
            {
                MessageBox.Show("spaces cannot be empty.", "Error");
                return;
            }

            if (!Regex.IsMatch(name, @"^[a-zA-Z\s]+$"))
            {
                MessageBox.Show("First name and last name must contain only letters and spaces.", "Error");
                return;
            }







            if (name != null && user_name != null && password != null )
            {
                var db = new Student_Data_Context();


                if (id != 0)
                {
                    s = db.Admins.FirstOrDefault(x => x.Id == id);
                }
                else
                {

                }

                if (s== null)
                {
                    if (fine)
                    {


                        s = new Admin()
                        {
                            Name = name,
                            Password = password,
                            UserName = user_name,
                            Module= module,

                            
                        };

                        db.Admins.Add(s);
                        db.SaveChanges();
                        
                        MessageBox.Show("SUCESSFLLY ADDED");
                        CloseWindow();
                    }
                    else
                    {
                        MessageBox.Show("UserName Not available");
                    }

                }
                else
                {

                   

                 
                    s.UserName = user_name;
                    s.Password = password;
                    s.Name = name;
                   

                    db.Admins.Remove(s);
                    db.SaveChanges();
                    db.Admins.Add(s);
                    db.SaveChanges();
                    
                    MessageBox.Show("Succesfully Edit Your Profile ");

                  










                }


            }

            else
            {
                MessageBox.Show(" You did't fill all boxes");
                return;
            }





        }



    }
}
