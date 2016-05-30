using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.ExtendToolboxCtrl
{
    public interface IDGV_ListBoxItem
    {
         
        List<string> Items {get;set;}
        object CastToType(string Item);
    }
}
