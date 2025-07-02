using System;
using System.IO.Ports;
using System.Windows.Forms;
using System.Management;


namespace billGUI
{
    public class SerialPortManager
    {
        private static SerialPortManager instance;
        public SerialPort Port { get; private set; }

        private SerialPortManager()
        {
            string portName = DetectArduinoPort();
            if (portName != null)
            {
                Port = new SerialPort(portName, 9600);
            }
            else
            {
                MessageBox.Show("Arduino not found. Please check the connection.");
            }
        }

        public static SerialPortManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new SerialPortManager();
                return instance;
            }
        }

        public bool Open()
        {
            if (Port == null)
                return false;

            if (!Port.IsOpen)
            {
                try
                {
                    Port.Open();
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to open serial port: " + ex.Message);
                }
            }
            return false;
        }

        public void Close()
        {
            try
            {
                if (Port != null && Port.IsOpen)
                {
                    Port.Close();
                    Port.Dispose();
                }
            }
            catch { }
        }

        // Auto-detects Arduino's COM port
        private string DetectArduinoPort()
        {
            try
            {
                using (var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PnPEntity WHERE Name LIKE '%(COM%'"))
                {
                    foreach (var device in searcher.Get())
                    {
                        string name = device["Name"]?.ToString();

                        if (name != null && (name.Contains("Arduino") || name.Contains("CH340") || name.Contains("USB Serial")))
                        {
                            int startIndex = name.LastIndexOf("(COM");
                            int endIndex = name.LastIndexOf(")");
                            if (startIndex != -1 && endIndex != -1)
                            {
                                string portName = name.Substring(startIndex + 1, endIndex - startIndex - 1); 
                                //MessageBox.Show($"Auto-detected Arduino on: {portName}", "Serial Port Detection", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return portName;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error detecting serial port: " + ex.Message);
            }

            return null;
        }

    }
}
