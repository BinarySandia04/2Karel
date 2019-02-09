﻿using System;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Xml;
using System.ComponentModel;

namespace _2KarelInstaller
{
    public partial class Form1 : Form
    {
        public string dir = Directory.GetCurrentDirectory();
        public string verxml = Path.GetFullPath(Directory.GetCurrentDirectory()) + "newversions.xml";
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

        public void InitializeApplication()
        {

            label1.Text = "Checking for launcher updates...";
            progressBar1.Style = ProgressBarStyle.Marquee;
            progressBar1.MarqueeAnimationSpeed = 1;
            // Comprobar carpeta

           
                bool firstTime = false;
                string actualVersion = "";
                if (!File.Exists(verxml))
                {
                    firstTime = true;
                } else
                {
                    actualVersion = readVersion();
                }
                
                DownloadAndWait("https://github.com/BinarySandia04/2Karel/raw/master/Releases/versions.xml", verxml);
                string newVersion = readVersion();
                if(actualVersion != newVersion || firstTime)
                {
                    label1.Text = "Updating native launcher...";
                    
                        try
                    {
                        DownloadAsyncLauncher("https://github.com/BinarySandia04/2Karel/raw/master/Releases/2KLauncher.exe", Path.GetFullPath(Directory.GetCurrentDirectory()) + "/2KLauncher.exe");
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
                    StartLauncher();
                }
                
            
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
                wc.DownloadFileCompleted += DownloadCompleted;
            }
        }

        private void DownloadCompleted(object sender, AsyncCompletedEventArgs e)
        {
            StartLauncher();
        }

        // Event to track the progress
        void DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }

        void StartLauncher()
        {
            System.Diagnostics.Process.Start(Path.GetFullPath(Directory.GetCurrentDirectory()) + "/2KLauncher.exe");
            Environment.Exit(0);
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
