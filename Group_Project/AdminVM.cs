using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows;
using System.Collections.ObjectModel;
using System.Xml.Linq;
using System.Threading.Tasks.Dataflow;

namespace Group_Project
{
    public partial class AdminVM : ObservableObject
    {
        [ObservableProperty]
        public string? userName;
        [ObservableProperty]
        public string? password;
        [ObservableProperty]
        public ObservableCollection<Student> students;
        [ObservableProperty]
        public Student selectedStudent = null;
        [ObservableProperty]
        public Module selectedModule;
        [ObservableProperty]
        public ObservableCollection<Module> smodules;
        [ObservableProperty]
        public string moduleName;
        [ObservableProperty]
        public int id;
        [ObservableProperty]
        public ObservableCollection<Module> selectStudentmodules;
        [ObservableProperty]
        public string newName;
        [ObservableProperty]
        public ObservableCollection<Result> results;
        [ObservableProperty]
        public int rank;
        [ObservableProperty]
        public string username;
        [ObservableProperty]
        public string user_image;
        [ObservableProperty]
        public double user_gpa;

        [ObservableProperty]
        public string result;
        public AdminVM(Student s)
        {
            id = s.Id;
            username = s.UserName;
            user_image = s.ImageURL;
            user_gpa = s.GPA;
            var db = new Student_Data_Context();
            var list = db.Modules.ToList();
            smodules = new ObservableCollection<Module>(list);
            results = new ObservableCollection<Result>();


            foreach (var result in db.Results)
            {
                if (result.studentId == s.Id)
                {
                    results.Add(result);
                }
            }

            db.SaveChanges();

      
            var list2 = db.Students.OrderByDescending(s => s.GPA).ToList();
            students = new ObservableCollection<Student>(list2);
            Student st = db.Students.FirstOrDefault(p => p.Id == id);
            rank = students.IndexOf(st) + 1;




        }
        public AdminVM()
        {
            var db = new Student_Data_Context();
            var list = db.Students.OrderByDescending(s => s.GPA).ToList();
            students = new ObservableCollection<Student>(list);
            Student s = db.Students.FirstOrDefault(p => p.Id == id);
            rank = students.IndexOf(s) + 1;
            
            var list2 = db.Modules.ToList();

            smodules = new ObservableCollection<Module>(list2);




        }
        public void CloseWindow()
        {
            // get the current window
            Window window = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive);

            // close the window
            window.Close();
        }







        [RelayCommand]
        public void AddModules()
        {
           var vm=new Module_Register_View_Model();
            var window = new Module_Register_window(vm);
            window.Show();


        }







        [RelayCommand]
        public void add_results(Object obj)

