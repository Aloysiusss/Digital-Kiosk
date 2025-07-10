==================================================
          FORM5: BILL & COIN PAYMENT SCREEN
==================================================

DESCRIPTION:
------------
Form5 is the payment interface where users insert bills and coins to pay a specified amount.
It:
- Displays the amount due and the total amount inserted so far.
- Dynamically shows inserted denominations (e.g., "₱20 Bill", "₱5 Coin").
- Controls navigation via Back and Next buttons.
- Communicates with Arduino hardware (through Form1) to receive inserted cash data.
- Sends transaction and receipt print commands via serial port.
- Updates database with payment and inserted denominations.
- Displays real-time clock and status messages to guide the user.

CONTROLS & UI:
--------------
- btnBack (PictureBox): Back button to return to Form3.
- btnNext (PictureBox): Next button to confirm payment and proceed.
- lblForPaymentValue (Label): Shows total amount due (e.g., "₱500.00").
- lblAmountInsertedValue (Label): Shows current inserted total.
- lblDenomination (Label): Lists inserted bill/coin denominations dynamically.
- pnlDenomination (Panel): Scrollable container for denomination labels.
- lblMessage (Label): Displays prompts like "Please Insert Bills".
- lblDateTime (Label): Shows current date and time, updated every second.
- lblFooterLeft (Label): Displays footer branding text.

KEY FUNCTIONALITY:
------------------
- Initializes UI dynamically on Form5_Load.
- Updates inserted amount and denomination labels via UpdateInsertedAmount called from Form1 when Arduino reports inserted cash.
- Shows/hides navigation buttons based on amount inserted vs. amount due.
- Generates a transaction code string from inserted denominations for record keeping.
- On clicking Next:
  - Validates payment amount.
  - Sends transaction details line-by-line over serial port for receipt printing.
  - Waits for "DONE PRINTING" confirmation from printer.
  - Updates student balance and records inserted denominations in database.
  - Opens Form6 for next process step.
- Back button returns to previous Form3 without saving.
- Keyboard ESC key closes form gracefully.

SERIAL COMMUNICATION:
---------------------
- Sends transaction lines, total amount, time, transaction code, and student ID to printer via serial port (through Form1).
- Commands sent: transaction data → "print" → await "DONE PRINTING" → "reset" → "power_off".
- Reads serial responses and handles errors or timeouts.

DATABASE INTERACTIONS:
----------------------
- Uses DatabaseHelper class to update student balance with inserted payment amount. 
- Records each inserted denomination and transaction code in the database.

UI BEHAVIOR NOTES:
------------------
- Form is borderless and maximized for kiosk use.
- Back button only visible when no payment inserted.
- Next button appears only when inserted amount meets or exceeds amount due.
- Denominations scroll vertically if many inserted.
- Clock updated every second via Timer.

DEPENDENCIES:
-------------
- Depends on Form1 for serial port communication and inserted amount notifications.
- Uses DatabaseHelper for DB operations.
- Requires Form3 reference for navigation back.
- Uses resource images for buttons (leftarrow, rightarrow).
