using System;
using System.Windows.Forms;

namespace _2KarelInstaller
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Form1 app = new Form1();
            Application.Run(app);
            app.InitializeComponent();
            app.InitializeApplication();
        }
        

    }

    
}
