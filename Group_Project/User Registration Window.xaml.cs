﻿using System;
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
    /// Interaction logic for User_Registration_Window.xaml
    /// </summary>
    public partial class User_Registration_Window : Window
    {
        public User_Registration_Window(StudentVM vm)
        {
            InitializeComponent();
            DataContext = vm;
        }
        public User_Registration_Window()
        {
            InitializeComponent();
            DataContext = new StudentVM();
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
        }

      
    }
}