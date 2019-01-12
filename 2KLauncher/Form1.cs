using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

        void CheckForUpdates()
        {
            //if (updates)
            //{
            changeFormTitle("2Karel Launcher");
            changeDownloadMessage("Downloading 2Karel...");
            changeBottomDownloadMessage("Starting...");
            changeDownloadText1("0%");
            changeDownloadText2("...");
            changeDownloadBarPercentage1(0);
            changeDownloadBarPercentage2(0);
            //} else { iniciar juego...
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
