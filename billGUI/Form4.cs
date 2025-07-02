using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace billGUI
{
    public partial class Form4 : Form
    {
        public string TransactionType { get; private set; }
        public string Category { get; private set; }
        public decimal Amount { get; private set; }
        public int Quantity { get; private set; }

        private ComboBox cmbCategory;
        private ComboBox cmbTransactionType;
        private TextBox txtAmount;
        private NumericUpDown numQuantity;
        private Button btnSave, btnCancel;

        private readonly string connectionString = "Server=172.20.4.48;Database=CampusSolution;User Id=sa;Password=WvRF!77kyExE&oq!PatJ;";

        public Form4()
        {
            InitializeComponent();
            this.Text = "Add Transaction";
            this.Size = new Size(700, 800);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = Color.White;

            InitializeFormControls();
            this.Load += Form4_Load;
        }

        private void InitializeFormControls()
        {
            int formWidth = this.ClientSize.Width;
            int controlWidth = 600;
            int startY = 30;
            int spacingY = 110;

            Label lblHeader = new Label
            {
                Text = "Add Transaction",
                Font = new Font("Segoe UI", 22, FontStyle.Bold),
                ForeColor = Color.FromArgb(51, 58, 85),
                Size = new Size(formWidth, 40),
                Location = new Point(0, startY),
                TextAlign = ContentAlignment.MiddleCenter
            };
            this.Controls.Add(lblHeader);

            void AddCenteredControl(string labelText, Control control, int rowIndex)
            {
                Label lbl = new Label
                {
                    Text = labelText,
                    Font = new Font("Segoe UI", 12, FontStyle.Bold),
                    Size = new Size(controlWidth, 24),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Location = new Point((formWidth - controlWidth) / 2, startY + spacingY * rowIndex)
                };
                this.Controls.Add(lbl);

                control.Width = controlWidth;
                control.Location = new Point((formWidth - controlWidth) / 2, lbl.Bottom + 6);
                this.Controls.Add(control);
            }

            // Category with placeholder
            cmbCategory = new ComboBox
            {
                Font = new Font("Segoe UI", 16),
                DropDownStyle = ComboBoxStyle.DropDownList,
                DrawMode = DrawMode.OwnerDrawFixed
            };

            cmbCategory.DrawMode = DrawMode.OwnerDrawFixed;
            cmbCategory.DrawItem += (s, e) =>
            {
                if (e.Index < 0)
                {
                    // When no item, draw placeholder in gray
                    using (Brush brush = new SolidBrush(Color.Gray))
                        e.Graphics.DrawString("Choose", cmbCategory.Font, brush, e.Bounds);
                }
                else
                {
                    Color bgColor = (e.State & DrawItemState.Selected) == DrawItemState.Selected ? Color.White : Color.White;
                    Color textColor = Color.Black;

                    using (Brush bgBrush = new SolidBrush(bgColor))
                        e.Graphics.FillRectangle(bgBrush, e.Bounds);

                    using (Brush textBrush = new SolidBrush(textColor))
                        e.Graphics.DrawString(cmbCategory.Items[e.Index].ToString(), cmbCategory.Font, textBrush, e.Bounds);
                }

                e.DrawFocusRectangle();
            };

            cmbCategory.SelectedIndex = -1;

            cmbTransactionType = new ComboBox
            {
                Font = new Font("Segoe UI", 16),
                DropDownStyle = ComboBoxStyle.DropDownList,
                DrawMode = DrawMode.OwnerDrawFixed
            };
            cmbTransactionType.DrawItem += (s, e) =>
            {
                if (e.Index < 0)
                {
                    // No item selected - draw placeholder in gray
                    using (Brush brush = new SolidBrush(Color.Gray))
                        e.Graphics.DrawString("Choose", cmbTransactionType.Font, brush, e.Bounds);
                }
                else
                {
                    bool isSelected = (e.State & DrawItemState.Selected) == DrawItemState.Selected;

                    Color bgColor = Color.White; 
                    Color textColor = Color.Black;

                    using (Brush bgBrush = new SolidBrush(bgColor))
                        e.Graphics.FillRectangle(bgBrush, e.Bounds);

                    using (Brush textBrush = new SolidBrush(textColor))
                        e.Graphics.DrawString(cmbTransactionType.Items[e.Index].ToString(), cmbTransactionType.Font, textBrush, e.Bounds);
                }
                e.DrawFocusRectangle();
            };

            txtAmount = new TextBox
            {
                Font = new Font("Segoe UI", 16),
                Width = controlWidth,
                BorderStyle = BorderStyle.FixedSingle
            };
            txtAmount.KeyPress += (sender, e) =>
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
                {
                    e.Handled = true;
                }
                if (e.KeyChar == '.' && (sender as TextBox).Text.Contains('.'))
                {
                    e.Handled = true;
                }
            };

            numQuantity = new NumericUpDown
            {
                Font = new Font("Segoe UI", 16),
                Minimum = 1,
                Maximum = 1000,
                Value = 1,
                Width = controlWidth
            };

            AddCenteredControl("Choose Category", cmbCategory, 1);
            AddCenteredControl("Transaction Type", cmbTransactionType, 2);
            AddCenteredControl("Amount", txtAmount, 3);
            AddCenteredControl("Quantity", numQuantity, 4);

            int buttonsTopY = numQuantity.Bottom + 30;

            btnSave = new Button
            {
                Text = "Add",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                BackColor = Color.FromArgb(0, 200, 100),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(100, 40),
                DialogResult = DialogResult.OK
            };
            btnSave.Click += BtnSave_Click;

            btnCancel = new Button
            {
                Text = "Cancel",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                BackColor = Color.LightGray,
                ForeColor = Color.Black,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(100, 40),
                DialogResult = DialogResult.Cancel
            };

            int buttonsTotalWidth = btnSave.Width + btnCancel.Width + 30;
            int buttonsStartX = (formWidth - buttonsTotalWidth) / 2;

            btnSave.Location = new Point(buttonsStartX, buttonsTopY);
            btnCancel.Location = new Point(buttonsStartX + btnSave.Width + 30, buttonsTopY);

            this.Controls.Add(btnSave);
            this.Controls.Add(btnCancel);
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            LoadCategoriesFromDatabase();

            cmbCategory.SelectedIndexChanged += (s, ev) =>
            {
                if (cmbCategory.SelectedIndex >= 0)
                {
                    LoadTransactionTypesByCategory(cmbCategory.SelectedItem.ToString());
                }
            };

            cmbTransactionType.SelectedIndexChanged += (s, ev) =>
            {
                if (cmbCategory.SelectedIndex >= 0 && cmbTransactionType.SelectedIndex >= 0)
                {
                    LoadItemPrice(cmbCategory.SelectedItem.ToString(), cmbTransactionType.SelectedItem.ToString());
                }
                else
                {
                    txtAmount.Text = string.Empty;
                }
            };
        }

        private void LoadCategoriesFromDatabase()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT DISTINCT ItemCategory FROM PaymentItemMaster ORDER BY ItemCategory", conn))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        cmbCategory.Items.Clear();
                        while (reader.Read())
                        {
                            cmbCategory.Items.Add(reader["ItemCategory"].ToString());
                        }
                    }
                }
                cmbCategory.SelectedIndex = -1; // show placeholder
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load categories:\n" + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadTransactionTypesByCategory(string category)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT DISTINCT ItemDesc FROM PaymentItemMaster WHERE ItemCategory = @Category ORDER BY ItemDesc";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Category", category);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            cmbTransactionType.Items.Clear();
                            while (reader.Read())
                            {
                                cmbTransactionType.Items.Add(reader["ItemDesc"].ToString());
                            }
                        }
                    }
                }

                cmbTransactionType.SelectedIndex = -1;
                cmbTransactionType.Text = "Choose";
                cmbTransactionType.Invalidate();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load transaction types:\n" + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadItemPrice(string category, string itemDesc)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT ItemPrice FROM PaymentItemMaster WHERE ItemCategory = @Category AND ItemDesc = @ItemDesc";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Category", category);
                        cmd.Parameters.AddWithValue("@ItemDesc", itemDesc);
                        object result = cmd.ExecuteScalar();

                        txtAmount.Text = result != null && decimal.TryParse(result.ToString(), out decimal price)
                            ? price.ToString("0.00")
                            : string.Empty;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load item price:\n" + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (cmbCategory.SelectedIndex < 0)
            {
                MessageBox.Show("Please select a category.");
                return;
            }

            if (cmbTransactionType.SelectedIndex < 0)
            {
                MessageBox.Show("Please select a transaction type.");
                return;
            }

            if (!decimal.TryParse(txtAmount.Text.Trim(), out decimal parsedAmount) || parsedAmount < 0)
            {
                MessageBox.Show("Please enter a valid, non-negative amount.");
                return;
            }

            Category = cmbCategory.SelectedItem.ToString();
            TransactionType = cmbTransactionType.SelectedItem.ToString();
            Amount = parsedAmount;
            Quantity = (int)numQuantity.Value;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        protected override void WndProc(ref Message m)
        {
            const int WM_NCLBUTTONDOWN = 0xA1;
            const int HTCAPTION = 0x2;

            if (m.Msg == WM_NCLBUTTONDOWN && m.WParam.ToInt32() == HTCAPTION)
            {
                return; // prevent dragging the form
            }

            base.WndProc(ref m);
        }
    }
}
