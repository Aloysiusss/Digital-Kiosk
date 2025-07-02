namespace billGUI
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
            this.tapLabel = new System.Windows.Forms.Label();
            this.ama_logo = new System.Windows.Forms.PictureBox();
            this.tap_gesture = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.ama_logo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tap_gesture)).BeginInit();
            this.SuspendLayout();
            // 
            // tapLabel
            // 
            this.tapLabel.AutoSize = true;
            this.tapLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tapLabel.Location = new System.Drawing.Point(652, 532);
            this.tapLabel.Name = "tapLabel";
            this.tapLabel.Size = new System.Drawing.Size(258, 29);
            this.tapLabel.TabIndex = 2;
            this.tapLabel.Text = "Touch screen to start";
            // 
            // ama_logo
            // 
            this.ama_logo.BackColor = System.Drawing.Color.Transparent;
            this.ama_logo.Image = ((System.Drawing.Image)(resources.GetObject("ama_logo.Image")));
            this.ama_logo.Location = new System.Drawing.Point(218, 105);
            this.ama_logo.Name = "ama_logo";
            this.ama_logo.Size = new System.Drawing.Size(884, 307);
            this.ama_logo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.ama_logo.TabIndex = 1;
            this.ama_logo.TabStop = false;
            // 
            // tap_gesture
            // 
            this.tap_gesture.Image = global::billGUI.Properties.Resources.doubletap_gesture;
            this.tap_gesture.Location = new System.Drawing.Point(735, 468);
            this.tap_gesture.Name = "tap_gesture";
            this.tap_gesture.Size = new System.Drawing.Size(66, 50);
            this.tap_gesture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.tap_gesture.TabIndex = 3;
            this.tap_gesture.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1303, 628);
            this.Controls.Add(this.tapLabel);
            this.Controls.Add(this.ama_logo);
            this.Controls.Add(this.tap_gesture);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "AMA - Digital Cashier";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ama_logo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tap_gesture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox tap_gesture;
        private System.Windows.Forms.PictureBox ama_logo;
        private System.Windows.Forms.Label tapLabel;
    }
}

