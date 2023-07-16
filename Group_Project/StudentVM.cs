using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.EntityFrameworkCore;
using System.Windows;
using Group_Project.Migrations;
using System.Collections.ObjectModel;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using System.Text.RegularExpressions;

namespace Group_Project
{
    public partial class StudentVM : ObservableObject
    {
        [ObservableProperty]
        public string name;
        [ObservableProperty]
        public string user_name;
        [ObservableProperty]
        public string password;
        [ObservableProperty]
        public ObservableCollection<Student> students;
        [ObservableProperty]
        public int id;
        [ObservableProperty]
        public Student student;
        [ObservableProperty]
        public Student s;
       
        [ObservableProperty]
        public string imageURL;

        
     

        public StudentVM()
        {
            Show();
        }
        public StudentVM(Student s)
        {
            if (s != null)
            {
                id = s.Id;
                
               name= s.Name;
                user_name=s.UserName;
                password=s.Password;
                imageURL= s.ImageURL;
                Show();
            }
            else
            {
                Show();
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
        public void Show()
        {
            var db = new Student_Data_Context();
            var list= db.Students.OrderBy(x => x.GPA).ToList();
            students= new ObservableCollection<Student>(list);
        }
        [RelayCommand]
        public void UploadPhoto()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Image files | *.bmp; *.png; *.jpg";
            dialog.FilterIndex = 1;
            if (dialog.ShowDialog() == true)
            {
                imageURL= dialog.FileName;
                MessageBox.Show("Imgae successfuly uploded!", "successfull");
               

            }
        }
        [RelayCommand]
        public void previewImage()
        {
           
        }


        [RelayCommand]
        public void Save()
        {
            bool fine = true;
           var dx= new Student_Data_Context();
            foreach(var stu in dx.Students)
            {
                if(user_name==stu.UserName)
                {
                    fine= false;
                    break;
                }
            }
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(user_name) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("spaces cannot be empty.", "Error");
                return;
            }

            if (!Regex.IsMatch(name, @"^[a-zA-Z\s]+$"))
            {
                MessageBox.Show("First name and last name must contain only letters and spaces.", "Error");
                return;
            }







            if (name != null && user_name != null && password != null && imageURL!=null ) { 
                var db = new Student_Data_Context();


                if (id != 0)
                {
                    student = db.Students.FirstOrDefault(x => x.Id == id);
                }
                else
                {
                   
                }

                if (student == null )
                {
                    if (fine)
                    {


                        student = new Student()
                        {
                            Name = name,
                            Password = password,
                            UserName = user_name,
                            ImageURL = imageURL,
                        };

                        db.Students.Add(student);
                        db.SaveChanges();
                        students.Add(student);
                        MessageBox.Show("SUCESSFULLY ADDED");
                        CloseWindow();
                        var window = new Admin_Window();
                        window.Show();
                    }
                    else
                    {
                        MessageBox.Show("UserName Not available");
                    }
                  
                }
                else
                {
                   
                        s = students.FirstOrDefault(x => x.Id == id);
                       
                        student.Name = name;
                        student.Password = password;
                        student.UserName = user_name;
                    student.ImageURL = imageURL;
                    s.UserName = user_name;
                    s.Password=password;
                    s.Name=name;
                    s.ImageURL = imageURL;
                     
                        db.Students.Remove(student);
                        db.SaveChanges();
                        db.Students.Add(student);
                        db.SaveChanges();
                        int index=students.IndexOf(s);
                       students.RemoveAt(index);
                       students.Insert(index, s);
                    MessageBox.Show("Succesfully Edit Your Profile ");
                    CloseWindow();
                    var vm = new AdminVM(student);
                    var windw = new User_Window(vm);
                    windw.Show();
                    









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
