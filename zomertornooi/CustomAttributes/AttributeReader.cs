using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomAttributes
{
    public static class AttributeReader
    {
        public static bool ReadAttribute_ShowInPropertyGrid(object obj )
        {
            var customAttributes = (PropertyGridViewer[])(obj.GetType()).GetCustomAttributes(typeof(PropertyGridViewer), true);
            if (customAttributes.Length > 0)
            {
                var myAttribute = customAttributes[0];
                return myAttribute.ShowInPropertyGrid;                
            }
            return false;
        }
    }
}
