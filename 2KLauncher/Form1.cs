using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

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
                File.Move(Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\")) + "newversions.xml", Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\")) + "versions.xml");
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
            } else {
                // No update
                LaunchGame();
            }
        }

        void LaunchGame()
        {
            if (File.Exists("Path to game...."))
            {

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
