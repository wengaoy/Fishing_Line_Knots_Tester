namespace serial
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tscbCOM = new System.Windows.Forms.ToolStripComboBox();
            this.cboBDRate = new System.Windows.Forms.ToolStripComboBox();
            this.btnconn = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbtnStart_Stop = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnExit = new System.Windows.Forms.ToolStripButton();
            this.tsbtnCalibrationReading = new System.Windows.Forms.ToolStripButton();
            this.lblCal_average = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbtnTare = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblPort = new System.Windows.Forms.ToolStripStatusLabel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblSavedZeroADC = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tscbCOM,
            this.cboBDRate,
            this.btnconn,
            this.toolStripSeparator1,
            this.tsbtnStart_Stop,
            this.toolStripSeparator2,
            this.btnExit,
            this.tsbtnCalibrationReading,
            this.lblCal_average,
            this.toolStripSeparator3,
            this.tsbtnTare,
            this.toolStripSeparator4,
            this.toolStripButton1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1683, 57);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tscbCOM
            // 
            this.tscbCOM.Name = "tscbCOM";
            this.tscbCOM.Size = new System.Drawing.Size(121, 57);
            // 
            // cboBDRate
            // 
            this.cboBDRate.Items.AddRange(new object[] {
            "2400",
            "4800",
            "9600",
            "14400",
            "19200",
            "28800",
            "38400",
            "57600",
            "76800",
            "115200",
            "230400",
            "250000",
            "500000",
            "1000000"});
            this.cboBDRate.Name = "cboBDRate";
            this.cboBDRate.Size = new System.Drawing.Size(121, 57);
            // 
            // btnconn
            // 
            this.btnconn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnconn.Image = global::serial.Properties.Resources.disconnected_48;
            this.btnconn.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnconn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnconn.Name = "btnconn";
            this.btnconn.Size = new System.Drawing.Size(52, 52);
            this.btnconn.Text = "Disconnected";
            this.btnconn.Click += new System.EventHandler(this.btnconn_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 57);
            // 
            // tsbtnStart_Stop
            // 
            this.tsbtnStart_Stop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnStart_Stop.Image = global::serial.Properties.Resources.play48;
            this.tsbtnStart_Stop.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbtnStart_Stop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnStart_Stop.Name = "tsbtnStart_Stop";
            this.tsbtnStart_Stop.Size = new System.Drawing.Size(52, 52);
            this.tsbtnStart_Stop.Text = "Start";
            this.tsbtnStart_Stop.ToolTipText = "Start Test";
            this.tsbtnStart_Stop.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 57);
            // 
            // btnExit
            // 
            this.btnExit.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btnExit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnExit.Image = global::serial.Properties.Resources.exit1;
            this.btnExit.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnExit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(52, 52);
            this.btnExit.Text = "toolStripButton2";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // tsbtnCalibrationReading
            // 
            this.tsbtnCalibrationReading.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbtnCalibrationReading.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnCalibrationReading.Image = global::serial.Properties.Resources.scales;
            this.tsbtnCalibrationReading.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbtnCalibrationReading.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnCalibrationReading.Name = "tsbtnCalibrationReading";
            this.tsbtnCalibrationReading.Size = new System.Drawing.Size(52, 52);
            this.tsbtnCalibrationReading.Text = "Caliration Reading";
            this.tsbtnCalibrationReading.Click += new System.EventHandler(this.tsbtnCalibrationReading_Click);
            // 
            // lblCal_average
            // 
            this.lblCal_average.Name = "lblCal_average";
            this.lblCal_average.Size = new System.Drawing.Size(28, 52);
            this.lblCal_average.Text = "??";
            this.lblCal_average.ToolTipText = "zero adc data";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 57);
            // 
            // tsbtnTare
            // 
            this.tsbtnTare.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbtnTare.Font = new System.Drawing.Font("Segoe UI Black", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsbtnTare.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnTare.Image")));
            this.tsbtnTare.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnTare.Name = "tsbtnTare";
            this.tsbtnTare.Size = new System.Drawing.Size(63, 52);
            this.tsbtnTare.Text = "TARE";
            this.tsbtnTare.Click += new System.EventHandler(this.tsbtnTare_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 57);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = global::serial.Properties.Resources.getData48;
            this.toolStripButton1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(52, 52);
            this.toolStripButton1.Text = "toolStripButton1";
            this.toolStripButton1.ToolTipText = "Get Saved Zero ADC reading";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblPort,
            this.lblSavedZeroADC});
            this.statusStrip1.Location = new System.Drawing.Point(0, 1053);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1683, 36);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblPort
            // 
            this.lblPort.Name = "lblPort";
            this.lblPort.Size = new System.Drawing.Size(28, 29);
            this.lblPort.Text = "??";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1683, 996);
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 57);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1683, 996);
            this.panel1.TabIndex = 3;
            // 
            // lblSavedZeroADC
            // 
            this.lblSavedZeroADC.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.lblSavedZeroADC.Name = "lblSavedZeroADC";
            this.lblSavedZeroADC.Size = new System.Drawing.Size(32, 29);
            this.lblSavedZeroADC.Text = "??";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1683, 1089);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Fishing Line Tester";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripComboBox tscbCOM;
        private System.Windows.Forms.ToolStripComboBox cboBDRate;
        private System.Windows.Forms.ToolStripButton btnconn;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblPort;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbtnStart_Stop;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tsbtnCalibrationReading;
        private System.Windows.Forms.ToolStripLabel lblCal_average;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStripButton tsbtnTare;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton btnExit;
        private System.Windows.Forms.ToolStripStatusLabel lblSavedZeroADC;
    }
}

