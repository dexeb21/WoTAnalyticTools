namespace BattleResultCollector
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.buttonTest = new System.Windows.Forms.Button();
            this.openWoTCahceFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.buttonTest2 = new System.Windows.Forms.Button();
            this.LogRichTextBox = new System.Windows.Forms.RichTextBox();
            this.CacheLoadTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // notifyIcon
            // 
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "WoT Collector";
            this.notifyIcon.Visible = true;
            // 
            // buttonTest
            // 
            this.buttonTest.Location = new System.Drawing.Point(414, 270);
            this.buttonTest.Name = "buttonTest";
            this.buttonTest.Size = new System.Drawing.Size(132, 23);
            this.buttonTest.TabIndex = 0;
            this.buttonTest.Text = "Test_Selected_Append";
            this.buttonTest.UseVisualStyleBackColor = true;
            this.buttonTest.Click += new System.EventHandler(this.buttonTest_Click);
            // 
            // openWoTCahceFileDialog
            // 
            this.openWoTCahceFileDialog.DefaultExt = "*.dat";
            this.openWoTCahceFileDialog.Filter = "WoT Cache|*.dat";
            this.openWoTCahceFileDialog.InitialDirectory = "D:\\MyClouds\\YandexDisk\\wot\\REP_Cache\\";
            this.openWoTCahceFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.openWoTCahceFileDialog_FileOk);
            // 
            // buttonTest2
            // 
            this.buttonTest2.Location = new System.Drawing.Point(287, 270);
            this.buttonTest2.Name = "buttonTest2";
            this.buttonTest2.Size = new System.Drawing.Size(121, 23);
            this.buttonTest2.TabIndex = 1;
            this.buttonTest2.Text = "Test_AutoUpdate";
            this.buttonTest2.UseVisualStyleBackColor = true;
            this.buttonTest2.Click += new System.EventHandler(this.buttonTest2_Click);
            // 
            // LogRichTextBox
            // 
            this.LogRichTextBox.Location = new System.Drawing.Point(12, 12);
            this.LogRichTextBox.Name = "LogRichTextBox";
            this.LogRichTextBox.Size = new System.Drawing.Size(534, 252);
            this.LogRichTextBox.TabIndex = 2;
            this.LogRichTextBox.Text = "";
            // 
            // CacheLoadTimer
            // 
            this.CacheLoadTimer.Interval = 1000;
            this.CacheLoadTimer.Tick += new System.EventHandler(this.CacheLoadTimer_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(558, 305);
            this.Controls.Add(this.LogRichTextBox);
            this.Controls.Add(this.buttonTest2);
            this.Controls.Add(this.buttonTest);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Collector";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.Button buttonTest;
        private System.Windows.Forms.OpenFileDialog openWoTCahceFileDialog;
        private System.Windows.Forms.Button buttonTest2;
        private System.Windows.Forms.RichTextBox LogRichTextBox;
        private System.Windows.Forms.Timer CacheLoadTimer;
    }
}

