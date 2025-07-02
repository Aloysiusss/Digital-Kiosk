using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace billGUI
{
    public partial class Form6 : Form
    {
        private Label lblCenterMessage; //CENTER TEXT LABEL
        private Label footerLeft; //LABEL AMA
        private Label footerRight;//LABEL CLOCK TIME
        private Timer clockTimer;
        private Timer returnTimer; //CLOSE FORM6 AND BACK TO FORM1

        public Form6()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.None;

            this.Load += Form6_Load;
            this.Resize += (s, e) => UpdateClockPosition();
        }

        private void Form6_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.White;

            lblCenterMessage = new Label
            {
                Text = "Please take your receipt........",
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                AutoSize = true,
                BackColor = Color.Transparent,
                ForeColor = Color.Black
            };
            this.Controls.Add(lblCenterMessage);

            footerLeft = new Label
            {
                Text = "Powered by: AMA - SDI (2025)",
                Font = new Font("Segoe UI", 13, FontStyle.Bold),
                ForeColor = Color.Black,
                BackColor = Color.Transparent,
                BorderStyle = BorderStyle.None,
                AutoSize = true,
                Location = new Point(10, this.ClientSize.Height - 40)
            };
            this.Controls.Add(footerLeft);

            footerRight = new Label
            {
                Text = DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt"),
                Font = new Font("Segoe UI", 13, FontStyle.Bold),
                BackColor = Color.Transparent,
                BorderStyle = BorderStyle.None,
                ForeColor = Color.Black,
                AutoSize = true,
                Location = new Point(this.ClientSize.Width - 225, this.ClientSize.Height - 40)
            };
            this.Controls.Add(footerRight);
            UpdateClockPosition();

            clockTimer = new Timer();
            clockTimer.Interval = 1000;
            clockTimer.Tick += ClockTimer_Tick;
            clockTimer.Start();
        }
        public void StartReturnTimer()
        {
            returnTimer = new Timer();
            returnTimer.Interval = 20000; // 20 seconds
            returnTimer.Tick += ReturnTimer_Tick;
            returnTimer.Start();
        }

        private void ReturnTimer_Tick(object sender, EventArgs e)
        {
            returnTimer.Stop(); 
            clockTimer?.Stop(); 
            this.Hide();

            Form1.Instance.Show(); 
            this.Close();
        }
        //STOP THE TIMER
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            returnTimer?.Stop();
            clockTimer?.Stop();
            base.OnFormClosing(e);
        }

        private void UpdateClockPosition()
        {
            if (lblCenterMessage != null)
            {
                lblCenterMessage.Location = new Point(
                    (this.ClientSize.Width - lblCenterMessage.Width) / 2,
                    (this.ClientSize.Height - lblCenterMessage.Height) / 2
                );
            }

            if (footerLeft != null)
            {
                footerLeft.Location = new Point(10, this.ClientSize.Height - footerLeft.Height - 10);
            }

            if (footerRight != null)
            {
                int newX = this.ClientSize.Width - footerRight.Width - 10;
                if (newX < 0) newX = 0;
                footerRight.Location = new Point(newX, this.ClientSize.Height - footerRight.Height - 10);
                footerRight.BringToFront();
            }
        }

        private void ClockTimer_Tick(object sender, EventArgs e)
        {
            footerRight.Text = DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt");
            footerRight.AutoSize = true;
            footerRight.Refresh();
            int newX = this.ClientSize.Width - footerRight.Width - 10;
            if (newX < 0) newX = 0;
            footerRight.Location = new Point(newX, this.ClientSize.Height - 40);
            footerRight.BringToFront();
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            LinearGradientBrush gradientBrush = new LinearGradientBrush(
                this.ClientRectangle,
                Color.White,
                Color.LightGray,
                LinearGradientMode.Vertical
            );
            e.Graphics.FillRectangle(gradientBrush, this.ClientRectangle);
            base.OnPaint(e);
        }
        //KEYBOARD CODE TO CLOSE THE FORM ESC 
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                this.Close();
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
