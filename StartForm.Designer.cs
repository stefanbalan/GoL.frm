namespace GoLife
{
    partial class StartForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StartForm));
            this.btnStartStop = new System.Windows.Forms.Button();
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.txtBack = new System.Windows.Forms.TextBox();
            this.txtLive = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtBorn = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtDead = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtDelay = new System.Windows.Forms.TextBox();
            this.chkHighlight = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.trkDelay = new System.Windows.Forms.TrackBar();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lstPatterns = new System.Windows.Forms.ListBox();
            this.btnLoadPattern = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.stsLabelAverageTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.btnStep = new System.Windows.Forms.Button();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trkDelay)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnStartStop
            // 
            this.btnStartStop.Location = new System.Drawing.Point(12, 12);
            this.btnStartStop.Name = "btnStartStop";
            this.btnStartStop.Size = new System.Drawing.Size(100, 23);
            this.btnStartStop.TabIndex = 0;
            this.btnStartStop.Text = "Start/Stop";
            this.btnStartStop.UseVisualStyleBackColor = true;
            this.btnStartStop.Click += new System.EventHandler(this.btnStartStop_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Background";
            // 
            // txtBack
            // 
            this.txtBack.Location = new System.Drawing.Point(81, 19);
            this.txtBack.Name = "txtBack";
            this.txtBack.Size = new System.Drawing.Size(100, 20);
            this.txtBack.TabIndex = 2;
            this.txtBack.LostFocus += new System.EventHandler(this.txtBackground_LostFocus);
            // 
            // txtLive
            // 
            this.txtLive.Location = new System.Drawing.Point(81, 45);
            this.txtLive.Name = "txtLive";
            this.txtLive.Size = new System.Drawing.Size(100, 20);
            this.txtLive.TabIndex = 5;
            this.txtLive.LostFocus += new System.EventHandler(this.txtLive_LostFocus);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Live cell";
            // 
            // txtBorn
            // 
            this.txtBorn.Location = new System.Drawing.Point(81, 71);
            this.txtBorn.Name = "txtBorn";
            this.txtBorn.Size = new System.Drawing.Size(100, 20);
            this.txtBorn.TabIndex = 8;
            this.txtBorn.LostFocus += new System.EventHandler(this.txtBorn_LostFocus);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "New born cell";
            // 
            // txtDead
            // 
            this.txtDead.Location = new System.Drawing.Point(81, 97);
            this.txtDead.Name = "txtDead";
            this.txtDead.Size = new System.Drawing.Size(100, 20);
            this.txtDead.TabIndex = 11;
            this.txtDead.LostFocus += new System.EventHandler(this.txtDead_LostFocus);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 100);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Dying cell";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtBack);
            this.groupBox1.Controls.Add(this.txtDead);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtLive);
            this.groupBox1.Controls.Add(this.txtBorn);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(12, 177);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(213, 132);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Colors";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(6, 25);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(34, 13);
            this.label5.TabIndex = 15;
            this.label5.Text = "Delay";
            // 
            // txtDelay
            // 
            this.txtDelay.Location = new System.Drawing.Point(107, 22);
            this.txtDelay.Name = "txtDelay";
            this.txtDelay.Size = new System.Drawing.Size(100, 20);
            this.txtDelay.TabIndex = 14;
            this.txtDelay.LostFocus += new System.EventHandler(this.txtDelay_LostFocus);
            // 
            // chkHighlight
            // 
            this.chkHighlight.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkHighlight.Location = new System.Drawing.Point(35, 104);
            this.chkHighlight.Name = "chkHighlight";
            this.chkHighlight.Size = new System.Drawing.Size(172, 20);
            this.chkHighlight.TabIndex = 16;
            this.chkHighlight.Text = "Highlight changes";
            this.chkHighlight.UseVisualStyleBackColor = true;
            this.chkHighlight.Visible = false;
            this.chkHighlight.CheckedChanged += new System.EventHandler(this.chkHighlight_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtDelay);
            this.groupBox2.Controls.Add(this.trkDelay);
            this.groupBox2.Controls.Add(this.chkHighlight);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Location = new System.Drawing.Point(12, 41);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(213, 130);
            this.groupBox2.TabIndex = 14;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Options";
            // 
            // trkDelay
            // 
            this.trkDelay.LargeChange = 1;
            this.trkDelay.Location = new System.Drawing.Point(9, 48);
            this.trkDelay.Maximum = 5;
            this.trkDelay.Minimum = 1;
            this.trkDelay.Name = "trkDelay";
            this.trkDelay.Size = new System.Drawing.Size(198, 45);
            this.trkDelay.TabIndex = 17;
            this.trkDelay.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.trkDelay.Value = 1;
            this.trkDelay.Scroll += new System.EventHandler(this.trkDelay_Scroll);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox3.Controls.Add(this.lstPatterns);
            this.groupBox3.Controls.Add(this.btnLoadPattern);
            this.groupBox3.Location = new System.Drawing.Point(12, 315);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(213, 218);
            this.groupBox3.TabIndex = 15;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Patterns";
            // 
            // lstPatterns
            // 
            this.lstPatterns.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lstPatterns.FormattingEnabled = true;
            this.lstPatterns.Location = new System.Drawing.Point(6, 48);
            this.lstPatterns.Name = "lstPatterns";
            this.lstPatterns.Size = new System.Drawing.Size(201, 160);
            this.lstPatterns.TabIndex = 2;
            this.lstPatterns.DoubleClick += new System.EventHandler(this.lstPatterns_DoubleClick);
            // 
            // btnLoadPattern
            // 
            this.btnLoadPattern.Location = new System.Drawing.Point(132, 19);
            this.btnLoadPattern.Name = "btnLoadPattern";
            this.btnLoadPattern.Size = new System.Drawing.Size(75, 23);
            this.btnLoadPattern.TabIndex = 0;
            this.btnLoadPattern.Text = "Load";
            this.btnLoadPattern.UseVisualStyleBackColor = true;
            this.btnLoadPattern.Click += new System.EventHandler(this.btnLoadPattern_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stsLabelAverageTime});
            this.statusStrip1.Location = new System.Drawing.Point(0, 536);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(248, 22);
            this.statusStrip1.TabIndex = 16;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // stsLabelAverageTime
            // 
            this.stsLabelAverageTime.Name = "stsLabelAverageTime";
            this.stsLabelAverageTime.Size = new System.Drawing.Size(41, 17);
            this.stsLabelAverageTime.Text = "000ms";
            // 
            // btnStep
            // 
            this.btnStep.Location = new System.Drawing.Point(125, 12);
            this.btnStep.Name = "btnStep";
            this.btnStep.Size = new System.Drawing.Size(100, 23);
            this.btnStep.TabIndex = 17;
            this.btnStep.Text = "Step";
            this.btnStep.UseVisualStyleBackColor = true;
            this.btnStep.Click += new System.EventHandler(this.btnStep_Click);
            // 
            // timer
            // 
            this.timer.Enabled = true;
            this.timer.Interval = 5000;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // StartForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(248, 558);
            this.Controls.Add(this.btnStep);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnStartStop);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "StartForm";
            this.Text = "Game of Life";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trkDelay)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnStartStop;
        private System.Windows.Forms.ColorDialog colorDialog;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtBack;
        private System.Windows.Forms.TextBox txtLive;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtBorn;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtDead;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtDelay;
        private System.Windows.Forms.CheckBox chkHighlight;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ListBox lstPatterns;
        private System.Windows.Forms.Button btnLoadPattern;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel stsLabelAverageTime;
        private System.Windows.Forms.TrackBar trkDelay;
        private System.Windows.Forms.Button btnStep;
        private System.Windows.Forms.Timer timer;
    }
}

