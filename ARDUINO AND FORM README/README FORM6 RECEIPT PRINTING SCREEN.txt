==================================================
              FORM6: RECEIPT PRINTING SCREEN
==================================================

DESCRIPTION:
------------
Form6 is the final screen displayed after a payment transaction.
It instructs the user to take their printed receipt and automatically returns to Form1 after a delay (20 Seconds countdown).

Main features include:
- Shows a centered message prompting the user to "Please take your receipt........".
- Displays footer information including:
  - Left footer: "Powered by: AMA - SDI (2025)"
  - Right footer: Current date and time, updated every second.
- Maximized, borderless form with a smooth gradient background.
- Starts a 20-second countdown timer after which it automatically closes and returns to the main screen (Form1).
- Supports closing via ESC key.

CONTROLS & UI:
--------------
- lblCenterMessage: Large bold centered message label.
- footerLeft: Footer label on the bottom-left with branding.
- footerRight: Footer label on the bottom-right showing live clock.
- clockTimer: Timer that updates the footerRight clock every second.
- returnTimer: Timer that closes this form after 20 seconds and shows Form1 again.

KEY FUNCTIONALITY:
------------------
- On load:
  - Sets background to white with a vertical gradient.
  - Creates and positions UI elements dynamically.
  - Starts clockTimer to update the time label every second.
- Starts returnTimer via `StartReturnTimer()` method to auto-close form after 20 seconds.
- Handles window resize to reposition labels properly.
- Overrides OnPaint to draw gradient background.
- Overrides ProcessCmdKey to listen for ESC key to close immediately.

INTER-FORM COMMUNICATION:
-------------------------
- When returnTimer expires, hides and closes Form6 and shows the singleton instance of Form1.

KNOWN LIMITATIONS / TODOS:
--------------------------
- Assumes Form1 is implemented as a singleton with an Instance property.
- No user interaction besides ESC to close or wait for auto-close.
- No receipt printing logic here; this form assumes printing is done beforehand.

DEPENDENCIES:
-------------
- Requires Form1 to show again after timeout.
- Uses standard .NET WinForms controls and timers.
- Uses AMA branding and clock formats consistently with other forms.
