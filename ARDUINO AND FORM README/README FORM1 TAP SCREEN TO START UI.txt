==================================================
          FORM1: TAP SCREEN TO START UI
==================================================

FORM NAME:
----------
Form1.cs

DESCRIPTION:
------------
Form1 acts as the **first screen** in the kiosk system.
It displays a tap gesture and waits for user interaction (touch/mouse click).
Once tapped, it transitions to Form2.

Additionally, this form:
 Initializes and opens the Serial Port connected to Arduino.  
 Listens for bill and coin insertion events from Arduino.  
 Updates Form5 (if open) with the inserted amount.  
 Handles graceful SerialPort disposal on close.  

CONTROLS & UI:
--------------
- `tap_gesture`: Displays a double-tap gesture animation.
- `tapLabel`: Text instructing users to "Touch screen to start".
- `ama_logo`: Centered institution logo.
- Custom gradient background and layout.
- Mouse click triggers transition to Form2.

SERIAL PORT INITIALIZATION:
---------------------------
- Uses Singleton `SerialPortManager` to auto-detect COM port.
- Detects ports such as COM3/CH340/Arduino USB Serial.
- Subscribes to `DataReceived` event.
- On receiving "BILL:" or "COIN:", parses the value and:
  - Updates `totalBillAmount` and `totalCoinAmount`
  - Calculates the `totalAmountInserted`
  - Sends updated amount to Form5 (if active)

SERIAL FORMAT EXPECTED FROM ARDUINO:
------------------------------------
- `BILL: <int>`  → Total bill amount
- `COIN: <int>`  → Total coin amount

ESC KEY FUNCTION:
-----------------
- Pressing `ESC` closes the form (used for admin/testing purposes).

ON FORM CLOSE:
--------------
- Ensures serial port is properly closed via `SerialPortManager.Instance.Close()` to avoid port lock issues.

INTER-FORM COMMUNICATION:
-------------------------
- Form1 stores a reference to Form5 (via `SetForm5Instance()`)
- This is used to push updates (like inserted amount) to the Form5 UI labels.

KNOWN LIMITATIONS / TODOS:
---------------------------
- Student ID is hardcoded in `OpenPaymentForm()` (e.g., "12345")
- Form3 creation is only used if `OpenPaymentForm()` is uncommented
- COM port errors are silently handled; recommend adding user-facing error logs if needed

DEPENDENCIES:
-------------
- Requires `SerialPortManager.cs` to auto-detect and open COM port.
- Relies on `Form5.cs` to display inserted cash.
- Uses resources like `doubletap_gesture` and `ama_logo`.