        {
            if (obj is Student student)
            {
                CloseWindow();
                var vm = new AdminVM(student);
                Results_Add_Window window = new Results_Add_Window(vm);
                window.Show();








            }



        }
        [RelayCommand]
        public void AdminAccess()
        {
            var db = new Student_Data_Context();
            bool fine = false;
            foreach (var main in db.MainAdmins)
            {
                if (userName == main.UserName && password == main.Password)
                {
                    fine = true;
                    Window window2 = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive);

                    // close the window
                    window2.Close();
                    var window = new MainWindow();
                    window.Show();
                }

            }
            if (fine == false)
            {
                MessageBox.Show("very bad");
            }

        }




        [RelayCommand]
        public void Result_Add( Object obj)
        {
            if(obj is Module module) {
                var db = new Student_Data_Context();
                Student s = db.Students.FirstOrDefault(p => p.Id == id);
                Module m = db.Modules.FirstOrDefault(p => p.Id == module.Id);
                foreach (var res in db.Results)
                {
                    if (m.ModuleName == res.Modulename && s.Id == res.studentId)
                    {
                        db.Results.Remove(res);
                        db.SaveChanges();
                    }
                }
                db.SaveChanges();
                if (module != null)
                {
                    char x = result[result.Length - 1];
                    module.ModuleResult = x.ToString();
                    MessageBox.Show("added result for selected module succesfully" + "\n result of  " + module.ModuleName + " = " + module.ModuleResult);



                }
                else
                {
                    MessageBox.Show("Select Module First");
                }
                
            }
           

        }
        [RelayCommand]
        public void saveResults()
        {
            var db = new Student_Data_Context();
            Student s = db.Students.FirstOrDefault(p => p.Id == id);
            if (selectedModule != null)
            {
                Module m = db.Modules.FirstOrDefault(p => p.Id == selectedModule.Id);
                foreach (var res in db.Results)
                {
                    if (m.ModuleName == res.Modulename && s.Id == res.studentId)
                    {
                        db.Results.Remove(res);
                        db.SaveChanges();
                    }
                }

                db.SaveChanges();
            }

    

        }

        [RelayCommand]
        public void CalResults()
        {
            double total = 0;
            double totalcredit = 0;
            bool check = true;
            foreach (var mod in smodules)
            {
                if (mod.ModuleResult != "A" && (mod.ModuleResult != "A+") && (mod.ModuleResult != "A-") && (mod.ModuleResult != "B+") && (mod.ModuleResult != "B") && (mod.ModuleResult != "B-") && (mod.ModuleResult != "C") && (mod.ModuleResult != "C-") && (mod.ModuleResult != "C+") && (mod.ModuleResult != "E"))
                {

                    check = false;
                    break;

                }
            }

            if (check)
            {
                foreach (var mod in smodules)
                {
                    if (mod.ModuleResult == "A+")
                    {
                        total = total + 4 * mod.Credit;
                        totalcredit=totalcredit+ mod.Credit;
                        

                    }
                    else if (mod.ModuleResult == "A")
                    {
                        total = total + 4 * mod.Credit;
                        totalcredit = totalcredit + mod.Credit;
                    }
                    else if (mod.ModuleResult == "A-")
                    {
                        total = total + 3.7 * mod.Credit;
                        totalcredit = totalcredit + mod.Credit;
                    }
                    else if (mod.ModuleResult == "B+")
                    {
                        total = total + 3.3 * mod.Credit;
                        totalcredit = totalcredit + mod.Credit;
                    }
                    else if (mod.ModuleResult == "B")
                    {
                        total = total + 3 * mod.Credit;
                        totalcredit = totalcredit + mod.Credit;
                    }
                    else if (mod.ModuleResult == "B-")
                    {
                        total = total + 2.7 * mod.Credit;
                        totalcredit = totalcredit + mod.Credit;
                    }
                    else if (mod.ModuleResult == "C+")
                    {
                        total = total + 2.3;
                        totalcredit = totalcredit + mod.Credit;
                    }
                    else if (mod.ModuleResult == "C")
                    {
                        total = total + 2 * mod.Credit;
                        totalcredit = totalcredit + mod.Credit;
                    }
                    else if (mod.ModuleResult == "C-")
                    {
                        total = total + 1.7 * mod.Credit;
                        totalcredit = totalcredit + mod.Credit;
                    }
                    else if (mod.ModuleResult == "E")
                    {
                        total = total + 0;
                        totalcredit = totalcredit + mod.Credit;
                    }
                    else
                    {
                        total = total + 0;
                        totalcredit = totalcredit + mod.Credit;
                    }
                }
                double gpa = total / totalcredit;
                Math.Round(gpa,2);
                var db = new Student_Data_Context();
                Student? s = db.Students.FirstOrDefault(p => p.Id == Id);
                if ( s!=null && gpa > 0)
                {
                    s.GPA = gpa;
                    user_gpa = gpa;
                    db.SaveChanges();
                    MessageBox.Show("Calculated The Result Succesfully");
                    MessageBox.Show(s.GPA.ToString(), "GPA OF " + s.Name);
                }
                else
                {
                    MessageBox.Show("GPA HAS NO VALUE");
                    return;
                }

                foreach (var mod in smodules)
                {

                    var database = new Student_Data_Context();
                    Result r = new Result();
                    r.value = mod.ModuleResult;
                    r.studentId = s.Id;
                    r.Modulename = mod.ModuleName;
                    database.Add(r);
                    database.SaveChanges();
                }


            }
            else
            {
                MessageBox.Show("Some results not added");
                return;
            }





        }










 
        [RelayCommand]
        public void ShowStudents()
        {
            var db = new Student_Data_Context();
            var list = db.Students.OrderByDescending(s => s.GPA).ToList();
            students = new ObservableCollection<Student>(list);
            Student s = db.Students.FirstOrDefault(p => p.Id == id);
            rank = students.IndexOf(s) + 1;

        }
        [RelayCommand]
        public void showModules()
        {
            var db = new Student_Data_Context();
            var list = db.Modules.OrderBy(x => x.Id).ToList();
            smodules = new ObservableCollection<Module>(list);

        }

        [RelayCommand]
        public void Access()
        {
            var db = new Student_Data_Context();
            bool fine = false;

            {
                
                foreach (var admin in db.Admins)
                {
                    if (password == admin.Password && userName == admin.UserName)
                    {

                        fine = true;
                        var adminWindow = new Admin_Window();
                        CloseWindow();
                        MessageBox.Show("Succesfully Logged");
                        adminWindow.Show();
                       

                    }
                  

                }
            }
            if(fine==false)
            {
                MessageBox.Show("Invalid Login");
            }



        }

        [RelayCommand]
        public void userLog()
        {
            var db = new Student_Data_Context();
            bool userFound = false;
            foreach (var student in db.Students)
            {
                if (password == student.Password && userName == student.UserName)
                {

                    userFound = true;



                }

            }
            if (userFound)
            {
                Student s = db.Students.SingleOrDefault(p => p.UserName == userName && p.Password == password);
                var vm = new AdminVM(s);
                var userWindow = new User_Window(vm);
                CloseWindow();
                MessageBox.Show("Succesfully Logged");
                userWindow.Show();


            }
            else
            {
                MessageBox.Show("Fill Details to Log");
                return;

            }
        }
        [RelayCommand]
        public void resultshow(object obj)
        {
            if (obj is Student student)
            {

                AdminVM vm = new AdminVM(student);
                var window = new ShowResultsWinodow(vm);
                window.Show();

            }

        }

        [RelayCommand]
        public void DeleteStudent()
        {
            var db = new Student_Data_Context();
            Student s = db.Students.SingleOrDefault(p => p.Id == id);
            Student k = students.SingleOrDefault(p => p.Id == id);

            if (s != null)
            {

                db.Students.Remove(s);
                students.Remove(k);


                db.SaveChanges();
                MessageBox.Show("Delete Student Correctly");
            }
            else
            {
                MessageBox.Show("USER UNAVAILABLE");
                return;
            }

        }
        [RelayCommand]
        public void EditStudent()
        {
            var db = new Student_Data_Context();
            Student s = db.Students.SingleOrDefault(p => p.Id == id);
            Student k = students.SingleOrDefault(p => p.Id == id);
            if (s != null)
            {
                CloseWindow();
                var vm = new StudentVM(s);
                var window = new User_Registration_Window(vm);
                window.Show();






            }
            else
            {
                MessageBox.Show("USER HAS ALREADY REMOVED");
            }




        }
        [RelayCommand]
        public void EditSeletctStudent(object obj)
        {
            if(obj is Student student)
            {
                var vm = new StudentVM(student);
                var window = new User_Registration_Window(vm);
                window.Show();

            }

        }
        [RelayCommand]
        public void addStudent()
        {
            
            var vm = new StudentVM();
            var window = new User_Registration_Window(vm);
            window.Show();


        }
        [RelayCommand]
        public void ModuleViewWindowOpen()
        {
            var window = new ModuleViewWindow();
            window.Show();
        }
        [RelayCommand]
        public void updateModules()

        {
            if (selectedModule != null)
            {
                var db = new Student_Data_Context();
                selectedModule.ModuleName = newName;
                Module m = db.Modules.SingleOrDefault(p => p.Id == selectedModule.Id);

                if (newName != null)
                {
                    m.ModuleName = newName;
                    db.Remove(m);
                    db.SaveChanges();
                    db.Add(m);
                    db.SaveChanges();
                    MessageBox.Show("Succesfully Edited The Module");
                    CloseWindow();

                    var window = new ModuleViewWindow();
                    window.Show();
                }
                else
                {
                    MessageBox.Show("Error is there");
                }
            }
            else
            {
                MessageBox.Show("First Select a module");

            }


        }
        [RelayCommand]
        public void DeleteAnyUser(Object obj)
        {
            var db = new Student_Data_Context();

            if (obj is Student selectstudent)
            {
                db.Students.Remove(selectstudent);
                students.Remove(selectstudent);
                db.SaveChanges();
                MessageBox.Show("Delete Student Correctly");
            }


        }
        [RelayCommand]
        public void deleteModule()
        {
            if (selectedModule != null)
            {
                var db = new Student_Data_Context();
                MessageBox.Show("HRI");
                Module m = db.Modules.SingleOrDefault(p => p.Id == selectedModule.Id);
                db.Remove(m);
                db.SaveChanges();
                smodules.Remove(selectedModule);

            }
            else
            {
                MessageBox.Show("First Select a module");
            }
        }
    }
}

