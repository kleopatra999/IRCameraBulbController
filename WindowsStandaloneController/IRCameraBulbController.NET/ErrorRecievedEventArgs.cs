using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IRCameraBulbController.NET
{
    class ErrorRecievedEventArgs : EventArgs
    {
        public string ErrorMessage { get; set; }

        public ErrorRecievedEventArgs() { }

        public ErrorRecievedEventArgs(string message)
        {
            this.ErrorMessage = message;
        }
    }
}
