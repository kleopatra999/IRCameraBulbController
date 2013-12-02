using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Ports;
using System.Linq;
using System.Text;

namespace IRCameraBulbController.NET
{
    class CameraController
    {

        public event ErrorRecievedHandler ErrorRecieved;
        public delegate void ErrorRecievedHandler(object sender, ErrorRecievedEventArgs e);

        private SerialPort _port;
        private int _baud_rate = 9600;
        private bool _busy = false;

        public int Duration { get; set; }
        public int Gap { get; set; }
        public int Quantity { get; set; }
        public bool MirrorUp { get; set; }
        public string PortName
        {
            get
            {
                return (_port != null) ? _port.PortName : "";
            }
            set
            {
                if (_port != null)
                {
                    _port.Close();
                    _port.DataReceived -= Port_DataReceived;
                }
                if (!String.IsNullOrEmpty(value))
                {
                    _port = new SerialPort(value, _baud_rate);
                    _port.DataReceived += Port_DataReceived;
                }
                else
                {
                    _port = null;
                }
            }
        }
        public bool Busy { get { return _busy; } }


        public CameraController(int rate)
        {
            this._baud_rate = rate;
        }

        public void Start()
        {
            if (_port != null)
            {
                string command = string.Format("EXP:{0}:{1}:{2}:{3}\n", this.Duration, this.Gap, this.MirrorUp ? 1 : 0, this.Quantity);
                SendCommand(command);
            }
        }

        public void Abort()
        {
            SendCommand("ABT\n");
        }


        protected void OnErrorRecieved(ErrorRecievedEventArgs e)
        {
            ErrorRecieved(this, e);
        }


        private void Port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string data = _port.ReadLine();
            Debug.WriteLine(String.Format("Data recieved: {0}", data));

            if (data.Length > 3)
            {
                string command = data.Substring(0, 3).ToUpper();
                switch (command)
                {
                    case "SRT":
                        _busy = true;
                        break;
                    case "FIN":
                        _busy = false;
                        break;
                    case "ERR":
                        string s = data.Substring(data.IndexOf(':') + 1);
                        OnErrorRecieved(new ErrorRecievedEventArgs(s));
                        break;
                }
            }

        }


        private void SendCommand(string command)
        {
            if (_port != null)
            {
                if (!_port.IsOpen) _port.Open();
                _port.Write(command);

                Debug.WriteLine(String.Format("Data sent: {0}", command));
            }
        }
    }
}
