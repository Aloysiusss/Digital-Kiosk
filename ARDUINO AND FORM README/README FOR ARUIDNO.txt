==================================================
        ARDUINO RECEIPT KIOSK CONTROLLER
==================================================

DESCRIPTION:
------------
This Arduino sketch handles:
1. Reading pulses from coin and bill acceptors.
2. Summing inserted amounts.
3. Receiving and processing commands from a Windows UI (Form5).
4. Generating and printing a formatted official receipt via a thermal printer.

PIN ASSIGNMENTS:
----------------
- Coin Acceptor Pin: D3 (ARDUINO DIGITAL PIN 3)
- Bill Acceptor Pin: D2 (ARDUINO DIGITAL PIN 2)
- Relay Control Pin: D7 (Used for enabling/disabling peripherals)
- Thermal Printer TX Pin: D11
- Thermal Printer RX Pin: D10

FEATURES:
---------
 Real-time counting of inserted bills and coins  
 Communicates with PC via Serial (9600 baud)  
 Accepts commands to print a receipt or reset data  
 Prints formatted receipts including:
   - Student ID
   - Date & Time
   - Itemized payments
   - Total and Paid Amount
 Supports up to 20 transaction lines from the UI 

SETUP INSTRUCTIONS:
-------------------
1. Connect coin acceptor signal to D3
2. Connect bill acceptor signal to D2
3. Connect relay module control to D7 (optional)
4. Connect thermal printer TX to D11, RX to D10
5. Upload the code via Arduino IDE
6. Ensure serial communication with PC over USB (COMx)

COMMANDS FROM PC (Form5):
-------------------------
- "reset"       → Clears all counters and data
- "power_on"    → Enables coin/bill detection (activates relay)
- "power_off"   → Disables detection and deactivates relay
- "print"       → Prints the formatted receipt if not already printed
- "TRN|..."     → Adds a transaction item line
- "TOTAL|..."   → Sets the total amount
- "TIME|..."    → Sets the date and time for receipt
- "TRANSNO|..." → Sets custom transaction number (CUSTOM TRANSACTION CODE -- BILL: A(20),B(50),C(100),D(200),E(500),F(1000) and COIN: 1 (1PHP),2(5 PHP),3(10 PHP),4 (20 PHP))
- "STUDENT|..." → Sets student ID

RECEIPT FORMAT:
---------------
- Center-aligned header
- Date and time
- Campus and terminal ID
- Random invoice number
- Transaction ID and student ID
- Item list with quantity and amount
- Total and Paid Amount
- Footer message

INTERRUPT LOGIC:
----------------
- Coin pulses increase `coinPulseCount`
- Bill pulses increase `billPulseCount`
- When no pulse is detected for 300ms+, amount is calculated:
   - Bill pulse × 10 pesos
   - Coin pulse × 1 peso

LIMITATIONS:
------------
- Only supports up to 20 lines of transaction data
- Receipt can only be printed once per session
- Pulse value assumptions (e.g., 1 pulse = 10 pesos bill) must match acceptor's configuration

NOTES:
------
- The printer uses SoftwareSerial; avoid high baud rates.
- `systemActive` flag ensures pulses are only counted after "power_on"
- Data is sent to the PC in this format:
   - "BILL: <amount>"
   - "COIN: <amount>"
- `printer.flush()` ensures all data is printed before delay

