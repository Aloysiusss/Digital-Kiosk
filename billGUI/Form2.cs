using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace billGUI
{
    public partial class Form2 : Form
    {
        private TextBox studentIdBox;
        private TableLayoutPanel keypadPanel;
        private Label footerLeft;
        private Label footerRight;
        private Panel centerPanel;
        private Timer clockTimer;

        public Form2()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.None;

            this.Load += Form2_Load;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            this.BackColor = Color.White;

            InitializeControls();

            clockTimer = new Timer
            {
                Interval = 1000 
            };
            clockTimer.Tick += ClockTimer_Tick;
            clockTimer.Start();
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

        private void InitializeControls()
        {
            // Center panel
            centerPanel = new Panel
            {
                Width = 600,
                Height = 700,
                BackColor = Color.Transparent
            };
            this.Controls.Add(centerPanel);

            centerPanel.Location = new Point(
                (this.ClientSize.Width - centerPanel.Width) / 2,
                (this.ClientSize.Height - centerPanel.Height) / 2
            );

            // Student ID TextBox
            studentIdBox = new TextBox
            {
                Font = new Font("Segoe UI", 28),
                Width = 580,
                Height = 60,
                TextAlign = HorizontalAlignment.Center,
                ForeColor = Color.Gray,
                BorderStyle = BorderStyle.FixedSingle,
                Text = "Enter Student ID Number",
                MaxLength = 11
            };
            studentIdBox.ShortcutsEnabled = false;

            studentIdBox.Location = new Point(
                centerPanel.Location.X + (centerPanel.Width - studentIdBox.Width) / 2,
                centerPanel.Location.Y + 45
            );

            studentIdBox.GotFocus += (s, e) =>
            {
                if (studentIdBox.Text == "Enter Student ID Number")
                {
                    studentIdBox.Text = "Enter Student ID Number";
                    studentIdBox.ForeColor = Color.Gray;
                }
                studentIdBox.BringToFront();
            };
            studentIdBox.LostFocus += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(studentIdBox.Text) || studentIdBox.Text == "")
                {
                    studentIdBox.Text = "Enter Student ID Number";
                    studentIdBox.ForeColor = Color.Gray;
                }
            };
            this.Controls.Add(studentIdBox);

            studentIdBox.BringToFront();

            // Keypad
            keypadPanel = new TableLayoutPanel
            {
                ColumnCount = 3,
                RowCount = 4,
                Size = new Size(600, 450),
                Location = new Point(0, 120),
                BackColor = Color.Transparent
            };

            for (int i = 0; i < 3; i++)
                keypadPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            for (int i = 0; i < 4; i++)
                keypadPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));

            string[] keys = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "del", "0", "next" };

            foreach (string key in keys)
            {
                if (key == "del")
                {
                    Panel delPanel = new Panel
                    {
                        Dock = DockStyle.Fill,
                        BackColor = Color.Black,
                        Padding = new Padding(2),
                        Margin = new Padding(10)
                    };

                    PictureBox deletePic = new PictureBox
                    {
                        Dock = DockStyle.Fill,
                        SizeMode = PictureBoxSizeMode.Zoom,
                        Image = Properties.Resources.delete,
                        Cursor = Cursors.Hand,
                        BackColor = Color.LightGray
                    };
                    deletePic.Click += (s, e) =>
                    {
                        if (!string.IsNullOrEmpty(studentIdBox.Text) && studentIdBox.Text != "")
                        {
                            studentIdBox.Text = studentIdBox.Text.Substring(0, studentIdBox.Text.Length - 1);
                            studentIdBox.SelectionStart = studentIdBox.Text.Length;
                            studentIdBox.SelectionLength = 0;
                            studentIdBox.Focus();
                        }
                    };

                    delPanel.Controls.Add(deletePic);
                    keypadPanel.Controls.Add(delPanel);
                }
                else if (key == "next")
                {
                    Panel nextPanel = new Panel
                    {
                        Dock = DockStyle.Fill,
                        BackColor = Color.Black,
                        Padding = new Padding(2),
                        Margin = new Padding(10)
                    };

                    PictureBox nextPic = new PictureBox
                    {
                        Dock = DockStyle.Fill,
                        SizeMode = PictureBoxSizeMode.Zoom,
                        Image = Properties.Resources.right_arrow,
                        Cursor = Cursors.Hand,
                        BackColor = Color.LightGray
                    };
                    nextPic.Click += (s, e) =>
                    {
                        string enteredId = studentIdBox.Text.Trim();

                        if (string.IsNullOrWhiteSpace(enteredId) || enteredId == "Enter Student ID Number")
                        {
                            MessageBox.Show("Please enter a valid Student ID.");
                            return;
                        }

                        var studentRow = DatabaseHelper.GetStudentById(enteredId);
                        if (studentRow == null)
                        {
                            MessageBox.Show("Student ID not found in system.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        string fullName = studentRow["student_name"].ToString();
                        string branch = studentRow["Branch"].ToString();
                        //decimal balance = Convert.ToDecimal(studentRow["balance"]);

                        string message = $"Student ID: {enteredId}\nName: {fullName}\nBranch: {branch}";
                        using (ConfirmationModal modal = new ConfirmationModal(message))
                        {
                            DialogResult result = modal.ShowDialog();

                            if (result == DialogResult.OK)
                            {
                                Form3 form3 = new Form3(enteredId); 
                                form3.Show();
                                this.Hide();
                            }
                        }

                        //if (result == DialogResult.OK)
                        //{
                        //    Form3 form3 = new Form3(enteredId);  
                        //    form3.Show();
                        //    this.Hide();
                        //}

                        studentIdBox.Text = "";
                        studentIdBox.ForeColor = Color.Gray;
                    };

                    nextPanel.Controls.Add(nextPic);
                    keypadPanel.Controls.Add(nextPanel);
                }
                else
                {
                    Button btn = new Button
                    {
                        Text = key,
                        Dock = DockStyle.Fill,
                        Font = new Font("Segoe UI", 28, FontStyle.Bold),
                        BackColor = Color.LightGray,
                        ForeColor = Color.Black,
                        FlatStyle = FlatStyle.Flat,
                        Margin = new Padding(10),
                        TabStop = false
                    };
                    btn.FlatAppearance.BorderColor = Color.Black;
                    btn.FlatAppearance.BorderSize = 2;
                    btn.FlatAppearance.MouseOverBackColor = Color.DarkGray;

                    btn.Click += KeyButton_Click;
                    keypadPanel.Controls.Add(btn);
                }
            }

            centerPanel.Controls.Add(keypadPanel);  
            studentIdBox.BringToFront();

            // Footer labels
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
            footerRight.BringToFront();

            this.Resize += (s, e) =>
            {
                centerPanel.Location = new Point(
                    (this.ClientSize.Width - centerPanel.Width) / 2,
                    (this.ClientSize.Height - centerPanel.Height) / 2
                );

                footerLeft.Location = new Point(10, this.ClientSize.Height - 40);
                footerRight.Location = new Point(this.ClientSize.Width - footerRight.Width - 10, this.ClientSize.Height - 40);
            };
        }

        private void KeyButton_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            if (studentIdBox.Text == "Enter Student ID Number" || studentIdBox.ForeColor == Color.Gray)
            {
                studentIdBox.Clear();
                studentIdBox.ForeColor = Color.Black;
            }

            if (studentIdBox.Text.Length < studentIdBox.MaxLength)
            {
                studentIdBox.Text += btn.Text;
            }

            studentIdBox.SelectionStart = studentIdBox.Text.Length;
            studentIdBox.SelectionLength = 0;
            studentIdBox.Focus();

            if (studentIdBox.Text.Trim().ToLower() == "-close")
            {
                this.Close();
            }

            Console.WriteLine($"TextBox Text: '{studentIdBox.Text}'");
            Console.WriteLine($"Visible: {studentIdBox.Visible}, Enabled: {studentIdBox.Enabled}");
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

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Rectangle rect = this.ClientRectangle;

            if (rect.Width <= 0 || rect.Height <= 0)
                return;

            using (LinearGradientBrush brush = new LinearGradientBrush(
                rect,
                Color.White,
                Color.White,
                LinearGradientMode.Vertical))
            {
                ColorBlend blend = new ColorBlend();
                blend.Colors = new Color[]
                {
                    Color.FromArgb(180, 180, 180),
                    Color.White,
                    Color.White,
                    Color.White,
                    Color.FromArgb(180, 180, 180)
                };
                blend.Positions = new float[] { 0.0f, 0.2f, 0.5f, 0.8f, 1.0f };

                brush.InterpolationColors = blend;
                e.Graphics.FillRectangle(brush, rect);
            }
        }
    }
}
