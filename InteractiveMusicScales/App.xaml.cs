﻿using System.Windows;

namespace InteractiveMusicScales
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Handle_Startup(object sender, StartupEventArgs e)
        {
            this.MainWindow.Show();
        }
    }
}