using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace billGUI
{
    public partial class LoadingForm : Form
    {
        private Label lblCenterMessage;
        private PictureBox spinner;
        private Label footerLeft;
        private Label footerRight;
        private Timer clockTimer;

        public LoadingForm()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.TopMost = true;
            this.ControlBox = false;
            this.ShowInTaskbar = false;

            InitializeUI();

            clockTimer = new Timer();
            clockTimer.Interval = 1000; 
            clockTimer.Tick += ClockTimer_Tick;
            clockTimer.Start();

            this.Resize += (s, e) => UpdateLayout();
        }

        private void InitializeUI()
        {
            lblCenterMessage = new Label
            {
                Text = "Printing your receipt...\nPlease wait...",
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                ForeColor = Color.FromArgb(50, 50, 50),
                AutoSize = true,
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.Transparent
            };
            this.Controls.Add(lblCenterMessage);

            spinner = new PictureBox
            {
                SizeMode = PictureBoxSizeMode.AutoSize,
                Image = Properties.Resources.spinner_gif_unscreen,  
                BackColor = Color.Transparent
            };
            this.Controls.Add(spinner);

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
            UpdateLayout();
        }

        private void UpdateLayout()
        {
            lblCenterMessage.Location = new Point(
                (this.ClientSize.Width - lblCenterMessage.Width) / 2,
                (this.ClientSize.Height - lblCenterMessage.Height) / 2 - 100
            );

            spinner.Location = new Point(
                (this.ClientSize.Width - spinner.Width) / 2,
                lblCenterMessage.Bottom + 30
            );

            footerLeft.Location = new Point(10, this.ClientSize.Height - footerLeft.Height - 10);

            int footerRightX = this.ClientSize.Width - footerRight.Width - 10;
            if (footerRightX < 0) footerRightX = 0;
            footerRight.Location = new Point(footerRightX, this.ClientSize.Height - footerRight.Height - 10);
            footerLeft.BringToFront();
            footerRight.BringToFront();
        }

        private void ClockTimer_Tick(object sender, EventArgs e)
        {
            footerRight.Text = DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt");
            footerRight.AutoSize = true;
            footerRight.Refresh();

            int footerRightX = this.ClientSize.Width - footerRight.Width - 10;
            if (footerRightX < 0) footerRightX = 0;
            footerRight.Location = new Point(footerRightX, this.ClientSize.Height - footerRight.Height - 10);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            using (LinearGradientBrush gradientBrush = new LinearGradientBrush(
                this.ClientRectangle,
                Color.White,
                Color.LightGray,
                LinearGradientMode.Vertical))
            {
                e.Graphics.FillRectangle(gradientBrush, this.ClientRectangle);
            }

            base.OnPaint(e);
        }
    }
}
