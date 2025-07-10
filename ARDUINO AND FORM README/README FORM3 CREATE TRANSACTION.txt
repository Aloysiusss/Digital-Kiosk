===============================================================
             CREATE TRANSACTION Form3.cs
===============================================================

1. PURPOSE
---------------------------------------------------------------
Form3 is a full-screen WinForms interface designed to:
- Accept and display transaction entries for a specific student.
- Allow additions and deletions of transactions.
- Automatically compute and display the total bill.
- Enable navigation to next/previous forms.
- Communicate with Arduino via serial connection (power_on).
- Present a clean and responsive layout with clock and footer.

UI Components:
- DataGridView (dgv): Displays transaction list.
- Label (lblTotal): Shows total amount.
- PictureBoxes (btnBack, btnNext): Navigation controls.
- Labels (lblTitle, footerLeft, footerRight): Headers and footer.
- Button (btnAdd): Triggers add transaction modal.
- Timer (clockTimer): Updates footerRight clock every second.

Events:
- Form3_Load: Initializes and places controls.
- Resize: Repositions layout and clock dynamically.
- ClockTimer_Tick: Updates real-time clock.
- btnAdd_Click: Opens Form4 to add a transaction.
- btnBack_Click: Navigates to Form2.
- btnNext_Click: Saves data, sends command to Arduino, goes to Form5.
- dgv.CellClick: Removes row if "Remove" button clicked.

3. DATA HANDLING
---------------------------------------------------------------
Class: TransactionItem
- TransactionType (string)
- Quantity (int)
- Amount (decimal)

DataGridView Columns:
- Transaction Type
- Amount
- Quantity
- Final Amount (computed)
- Action (Button: "Remove")

GetTransactions(): Returns list of TransactionItem objects.
UpdateTotal(): Sums all final amounts and updates lblTotal.
SaveTransactions(): Outputs each transaction to console.

2. NAVIGATION LOGIC
---------------------------------------------------------------
- btnBack: Closes Form3 and returns to Form2.
- btnNext:
   a. Checks for transactions.
   b. Opens Form1 serial port if needed, sends "power_on".
   c. Saves data.
   d. Opens Form5 and hides Form3.

3. SHORTCUTS
---------------------------------------------------------------
ESC: Closes the form immediately.

4. DEPENDENCIES
---------------------------------------------------------------
- Form1: Holds SerialPort instance to communicate with Arduino.
- Form2: Previous screen.
- Form4: Modal to input new transaction (not included here).
- Form5: Final step or summary screen.

5. NOTES & RECOMMENDATIONS
---------------------------------------------------------------
- Form4 and Form5 should follow similar UI patterns.
- Ensure transaction values are validated in Form4.
- Proper disposal of serial port must be handled in Form1.
- Consider separating data logic from UI for maintainability.
- Check for nulls or invalid values when reading DataGridView.

