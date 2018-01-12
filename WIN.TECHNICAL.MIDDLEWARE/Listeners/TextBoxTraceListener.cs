using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Collections;
using System.Windows.Forms;

namespace WIN.TECHNICAL.MIDDLEWARE.Listeners
{
    public class TextBoxTraceListener : TraceListener
    {
        TextBox textBox;
        ArrayList lines = new ArrayList();

        public TextBoxTraceListener(TextBox textBox)
        {
            this.textBox = textBox;
        }

        public override void Write(string message)
        {
            WriteLine(message);
            System.Windows.Forms.Application.DoEvents();
        }

        public override void WriteLine(string message)
        {
            lines.Add(message);
            if (lines.Count > 100)
            {
                lines.RemoveAt(0);
            }
            textBox.Lines = (string[])lines.ToArray(typeof(string));
            textBox.SelectionLength = textBox.Text.Length;
            textBox.ScrollToCaret();
            System.Windows.Forms.Application.DoEvents();
        }
    }
}
