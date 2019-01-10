using System;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Xml;

namespace _2KarelInstaller
{
    public partial class Form1 : Form
    {
        public string dir = Directory.GetCurrentDirectory();
        public string verxml = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\")) + "versions.xml";
        public Form1()
        {
            InitializeComponent();
            InitializeApplication();
        }

        private string readVersion(){
            XmlDocument doc = new XmlDocument();
            doc.Load(verxml);
            XmlNode node = doc.DocumentElement.SelectSingleNode("/versions/launcher");
            return node.InnerText;
        }

        private void InitializeApplication()
        {

            label1.Text = "Checking for launcher updates...";
            progressBar1.Style = ProgressBarStyle.Marquee;
            progressBar1.MarqueeAnimationSpeed = 1;
            // Comprobar carpeta
            InitialErrorComprobation();

            if (CheckForLauncherUpdate())
            {
                bool firstTime = false;
                string actualVersion = "";
                if (!File.Exists(verxml))
                {
                    firstTime = true;
                } else
                {
                    actualVersion = readVersion();
                    File.Delete(verxml);
                }
                
                DownloadAndWait("https://github.com/BinarySandia04/2Karel/raw/master/Releases/versions.xml", verxml);
                string newVersion = readVersion();
                if(actualVersion != newVersion || firstTime)
                {
                    label1.Text = "Updating native launcher...";
                    string path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\")) + "2KLauncher";
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    try
                    {
                        DownloadAsyncLauncher("https://github.com/BinarySandia04/2Karel/raw/master/Releases/2KLauncher.exe", Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\")) + "2KLauncher/2KLauncher.exe");
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("No se puede instalar el launcher. Comprueba tu conexión a internet y vuelve a intentarlo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Environment.Exit(0);
                    }
                } else
                {
                    label1.Text = "Up to date";
                    // Ejecutar launcher
                    AfterInstallationComprobation();
                    StartLauncher();
                }

                /*
                using (WebClient wc = new WebClient())
                {
                    wc.DownloadFileAsync(
                        // Param1 = Link of file
                        new System.Uri("https://github.com/BinarySandia04/BinarySandia04.github.io/raw/master/download/2KSetup.msi"),
                        // Param2 = Path to save
                        Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\")) + "lol.msi"
                    );
                }*/
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

        public void DownloadAsyncLauncher(string uri, string path)
        {
            progressBar1.Style = ProgressBarStyle.Blocks;
            progressBar1.MarqueeAnimationSpeed = 0;
            progressBar1.Value = 0;
            using (WebClient wc = new WebClient())
            {
                wc.DownloadProgressChanged += DownloadProgressChanged;
                wc.DownloadFileAsync(
                    // Param1 = Link of file
                    new System.Uri(uri),
                    // Param2 = Path to save
                    path
                );
            }
        }
        
        // Event to track the progress
        void DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
            if(e.ProgressPercentage == 100)
            {
                // Hacer algo una vez descargado.
                StartLauncher();
            }
        }

        void StartLauncher()
        {
            System.Diagnostics.Process.Start(Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\")) + "2KLauncher/2KLauncher.exe");
            Environment.Exit(0);
        }

        public void AfterInstallationComprobation()
        {
            string path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\")) + "2KLauncher";
            if (!Directory.Exists(path))
            {
                MessageBox.Show("La instalacion del launcher se ha corrompido/borrado/movido. No se puede ejecutar. Para solucionar el error, vuelve a abrir este archivo.exe, y si no funciona, ponte en contacto conmigo!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(0);
            }
        }

        public void InitialErrorComprobation()
        {
            string fullPath = Path.GetFullPath(Directory.GetCurrentDirectory()).TrimEnd(Path.DirectorySeparatorChar);
            string[] projectName = fullPath.Split(Path.DirectorySeparatorChar);
            if (projectName[projectName.Length - 2] != "2Karel" || projectName[projectName.Length - 1] != "2KInstaller")
            {
                MessageBox.Show("La carpeta de instalación no és la correcta. Intenta reinstalar el programa o comprueba que se encuentre dentro de la carpeta .../2Karel/2KInstaller/2KarelInstaller.exe", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(0);
            }
        }

        private void DownloadAndWait(string ur, string path)
        {
            using (WebClient webClient = new WebClient())
            {
                // nastaveni ze webClient ma pouzit Windows Authentication
                webClient.UseDefaultCredentials = true;
                // spusteni stahovani
                webClient.DownloadFile(new Uri(ur), path);
            }

        }

    }
}
