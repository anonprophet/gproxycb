using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace GMaster
{
    class Print
    {
        delegate void PrintSafe(ref RichTextBox rtb, string line, Color col, bool addnewline);
        delegate void PrintSafeLB(ref ListBox lb, string item, int action);
        delegate void PrintSafeTextBox(ref TextBox txt, string text);
        public static void RtbPrint(ref RichTextBox rtb, string line, Color col, bool addnewline)
        {
            if (rtb.InvokeRequired)
            {
                PrintSafe i = new PrintSafe(RtbPrint);
                rtb.Invoke(i, new object[] { rtb, line, col, addnewline });
            }
            else
            {
                rtb.SelectionStart = rtb.TextLength;
                rtb.SelectionLength = 0;
                rtb.SelectionColor = col;
                rtb.SelectedText = line;
                if (addnewline)
                    rtb.AppendText(Environment.NewLine);
                rtb.ScrollToCaret();
            }
        }
        public static void LbPrint(ref ListBox lb, string item,int action )
        {
            if (lb.InvokeRequired)
            {
                PrintSafeLB i = new PrintSafeLB(LbPrint);
                lb.Invoke(i,new object[]{lb,item,action});
            }
            else
            {
                if (action == 1 )
                {
                    lb.Items.Add(item);
                }
                else if (action == 2)
                {
                    lb.Items.Remove(item);
                }
                else if ( action == 3)
                {
                    lb.Items.Clear( );
                }
            }
        }
        public static void TxtPrint(ref TextBox txt, string text)
        {
            if (txt.InvokeRequired)
            {
                PrintSafeTextBox i = new PrintSafeTextBox(TxtPrint);
                txt.Invoke(i, new object[] { txt, text });
            }
            else
            {
                txt.Text = text;
            }
        }

    }

}
