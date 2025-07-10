===============================================================
             TRANSACTION MODAL - Form4.cs
===============================================================

1. PURPOSE
---------------------------------------------------------------
Form4 is a modal dialog used to add a new transaction entry.
It lets the user:
- Select a Category from the database-driven dropdown.
- Select a Transaction Type filtered by the selected Category.
- View and edit the Amount (auto-filled based on selection).
- Specify the Quantity.
- Confirm or cancel the addition of the transaction.

3. DATABASE INTERACTIONS
---------------------------------------------------------------
- Uses SQL Server connection string to CampusSolution DB.
- LoadCategoriesFromDatabase(): Loads distinct ItemCategory.
- LoadTransactionTypesByCategory(string): Loads ItemDesc filtered by selected category.
- LoadItemPrice(string, string): Loads ItemPrice based on category and transaction type.

All DB calls use parameterized queries and proper exception handling.
Errors show message box alerts.

2. VALIDATIONS
---------------------------------------------------------------
- Category and Transaction Type must be selected.
- Amount must be a valid, non-negative decimal.
- Quantity is restricted via NumericUpDown control.
- If validation fails, user is prompted with a message box.

3. EVENTS
---------------------------------------------------------------
- Form4_Load: Loads categories and attaches event handlers.
- cmbCategory.SelectedIndexChanged: Reloads transaction types for new category.
- cmbTransactionType.SelectedIndexChanged: Loads price into txtAmount.
- txtAmount.KeyPress: Restricts input to digits and one decimal point.
- btnSave.Click: Validates inputs, sets public properties, closes dialog with OK result.
- btnCancel.DialogResult: Cancel closes dialog without saving.

4. PROPERTIES (for caller retrieval)
---------------------------------------------------------------
- string TransactionType { get; private set; }
- string Category { get; private set; }
- decimal Amount { get; private set; }
- int Quantity { get; private set; }

