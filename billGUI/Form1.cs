using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D; 

//1
using System.IO.Ports;


namespace billGUI
{

    public partial class Form1 : Form
    {
        public SerialPort serialPort; //2
        private Form5 form5Instance;
        private int totalAmountInserted = 0;
        private bool serialPortInitialized = false;
        private int totalBillAmount = 0;
        private int totalCoinAmount = 0;

        public static Form1 Instance { get; private set; }
        public Form1()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            Instance = this;

            this.KeyPreview = true; 
            this.KeyDown += Form1_KeyDown; 
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close(); // Close the form on ESC key press
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = new Size(900, 500);
            this.DoubleBuffered = true;

            this.MouseClick += Form1_MouseClick;

            tap_gesture.Image = Properties.Resources.doubletap_gesture;
            tap_gesture.BackColor = Color.Transparent;
            tap_gesture.Parent = this;

            int targetWidth = (int)(ama_logo.Width * 0.09);  
            int targetHeight = (int)((float)tap_gesture.Image.Height / tap_gesture.Image.Width * targetWidth);
            tap_gesture.Size = new Size(targetWidth, targetHeight);
            tap_gesture.SizeMode = PictureBoxSizeMode.Zoom;

            tapLabel.Text = "Touch screen to start";
            tapLabel.Font = new Font("Canva Sans", 18, FontStyle.Bold);
            tapLabel.ForeColor = Color.Black;
            tapLabel.BackColor = Color.Transparent;
            tapLabel.AutoSize = true;
            tapLabel.Parent = this;
            this.Controls.Add(tapLabel);

            CenterPictureBox();

            //3
            //if (!serialPortInitialized)
            //{
            //    serialPort = new SerialPort("COM4", 9600);
            //    serialPort.DataReceived -= SerialPort_DataReceived;
            //    serialPort.DataReceived += SerialPort_DataReceived;

            //    try
            //    {
            //        serialPort.Open();
            //        serialPortInitialized = true; // mark as initialized
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show("Failed to open serial port: " + ex.Message);
            //    }
            //}
            if (!serialPortInitialized)
            {
                serialPort = SerialPortManager.Instance.Port;

                if (serialPort != null)
                {
                    serialPort.DataReceived -= SerialPort_DataReceived;
                    serialPort.DataReceived += SerialPort_DataReceived;

                    if (SerialPortManager.Instance.Open())
                    {
                        serialPortInitialized = true;
                    }
                }
            }
        }
        private void OpenPaymentForm()
        {
            string studentId = "12345";
            Form3 form3Instance = new Form3(studentId);
            string paymentAmount = "0.00"; // example total to pay
            List<Form3.TransactionItem> transactions = form3Instance.GetTransactions();

            form5Instance = new Form5(paymentAmount, form3Instance, transactions);
            form5Instance.Show();
        
            //totalAmountInserted = 0; // reset total
        }
        //4
        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                string data = serialPort.ReadLine().Trim(); // read Arduino input
                if (data.StartsWith("BILL:"))
                {
                    string amountStr = data.Substring(5);
                    if (int.TryParse(amountStr, out int amount))
                    {
                        totalBillAmount = amount; 
                        totalAmountInserted = totalBillAmount + totalCoinAmount;
                        string formatted = $"₱ {totalAmountInserted}.00";

                        this.BeginInvoke(new Action(() =>
                        {
                            // Update label in Form5 if open
                            if (form5Instance != null && !form5Instance.IsDisposed)
                            {
                                form5Instance.UpdateInsertedAmount(formatted, "BILL");
                            }
                        }));
                    }
                }
                else if (data.StartsWith("COIN:"))
                {
                    string amountStr = data.Substring(5);
                    if (int.TryParse(amountStr, out int amount))
                    {
                        totalCoinAmount = amount;
                        totalAmountInserted = totalBillAmount + totalCoinAmount;
                        string formatted = $"₱ {totalAmountInserted}.00";

                        this.BeginInvoke(new Action(() =>
                        {
                            if (form5Instance != null && !form5Instance.IsDisposed)
                            {
                                form5Instance.UpdateInsertedAmount(formatted, "COIN");
                            }
                        }));
                    }
                }
            }
            catch
            {
                // Optionally log error here
            }
        }
        //5
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (serialPort != null && serialPort.IsOpen)
            {
                //serialPort.Close();
                SerialPortManager.Instance.Close();
            }
            base.OnFormClosing(e);
        }
        private void CenterPictureBox()
        {
            if (ama_logo.Image != null)
            {
                ama_logo.Size = ama_logo.Image.Size;

                ama_logo.Left = (this.ClientSize.Width - ama_logo.Width) / 2;
                ama_logo.Top = (this.ClientSize.Height - ama_logo.Height) / 2 - 30;

                if (tap_gesture.Image != null)
                {
                    tap_gesture.Left = (this.ClientSize.Width - tap_gesture.Width) / 2;
                    tap_gesture.Top = ama_logo.Bottom + 80;

                    tapLabel.Left = (this.ClientSize.Width - tapLabel.Width) / 2;
                    tapLabel.Top = tap_gesture.Bottom + 15;
                }
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Rectangle rect = this.ClientRectangle;

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
                blend.Positions = new float[]
                {
                    0.0f, 0.2f, 0.5f, 0.8f, 1.0f
                };

                brush.InterpolationColors = blend;
                e.Graphics.FillRectangle(brush, rect);
            }
            //using (Pen pen = new Pen(Color.Red, 1))
            //{
            //    e.Graphics.DrawLine(pen, this.ClientSize.Width / 2, 0, this.ClientSize.Width / 2, this.ClientSize.Height);

            //    e.Graphics.DrawLine(pen, 0, this.ClientSize.Height / 2, this.ClientSize.Width, this.ClientSize.Height / 2);
            //}        
        }
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            CenterPictureBox(); 
        }
        private async void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            Form2 nextForm = new Form2();
            nextForm.Opacity = 0;
            nextForm.Show();
            await Task.Delay(200);
            nextForm.Opacity = 1;
            this.Hide();
            //OpenPaymentForm();
        }
        public void SetForm5Instance(Form5 form5)
        {
            form5Instance = form5;
        }

    }
}
