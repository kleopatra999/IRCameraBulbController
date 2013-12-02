namespace IRCameraBulbController.NET
{
    partial class Main
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
            this.ButtonStart = new System.Windows.Forms.Button();
            this.ButtonAbort = new System.Windows.Forms.Button();
            this.NumericDuration = new System.Windows.Forms.NumericUpDown();
            this.NumericQuantity = new System.Windows.Forms.NumericUpDown();
            this.NumericGap = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.progressBar2 = new System.Windows.Forms.ProgressBar();
            this.CheckMirrorUp = new System.Windows.Forms.CheckBox();
            this.ComboPorts = new System.Windows.Forms.ComboBox();
            this.ButtonPortRefresh = new System.Windows.Forms.Button();
            this.TimerUI = new System.Windows.Forms.Timer(this.components);
            this.LabelStatus = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.NumericDuration)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericQuantity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericGap)).BeginInit();
            this.SuspendLayout();
            // 
            // ButtonStart
            // 
            this.ButtonStart.Location = new System.Drawing.Point(71, 141);
            this.ButtonStart.Name = "ButtonStart";
            this.ButtonStart.Size = new System.Drawing.Size(75, 23);
            this.ButtonStart.TabIndex = 4;
            this.ButtonStart.Text = "Start";
            this.ButtonStart.UseVisualStyleBackColor = true;
            // 
            // ButtonAbort
            // 
            this.ButtonAbort.Location = new System.Drawing.Point(71, 170);
            this.ButtonAbort.Name = "ButtonAbort";
            this.ButtonAbort.Size = new System.Drawing.Size(75, 23);
            this.ButtonAbort.TabIndex = 5;
            this.ButtonAbort.Text = "Abort";
            this.ButtonAbort.UseVisualStyleBackColor = true;
            // 
            // NumericDuration
            // 
            this.NumericDuration.Location = new System.Drawing.Point(114, 40);
            this.NumericDuration.Name = "NumericDuration";
            this.NumericDuration.Size = new System.Drawing.Size(73, 20);
            this.NumericDuration.TabIndex = 1;
            this.NumericDuration.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.NumericDuration.ValueChanged += new System.EventHandler(this.NumericDuration_ValueChanged);
            // 
            // NumericQuantity
            // 
            this.NumericQuantity.Location = new System.Drawing.Point(114, 92);
            this.NumericQuantity.Name = "NumericQuantity";
            this.NumericQuantity.Size = new System.Drawing.Size(73, 20);
            this.NumericQuantity.TabIndex = 3;
            this.NumericQuantity.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // NumericGap
            // 
            this.NumericGap.Location = new System.Drawing.Point(114, 66);
            this.NumericGap.Name = "NumericGap";
            this.NumericGap.Size = new System.Drawing.Size(73, 20);
            this.NumericGap.TabIndex = 2;
            this.NumericGap.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Duration";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(26, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(27, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Gap";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(26, 94);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Quantity";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 199);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(192, 23);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar1.TabIndex = 8;
            // 
            // progressBar2
            // 
            this.progressBar2.Location = new System.Drawing.Point(12, 222);
            this.progressBar2.Name = "progressBar2";
            this.progressBar2.Size = new System.Drawing.Size(192, 23);
            this.progressBar2.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar2.TabIndex = 9;
            // 
            // CheckMirrorUp
            // 
            this.CheckMirrorUp.AutoSize = true;
            this.CheckMirrorUp.Location = new System.Drawing.Point(59, 118);
            this.CheckMirrorUp.Name = "CheckMirrorUp";
            this.CheckMirrorUp.Size = new System.Drawing.Size(99, 17);
            this.CheckMirrorUp.TabIndex = 10;
            this.CheckMirrorUp.Text = "Mirror Up Mode";
            this.CheckMirrorUp.UseVisualStyleBackColor = true;
            // 
            // ComboPorts
            // 
            this.ComboPorts.FormattingEnabled = true;
            this.ComboPorts.Location = new System.Drawing.Point(13, 13);
            this.ComboPorts.Name = "ComboPorts";
            this.ComboPorts.Size = new System.Drawing.Size(160, 21);
            this.ComboPorts.TabIndex = 6;
            // 
            // ButtonPortRefresh
            // 
            this.ButtonPortRefresh.BackgroundImage = global::IRCameraBulbController.NET.Properties.Resources.Refresh_24;
            this.ButtonPortRefresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ButtonPortRefresh.Location = new System.Drawing.Point(179, 13);
            this.ButtonPortRefresh.Name = "ButtonPortRefresh";
            this.ButtonPortRefresh.Size = new System.Drawing.Size(23, 23);
            this.ButtonPortRefresh.TabIndex = 12;
            this.ButtonPortRefresh.UseVisualStyleBackColor = true;
            // 
            // LabelStatus
            // 
            this.LabelStatus.BackColor = System.Drawing.Color.White;
            this.LabelStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LabelStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.LabelStatus.Location = new System.Drawing.Point(0, 257);
            this.LabelStatus.Name = "LabelStatus";
            this.LabelStatus.Size = new System.Drawing.Size(216, 23);
            this.LabelStatus.TabIndex = 13;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(216, 280);
            this.Controls.Add(this.LabelStatus);
            this.Controls.Add(this.ButtonPortRefresh);
            this.Controls.Add(this.ComboPorts);
            this.Controls.Add(this.CheckMirrorUp);
            this.Controls.Add(this.progressBar2);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.NumericGap);
            this.Controls.Add(this.NumericQuantity);
            this.Controls.Add(this.NumericDuration);
            this.Controls.Add(this.ButtonAbort);
            this.Controls.Add(this.ButtonStart);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Main";
            this.Text = "IRCameraBulbController.NET";
            ((System.ComponentModel.ISupportInitialize)(this.NumericDuration)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericQuantity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericGap)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ButtonStart;
        private System.Windows.Forms.Button ButtonAbort;
        private System.Windows.Forms.NumericUpDown NumericDuration;
        private System.Windows.Forms.NumericUpDown NumericQuantity;
        private System.Windows.Forms.NumericUpDown NumericGap;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.ProgressBar progressBar2;
        private System.Windows.Forms.CheckBox CheckMirrorUp;
        private System.Windows.Forms.ComboBox ComboPorts;
        private System.Windows.Forms.Button ButtonPortRefresh;
        private System.Windows.Forms.Timer TimerUI;
        private System.Windows.Forms.Label LabelStatus;
    }
}

