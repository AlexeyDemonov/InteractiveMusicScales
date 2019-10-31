using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InteractiveMusicScales
{
    class Program
    {
        [STAThread]
        public static void Main()
        {
            var app = new App();
            var window = new MainWindow();

            app.MainWindow = window;

            app.InitializeComponent();
            app.Run();
        }
    }
}
