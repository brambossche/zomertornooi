using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marb.Bindinglist
{
    /// <summary>
    /// Binding list which overrides delete item to intercept before the item is removed from the collection    
    /// </summary>
    public class ExtBindingList<T> : BindingList<T>
    {

        public delegate void del_ItemAdded(object sender, T Item);
        public event del_ItemAdded ItemAdded;

        public delegate void del_ItemRemoved(object sender, T Item);
        public event del_ItemRemoved ItemRemoved;


        public ExtBindingList()
        {
            
        }
        public ExtBindingList(IList<T> inputlist)
            : base (inputlist)
        {
           
        }

        private string _Name = "";

        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        protected override void OnListChanged(ListChangedEventArgs e)
        {
            base.OnListChanged(e);

            if (e.ListChangedType == ListChangedType.ItemAdded)
            {
                if (ItemAdded != null)
                {
                    ItemAdded.Invoke(this,this[e.NewIndex]);
                }
            }
        }

        protected override void ClearItems()
        {
            //base.OnListChanged(new ListChangedEventArgs(ListChangedType.Reset,0));
            base.ClearItems();
        }

        protected T _Itemwhichwillberemoved;
        protected override void RemoveItem(int itemIndex)
        {

            if (this.Count > 0)
            {
                //itemIndex = index of item which is going to be removed
                //get item from binding list at itemIndex position
                _Itemwhichwillberemoved = this.Items[itemIndex];
                
                if (ItemRemoved != null)
                {
                    ItemRemoved(this, _Itemwhichwillberemoved);
                }
                base.RemoveItem(itemIndex);
            }
        }
    }
}
