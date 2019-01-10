using System;
using System.Windows.Forms;
using System.IO;
using System.Linq;

namespace _2KarelInstaller
{
    public partial class Form1 : Form
    {
        public string dir = Directory.GetCurrentDirectory();
        public Form1()
        {
            InitializeComponent();
            InitializeApplication();
        }

        private void InitializeApplication()
        {
            label1.Text = "Checking for launcher updates...";
            progressBar1.Style = ProgressBarStyle.Marquee;
            progressBar1.MarqueeAnimationSpeed = 1;
            // Comprobar carpeta
            ErrorComprobation();

            if (CheckForLauncherUpdate())
            {
                // Update launcher...
            }
            else
            {
                // Launch launcher!
            }
        }

        private bool CheckForLauncherUpdate()
        {
            return true;
        }

        public void ErrorComprobation()
        {
            string fullPath = Path.GetFullPath(Directory.GetCurrentDirectory()).TrimEnd(Path.DirectorySeparatorChar);
            string[] projectName = fullPath.Split(Path.DirectorySeparatorChar);
            if (projectName[projectName.Length - 2] != "2Karel" || projectName[projectName.Length - 1] != "2KInstaller")
            {
                MessageBox.Show("La carpeta de instalación no és la correcta. Intenta reinstalar el programa o comprueba que se encuentre dentro de la carpeta .../2Karel/2KInstaller/2KarelInstaller.exe", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(0);
            }
        }
    }
}
