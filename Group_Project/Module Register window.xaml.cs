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
    /// Interaction logic for Module_Register_window.xaml
    /// </summary>
    public partial class Module_Register_window : Window
    {
        public Module_Register_window(Module_Register_View_Model vm)
        {
            InitializeComponent();
            DataContext=vm;
        }
        private void clsclk(object sender, RoutedEventArgs e)
        {

            {
                Window window = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive);

                // close the window
                window.Close();

            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
    }
}
