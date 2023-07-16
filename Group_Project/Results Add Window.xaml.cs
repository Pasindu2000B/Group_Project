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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Group_Project
{
    
        /// <summary>
        /// Interaction logic for Results_Add_Window.xaml
        /// </summary>
        public partial class Results_Add_Window : Window
        {
            public Results_Add_Window( AdminVM vm  )
            {
            
                InitializeComponent();
                DataContext = vm;
            
            }

        
        private void mini(object sender, RoutedEventArgs e)
        {

            WindowState = WindowState.Minimized;

        }

        private void clsclk(object sender, RoutedEventArgs e)
        {
            Window window = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive);

            // close the window
            window.Close();
            var windowx = new Admin_Window();
            windowx.Show();
        }
    }
    
   
}