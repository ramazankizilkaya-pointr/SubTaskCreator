using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubTaskCreator
{
    public class Logger
    {
        private RichTextBox _logBox;

        public Logger(RichTextBox logBox)
        {
            _logBox = logBox;
        }

        public void LogMessage(string message)
        {
            if (_logBox.InvokeRequired)
            {
                _logBox.Invoke(new MethodInvoker(delegate {
                    _logBox.AppendText( message + Environment.NewLine);
                }));
            }
            else
            {
                _logBox.AppendText(message + Environment.NewLine);
            }

            _logBox.ScrollToCaret();
        }
    }
}
