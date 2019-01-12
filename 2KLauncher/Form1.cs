using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using Ionic.Zip;

namespace _2KLauncher
{
    public partial class Form1 : Form
    {
        public Point mouseLocation;

        public Form1()
        {
            InitializeComponent();
            CheckForUpdates();
        }

        bool CheckForNewVersionXml()
        {
            if(!File.Exists(Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\")) + "versions.xml")){
                // Actualizacion (primera vez?)
                if(File.Exists(Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\")) + "newversions.xml")) File.Move(Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\")) + "newversions.xml", Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\")) + "versions.xml");
                return true;
            } else
            {
                // Tenemos un newversions.xml, que ha descargado installer, y también un versions.xml, que ha cambiado desde una actualizacion pasada.
                // Para saber si hay que actualizar, tenemos que comparar versions.xml y newversions.xml
                XmlDocument doc = new XmlDocument();
                doc.Load(Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\")) + "newversions.xml");
                XmlNode node = doc.DocumentElement.SelectSingleNode("/versions/application");
                string newversion = node.InnerText;

                doc = new XmlDocument();
                doc.Load(Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\")) + "versions.xml");
                node = doc.DocumentElement.SelectSingleNode("/versions/application");
                string oldversion = node.InnerText;

                // Movemos a la version actual

                File.Delete(Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\")) + "newversions.xml");
                File.Move(Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\")) + "newversions.xml", Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\")) + "versions.xml");


                if (newversion != oldversion)
                {
                    // Update
                    return true;
                }
                else
                {
                    // No update
                    return false;
                }
            }
        }

        void DownloadGame()
        {
            // Intenta borrar juego
            try { Directory.Delete(Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\")) + "2KarelGame"); } catch (Exception) { }

            changeFormTitle("2Karel Launcher - Downloading");
            using (WebClient wc = new WebClient())
            {
                wc.DownloadProgressChanged += download_progressChanged;
                wc.DownloadFileCompleted += download_completed;
                wc.DownloadFileAsync(new Uri("https://github.com/BinarySandia04/2Karel/raw/master/Releases/2KG.zip"), Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\")) + "/2KG.zip");
            }
        }

        private void download_completed(object sender, AsyncCompletedEventArgs e)
        {
            // Cosas
            if(File.Exists(Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\")) + "/2KG.zip"))
            {
                changeDownloadBarPercentage1(100);
                changeDownloadText1("100%");
                InstallGame();
            } else
            {
                MessageBox.Show("Descarga incorrecta. Abortando", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(2);
            }
            
        }
        
        private void download_progressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            changeDownloadBarPercentage1(e.ProgressPercentage);
            changeDownloadText1(e.ProgressPercentage + "%");
            changeBottomDownloadMessage("Progreso: " + (e.ProgressPercentage * 66 / 100) + "%");
        }
        private int totalFiles = 0;
        private int filesExtracted = 0;
        private void InstallGame()
        {
            /*...*/
            changeFormTitle("2Karel Launcher - Installing");
            using (ZipFile zip = ZipFile.Read(Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\")) + "/2KG.zip"))
            {
                zip.ExtractProgress += install_progress;
                zip.ExtractAll(Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\")) + "/2KarelGame/", ExtractExistingFileAction.OverwriteSilently);
                
                totalFiles = zip.Count;
            }
        }

        void install_progress(object sender, ExtractProgressEventArgs e)
        {
            if (e.EntriesExtracted == e.EntriesTotal)
            {
                install_done();
            }
            if (e.TotalBytesToTransfer > 0)
            {
                changeDownloadBarPercentage2(Convert.ToInt32(100 * e.BytesTransferred / e.TotalBytesToTransfer));
                changeDownloadText2(Convert.ToInt32(100 * e.BytesTransferred / e.TotalBytesToTransfer) + "%");
                changeBottomDownloadMessage("Progreso: " + (66 + (Convert.ToInt32(100 * e.BytesTransferred / e.TotalBytesToTransfer) * 33 / 100)) + "%");
            }
            
        }

        private void install_done()
        {
            LaunchGame();
        }

        void CheckForUpdates()
        {
            changeFormTitle("2Karel Launcher");
            changeDownloadMessage("Downloading 2Karel...");
            changeBottomDownloadMessage("Starting...");
            changeDownloadText1("0%");
            changeDownloadText2("...");
            changeDownloadBarPercentage1(0);
            changeDownloadBarPercentage2(0);

            if (CheckForNewVersionXml())
            {
                // Update
                DownloadGame();
            } else {
                // No update
                LaunchGame();
            }
        }

        void LaunchGame()
        {
            if (File.Exists(Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\")) + "2KarelGame/2Karel.exe"))
            {
                Process.Start(Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\")) + "2KarelGame/2Karel.exe");
                Environment.Exit(1);
            }
            else
            {
                if (MessageBox.Show("Error no hay directorio del juego. Quieres reparar los archivos?", "Error", MessageBoxButtons.YesNo, MessageBoxIcon.Error).Equals(DialogResult.Yes))
                {
                    try
                    {
                        File.Delete(Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\")) + "versions.xml");
                        File.Delete(Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\")) + "newversions.xml");
                        Directory.Delete(Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\")) + "2KarelGame"); // Carpeta juego
                    }
                    catch (Exception)
                    { }
                    Environment.Exit(2); // Error
                } else
                {
                    MessageBox.Show("Abortando...");
                    Environment.Exit(2); // Error
                }
            }
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            mouseLocation = new Point(-e.X, -e.Y);
        }
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                Point mousePose = Control.MousePosition;
                mousePose.Offset(mouseLocation.X, mouseLocation.Y);
                Location = mousePose;
            }
        }
        private void X_Click(object sender, EventArgs e)
        {
            Environment.Exit(1);
        }
        private void labelTitle_MouseDown(object sender, MouseEventArgs e)
        {
            mouseLocation = new Point(-e.X, -e.Y);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        private void labelTitle_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point mousePose = Control.MousePosition;
                mousePose.Offset(mouseLocation.X, mouseLocation.Y);
                Location = mousePose;
            }
        }

        void changeDownloadBarPercentage2(int percent)
        {
            Size s = new Size(percent * 5, 28);
            InstallingBar.Size = s;
        }
        void changeDownloadBarPercentage1(int percent)
        {
            Size s = new Size(percent * 5, 28);
            DownloadBar.Size = s;
        }
        void changeBottomDownloadMessage(string text)
        {
            downdownMessage.Text = text;
        }
        void changeDownloadText2(string text)
        {
            DownloadText2.Text = text;
        }
        void changeDownloadText1(string text)
        {
            DownloadText1.Text = text;
        }
        void changeDownloadMessage(string text)
        {
            downloadMessage.Text = text;
        }
        void changeFormTitle(string text)
        {
            labelTitle.Text = text;
            this.Text = text;
        }
    }
}
