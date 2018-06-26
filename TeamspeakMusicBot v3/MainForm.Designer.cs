using System.Drawing;

namespace TeamspeakMusicBot_v3
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.artistLbl = new System.Windows.Forms.Label();
            this.trackLbl = new System.Windows.Forms.Label();
            this.albumArt = new System.Windows.Forms.PictureBox();
            this.songTimeBar = new System.Windows.Forms.ProgressBar();
            this.connectBtn = new System.Windows.Forms.PictureBox();
            this.lblConnect = new System.Windows.Forms.Label();
            this.lblChangeFile = new System.Windows.Forms.Label();
            this.changeFileBtn = new System.Windows.Forms.PictureBox();
            this.lblTime = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.albumArt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.connectBtn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.changeFileBtn)).BeginInit();
            this.SuspendLayout();
            // 
            // artistLbl
            // 
            this.artistLbl.AutoSize = true;
            this.artistLbl.Font = new System.Drawing.Font("Gadugi", 24F, System.Drawing.FontStyle.Bold);
            this.artistLbl.Location = new System.Drawing.Point(186, 300);
            this.artistLbl.Name = "artistLbl";
            this.artistLbl.Size = new System.Drawing.Size(100, 38);
            this.artistLbl.TabIndex = 1;
            this.artistLbl.Text = "Artist";
            // 
            // trackLbl
            // 
            this.trackLbl.AutoSize = true;
            this.trackLbl.Font = new System.Drawing.Font("Gadugi", 32F, System.Drawing.FontStyle.Bold);
            this.trackLbl.Location = new System.Drawing.Point(184, 347);
            this.trackLbl.Name = "trackLbl";
            this.trackLbl.Size = new System.Drawing.Size(132, 51);
            this.trackLbl.TabIndex = 2;
            this.trackLbl.Text = "Track";
            // 
            // albumArt
            // 
            this.albumArt.Location = new System.Drawing.Point(14, 278);
            this.albumArt.Name = "albumArt";
            this.albumArt.Size = new System.Drawing.Size(160, 160);
            this.albumArt.TabIndex = 3;
            this.albumArt.TabStop = false;
            // 
            // songTimeBar
            // 
            this.songTimeBar.BackColor = System.Drawing.SystemColors.ControlText;
            this.songTimeBar.Location = new System.Drawing.Point(193, 426);
            this.songTimeBar.Name = "songTimeBar";
            this.songTimeBar.Size = new System.Drawing.Size(595, 12);
            this.songTimeBar.TabIndex = 4;
            // 
            // connectBtn
            // 
            this.connectBtn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("connectBtn.BackgroundImage")));
            this.connectBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.connectBtn.Location = new System.Drawing.Point(12, 12);
            this.connectBtn.Name = "connectBtn";
            this.connectBtn.Size = new System.Drawing.Size(72, 64);
            this.connectBtn.TabIndex = 7;
            this.connectBtn.TabStop = false;
            this.connectBtn.Click += new System.EventHandler(this.connectBtn_Click);
            // 
            // lblConnect
            // 
            this.lblConnect.AutoSize = true;
            this.lblConnect.Location = new System.Drawing.Point(11, 79);
            this.lblConnect.Name = "lblConnect";
            this.lblConnect.Size = new System.Drawing.Size(94, 13);
            this.lblConnect.TabIndex = 8;
            this.lblConnect.Text = "Connect to Spotify";
            // 
            // lblChangeFile
            // 
            this.lblChangeFile.AutoSize = true;
            this.lblChangeFile.Location = new System.Drawing.Point(11, 192);
            this.lblChangeFile.Name = "lblChangeFile";
            this.lblChangeFile.Size = new System.Drawing.Size(123, 13);
            this.lblChangeFile.TabIndex = 9;
            this.lblChangeFile.Text = "Change TS3 file location";
            // 
            // changeFileBtn
            // 
            this.changeFileBtn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("changeFileBtn.BackgroundImage")));
            this.changeFileBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.changeFileBtn.Location = new System.Drawing.Point(12, 125);
            this.changeFileBtn.Name = "changeFileBtn";
            this.changeFileBtn.Size = new System.Drawing.Size(72, 64);
            this.changeFileBtn.TabIndex = 10;
            this.changeFileBtn.TabStop = false;
            this.changeFileBtn.Click += new System.EventHandler(this.changeFileBtn_Click);
            // 
            // lblTime
            // 
            this.lblTime.AutoSize = true;
            this.lblTime.Font = new System.Drawing.Font("Gadugi", 9F, System.Drawing.FontStyle.Bold);
            this.lblTime.Location = new System.Drawing.Point(707, 407);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(81, 16);
            this.lblTime.TabIndex = 13;
            this.lblTime.Text = "00:00 / 00:00";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lblTime);
            this.Controls.Add(this.changeFileBtn);
            this.Controls.Add(this.lblChangeFile);
            this.Controls.Add(this.lblConnect);
            this.Controls.Add(this.connectBtn);
            this.Controls.Add(this.songTimeBar);
            this.Controls.Add(this.albumArt);
            this.Controls.Add(this.trackLbl);
            this.Controls.Add(this.artistLbl);
            this.Name = "MainForm";
            this.Text = "Teamspeak 3 Music Bot";
            ((System.ComponentModel.ISupportInitialize)(this.albumArt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.connectBtn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.changeFileBtn)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public System.Windows.Forms.Label artistLbl;
        public System.Windows.Forms.Label trackLbl;
        public System.Windows.Forms.PictureBox albumArt;
        public System.Windows.Forms.ProgressBar songTimeBar;
        public System.Windows.Forms.PictureBox connectBtn;
        public System.Windows.Forms.Label lblConnect;
        public System.Windows.Forms.Label lblChangeFile;
        public System.Windows.Forms.PictureBox changeFileBtn;
        public System.Windows.Forms.Label lblTime;
    }
}

