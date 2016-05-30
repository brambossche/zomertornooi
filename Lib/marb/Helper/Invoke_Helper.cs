using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marb.Extender.Invoke
{
    public static class EasyInvoke
    {
        public static void Invoke(this System.Windows.Forms.Control control, Action action)
        {
            if (control == null) throw new ArgumentNullException("@this");
            if (control == null) throw new ArgumentNullException("action");
            if (control.InvokeRequired)
            {
                control.Invoke(action);
            }
            else
            {
                action();
            }
        }
    }
}
