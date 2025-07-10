==================================================
            FORM2: STUDENT ID ENTRY UI
==================================================

FORM NAME:
----------
Form2.cs

DESCRIPTION:
------------
Form2 is the second form in the payment kiosk flow.
It allows the user to enter their **Student ID Number** using an **on-screen numeric keypad**.

Main functions:
 Accepts numeric input only  
 Validates Student ID against local database  
 Shows student details in a confirmation modal  
 Transitions to Form3 after confirmation  
 Displays real-time clock and footer branding  
 Styled with gradient background and responsive layout  

UI ELEMENTS:
------------
1. **TextBox (`studentIdBox`)**
   - Displays "Enter Student ID Number" placeholder
   - Accepts up to 11 digits (alphanumeric blocked)
   - Clears placeholder when focused

2. **TableLayoutPanel (`keypadPanel`)**
   - Contains 12 touch buttons: 0–9, delete (x), and next (➡)
   - Dynamically builds keypad layout (4x3)

3. **Footer Labels (`footerLeft`, `footerRight`)**
   - `footerLeft`: Static text – “Powered by: AMA - SDI (2025)”
   - `footerRight`: Real-time date and clock updated every second

4. **Panel (`centerPanel`)**
   - Container for keypad and input field
   - Responsive centering on form resize

LOGIC FLOW:
-----------
1. User taps numbers to enter their Student ID.
2. Taps "Next" ➡
3. System checks if input is valid and not empty
4. Checks database via `DatabaseHelper.GetStudentById(id)`
5. If not found → error message
6. If found → show confirmation modal with:
   - Student ID
   - Name
   - Branch
7. If confirmed:
   - Opens `Form3`, passing the student ID
   - Hides current form

SPECIAL FEATURES:
-----------------
- Custom delete and next buttons as image buttons
- Removes input if "del" is tapped
- Form3 receives validated student ID
- Placeholder handling avoids unintentional input confusion
- Resizing support for center panel and footer

DEPENDENCIES:
-------------
- `DatabaseHelper.cs`: To query student records
- `ConfirmationModal.cs`: To confirm identity before continuing
- `Form3.cs`: Next step in flow (payment screen)
- `Properties.Resources`: Requires `delete.png` and `right_arrow.png` image assets

IMPORTANT NOTES:
----------------
- Student ID placeholder remains gray unless typing begins
- "Enter Student ID Number" string is reused for focus logic
- TextBox is hard-disabled from shortcuts like paste
- Keypad is responsive using `TableLayoutPanel` with flexible columns/rows
- Gradient is repeated on each paint and resize




