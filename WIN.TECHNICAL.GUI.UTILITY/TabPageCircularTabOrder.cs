//#define LOOP_PAGES

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

/*************************************************************************/
/*   Written By Ralph Varjabedian                                        */
/*   You may use this code freely and copy this code provided that       */
/*   You do not remove this copyright notice.                            */
/*   July 2008                                                           */
/*   http://varjabedian.net                                              */
/*   Revision 1.0                                                        */
/*************************************************************************/

namespace WIN.TECHNICAL.GUI.COMMONS
{
    public class TabPageCircularTabOrder : TabPage
    {
        public TabPageCircularTabOrder(string title)
            : base(title)
        {
        }

        public TabPageCircularTabOrder()
            : base()
        {
        }

        [DllImport("user32.dll")]
        static extern IntPtr GetFocus();

        [DllImport("user32.dll")]
        static extern IntPtr SetFocus(IntPtr hWnd);

        [DllImport("user32.dll")]
        static extern IntPtr GetNextDlgTabItem(IntPtr hDlg, IntPtr hCtrl, Int32 previous);

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Tab || keyData == (Keys.Shift | Keys.Tab))
            {
                bool handled = false;
                IntPtr hFocusedCtrl = GetFocus();

                IntPtr hLastCtrl  = GetNextDlgTabItem(Handle, IntPtr.Zero, 0);
                IntPtr hFirstCtrl = GetNextDlgTabItem(Handle, hLastCtrl, 1);

                if ((keyData & Keys.Shift) == Keys.Shift) // shift pressed, move back
                {
                    if (hFocusedCtrl == hFirstCtrl)
                        handled = JumpToNextTabGroup(true);
                }
                else // move forward
                {
                    if (hFocusedCtrl == hLastCtrl)
                        handled = JumpToNextTabGroup(false);
                }
                if (handled)
                    return true;
            }
            return base.ProcessDialogKey(keyData);
        }

        private bool JumpToNextTabGroup(bool previous)
        {
            TabPage page = GetAndSetNextPage(previous);
            if (page == null)
                return false;
            
            IntPtr hLastCtrl = GetNextDlgTabItem(page.Handle, IntPtr.Zero, 0);
            IntPtr hFirstCtrl = GetNextDlgTabItem(page.Handle, hLastCtrl, 1);

            SetFocus(!previous ? hFirstCtrl : hLastCtrl);
            return true;
        }

        private TabPage GetAndSetNextPage(bool previous)
        {
            TabControl tabControl = (TabControl) Parent;
            int i = Parent.Controls.GetChildIndex(this);

            if (previous)
            {
                if (i > 0)
                    i--;
                else
#if LOOP_PAGES
                    i = tabControl.TabPages.Count - 1;
#else
                    return null;
#endif
            }
            else
            {
                if (i < tabControl.TabPages.Count - 1)
                    i++;
                else
#if LOOP_PAGES
                    i = 0;
#else
                    return null;
#endif
            }
            
            TabPage nextPage = tabControl.TabPages[i];
            tabControl.SelectedTab = nextPage;
            return nextPage;
        }
    }
}
