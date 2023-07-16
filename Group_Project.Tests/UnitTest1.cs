using FluentAssertions;
using Group_Project;
using System.Collections.ObjectModel;

namespace Group_Project.Tests
{
    public class UnitTest1
    {
        public UnitTest1()
        {
            Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT","TESTING");
        }
        [Fact]
        public void NAMESHOULDBETHERE()
        {
            var vm=new StudentVM();
           
            vm.user_name = "caa";
            vm.imageURL = "aaa";
            vm.name = "name";
            vm.password = "password";
            vm.Save();
            var student=new Student();
            student.Name = "name";
            var db = new Student_Data_Context();
            using (db)
            {
                db.Database.EnsureCreated();
                db.Students.Any(p=>p.Name == student.Name).Should().BeTrue();
                var pat=db.Students.First();
                db.Students.Remove(pat);
                db.SaveChanges();
            }



        }
        [Fact]
        public void GpaShouldbezero()
        {
            var adminVM = new AdminVM();
            adminVM.id = 1;
            adminVM.smodules = new ObservableCollection<Module>
        {
            new Module { ModuleResult = "A", Credit = 3 },
            new Module { ModuleResult = "A", Credit = 3 },
        
        };
            adminVM.CalResults();
            var db=new Student_Data_Context();
            Student s=db.Students.FirstOrDefault(s=>s.Id == adminVM.id);
            using (db)
            {
                db.Database.EnsureCreated();
                s.GPA.Should().Be(4);
                var pat = db.Students.First();
                db.Students.Remove(pat);
                db.SaveChanges();
            }

        }
        [Fact]
        public void GpaShouldbeone()
        {
            var adminVM = new AdminVM();
            adminVM.id = 1;
            adminVM.smodules = new ObservableCollection<Module>
        {
            new Module { ModuleResult = "A", Credit = 3 },
            new Module { ModuleResult = "E", Credit = 3 },

        };
            adminVM.CalResults();
            var db = new Student_Data_Context();
            Student s = db.Students.FirstOrDefault(s => s.Id == adminVM.id);
            using (db)
            {
                db.Database.EnsureCreated();
                s.GPA.Should().Be(2);
               
            }

        }

    }
}