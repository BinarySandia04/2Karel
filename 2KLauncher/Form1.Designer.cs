namespace _2KLauncher
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.labelTitle = new System.Windows.Forms.Label();
            this.X = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.downloadMessage = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.downdownMessage = new System.Windows.Forms.Label();
            this.DownloadText1 = new System.Windows.Forms.Label();
            this.DownloadText2 = new System.Windows.Forms.Label();
            this.DownloadBar = new System.Windows.Forms.PictureBox();
            this.InstallingBar = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DownloadBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.InstallingBar)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(115)))), ((int)(((byte)(252)))));
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(554, 31);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(115)))), ((int)(((byte)(252)))));
            this.labelTitle.ForeColor = System.Drawing.Color.White;
            this.labelTitle.Location = new System.Drawing.Point(12, 9);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(85, 13);
            this.labelTitle.TabIndex = 1;
            this.labelTitle.Text = "2Karel Launcher";
            this.labelTitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.labelTitle_MouseDown);
            this.labelTitle.MouseMove += new System.Windows.Forms.MouseEventHandler(this.labelTitle_MouseMove);
            // 
            // X
            // 
            this.X.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.X.BackColor = System.Drawing.Color.Red;
            this.X.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.X.ForeColor = System.Drawing.Color.White;
            this.X.Location = new System.Drawing.Point(522, 0);
            this.X.Name = "X";
            this.X.Size = new System.Drawing.Size(32, 31);
            this.X.TabIndex = 2;
            this.X.Text = "X";
            this.X.UseVisualStyleBackColor = false;
            this.X.Click += new System.EventHandler(this.X_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.BackColor = System.Drawing.Color.DimGray;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(491, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(32, 31);
            this.button1.TabIndex = 3;
            this.button1.Text = "_";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // downloadMessage
            // 
            this.downloadMessage.AutoSize = true;
            this.downloadMessage.ForeColor = System.Drawing.Color.White;
            this.downloadMessage.Location = new System.Drawing.Point(12, 47);
            this.downloadMessage.Name = "downloadMessage";
            this.downloadMessage.Size = new System.Drawing.Size(111, 13);
            this.downloadMessage.TabIndex = 4;
            this.downloadMessage.Text = "Downloading 2Karel...";
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(67)))), ((int)(((byte)(67)))));
            this.pictureBox2.Location = new System.Drawing.Point(12, 74);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(500, 28);
            this.pictureBox2.TabIndex = 5;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(67)))), ((int)(((byte)(67)))));
            this.pictureBox3.Location = new System.Drawing.Point(12, 108);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(500, 28);
            this.pictureBox3.TabIndex = 6;
            this.pictureBox3.TabStop = false;
            // 
            // downdownMessage
            // 
            this.downdownMessage.AutoSize = true;
            this.downdownMessage.ForeColor = System.Drawing.Color.White;
            this.downdownMessage.Location = new System.Drawing.Point(12, 150);
            this.downdownMessage.Name = "downdownMessage";
            this.downdownMessage.Size = new System.Drawing.Size(78, 13);
            this.downdownMessage.TabIndex = 7;
            this.downdownMessage.Text = "13% - 200KB/s";
            // 
            // DownloadText1
            // 
            this.DownloadText1.AutoSize = true;
            this.DownloadText1.ForeColor = System.Drawing.Color.White;
            this.DownloadText1.Location = new System.Drawing.Point(518, 82);
            this.DownloadText1.Name = "DownloadText1";
            this.DownloadText1.Size = new System.Drawing.Size(27, 13);
            this.DownloadText1.TabIndex = 8;
            this.DownloadText1.Text = "39%";
            // 
            // DownloadText2
            // 
            this.DownloadText2.AutoSize = true;
            this.DownloadText2.ForeColor = System.Drawing.Color.White;
            this.DownloadText2.Location = new System.Drawing.Point(518, 117);
            this.DownloadText2.Name = "DownloadText2";
            this.DownloadText2.Size = new System.Drawing.Size(21, 13);
            this.DownloadText2.TabIndex = 9;
            this.DownloadText2.Text = "0%";
            // 
            // DownloadBar
            // 
            this.DownloadBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(61)))), ((int)(((byte)(202)))));
            this.DownloadBar.Location = new System.Drawing.Point(12, 74);
            this.DownloadBar.Name = "DownloadBar";
            this.DownloadBar.Size = new System.Drawing.Size(151, 28);
            this.DownloadBar.TabIndex = 10;
            this.DownloadBar.TabStop = false;
            // 
            // InstallingBar
            // 
            this.InstallingBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(61)))), ((int)(((byte)(202)))));
            this.InstallingBar.Location = new System.Drawing.Point(12, 108);
            this.InstallingBar.Name = "InstallingBar";
            this.InstallingBar.Size = new System.Drawing.Size(0, 28);
            this.InstallingBar.TabIndex = 11;
            this.InstallingBar.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(554, 177);
            this.Controls.Add(this.InstallingBar);
            this.Controls.Add(this.DownloadBar);
            this.Controls.Add(this.DownloadText2);
            this.Controls.Add(this.DownloadText1);
            this.Controls.Add(this.downdownMessage);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.downloadMessage);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.X);
            this.Controls.Add(this.labelTitle);
            this.Controls.Add(this.pictureBox1);
            this.Cursor = System.Windows.Forms.Cursors.AppStarting;
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(115)))), ((int)(((byte)(252)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "2Karel";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DownloadBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.InstallingBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Button X;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label downloadMessage;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Label downdownMessage;
        private System.Windows.Forms.Label DownloadText1;
        private System.Windows.Forms.Label DownloadText2;
        private System.Windows.Forms.PictureBox DownloadBar;
        private System.Windows.Forms.PictureBox InstallingBar;
    }
}

