using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Group_Project
{
    /// <summary>
    /// Interaction logic for Start_Window.xaml
    /// </summary>
    public partial class Start_Window : Window
    {
        public Start_Window()
        {
            InitializeComponent();
           

        }

    

        private void userlogclk(object sender, RoutedEventArgs e)
        {

            var StudentLogin = new User_Login_Window();
            StudentLogin.Show();

        }

        private void adminclk(object sender, RoutedEventArgs e)
        {
            var adminlog=new Admin_Login_Window();
            adminlog.Show();
        }

      

        private void mini(object sender, RoutedEventArgs e)
        {
            
            WindowState = WindowState.Minimized;

        }

        private void clsclk(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void mainadminclk(object sender, RoutedEventArgs e)
        {
            var mainadminlg=new MainAdminLoginWindow();
            mainadminlg.Show();
        }
    }
}
