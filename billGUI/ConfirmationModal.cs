using System;
using System.Drawing;
using System.Windows.Forms;

namespace billGUI
{
    public class ConfirmationModal : Form
    {
        private Label lblMessage;
        private Button btnOK, btnCancel;

        public ConfirmationModal(string message)
        {
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.White;
            this.Size = new Size(700, 300);
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.ControlBox = false;
            this.ShowInTaskbar = false;

            Label lblTitle = new Label
            {
                Text = "Student Information",
                ForeColor = Color.Red,
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                AutoSize = false,
                Size = new Size(700, 40),
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point(0, 20)
            };
            this.Controls.Add(lblTitle);


            lblMessage = new Label
            {
                Text = message,
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                Size = new Size(550, 150),
                Location = new Point(25, 55),
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleLeft
            };
            this.Controls.Add(lblMessage);

            // Buttons
            btnOK = new Button
            {
                Text = "Confirm",
                DialogResult = DialogResult.OK,
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                Size = new Size(150, 50),
                BackColor = Color.Green,
                ForeColor = Color.White
            };

            btnCancel = new Button
            {
                Text = "Cancel",
                DialogResult = DialogResult.Cancel,
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                Size = new Size(150, 50),
                BackColor = Color.DarkRed,
                ForeColor = Color.White
            };

            // Center buttons horizontally
            int spacing = 30;
            int totalWidth = btnOK.Width + btnCancel.Width + spacing;
            int startX = (this.ClientSize.Width - totalWidth) / 2;
            int y = 210;

            btnOK.Location = new Point(startX, y);
            btnCancel.Location = new Point(startX + btnOK.Width + spacing, y);

            this.Controls.Add(btnOK);
            this.Controls.Add(btnCancel);

            this.AcceptButton = btnOK;
            this.CancelButton = btnCancel;
        }
    }
}
