==================================================
  SERIALPORTMANAGER: ARDUINO COM PORT DETECTION
==================================================

DESCRIPTION:
------------
This singleton class handles serial port detection, opening, and closing 
for an Arduino device connected via USB. 

Main responsibilities include:
- Auto-detect the Arduino COM port using WMI queries.
- Open and close the serial port safely.
- Provide a global accessible instance for other forms/classes.

FUNCTIONAL DETAILS:
-------------------
- Singleton pattern ensures only one instance manages the serial port.
- Uses Windows Management Instrumentation (WMI) to scan all PnP devices with COM ports.
- Filters device names containing "Arduino", "CH340", or "USB Serial" to identify Arduino ports.
- Extracts the COM port name in the format "COMx".
- Provides `Open()` method to open the port at 9600 baud.
- Provides `Close()` method to close and dispose of the port.
- Shows message boxes on errors or if no Arduino is found.

USAGE:
------
- Access the instance via `SerialPortManager.Instance`.
- Call `Open()` to open the serial port.
- Access the port with `SerialPortManager.Instance.Port` for data communication.
- Call `Close()` to safely release the port resources.

ERROR HANDLING:
---------------
- Displays message box if Arduino is not detected or port fails to open.
- Silently catches exceptions during port closing to prevent crashes.

DEPENDENCIES:
-------------
- Requires System.Management assembly for WMI queries.
- Requires System.IO.Ports for SerialPort class.
- Uses Windows Forms for message dialogs.

NOTES:
------
- Auto-detection logic looks for common Arduino USB serial signatures.
- Assumes Arduino runs at 9600 baud rate.
- Can be extended to support other baud rates or device types if needed.
