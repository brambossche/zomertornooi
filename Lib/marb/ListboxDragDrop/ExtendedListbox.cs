using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections.ObjectModel;
using Marb.Bindinglist;

namespace Marb.Draganddrop
{

    //Don't start GUI from a different thread as is causes big issues for drag and drop which must be 
    //in as single state appartment and must run on the main UI thread!!!!
    //oherwise <AllowDrop = true> will cause citical error
    //[STAThread] must be assigned when you want somewhere drag and drop
    //http://stackoverflow.com/questions/3185861/system-invalidoperationexception-in-vs-2005
             

    public class extendedListbox<T> : ListBox
    {


        public delegate void del_Itemchange(object source, T Item);
        public event del_Itemchange ItemAdded;
        public event del_Itemchange ItemRemoved;


        private static bool DragDropBusy = false;
        private static extendedListbox<T> _Source;
        private ExtBindingList<T> _items;
        public extendedListbox(ExtBindingList<T> items)
            : base()
        {
            _items = items;

            foreach (T Titem in _items)
            {
                Items.Add(Titem);
            };
            
            AllowDrop = true;
            
            this.Dock = DockStyle.Fill;            
            SelectionMode = SelectionMode.One;
            MouseUp += extendedListbox_MouseUp;
            MouseDown += ExtendedListbox_MouseDown;
            DragOver += ExtendedListbox_DragOver;
            DragDrop += ExtendedListbox_DragDropCompleted;

            _items.ItemAdded += _items_ItemAdded;
            _items.ItemRemoved += _items_ItemRemoved;

        }



        void _items_ItemRemoved(object sender, T Item)
        {
            if (!DragDropBusy)
            {
                this.Items.Remove(Item);
                this.RefreshItems();
            }
        }

        void _items_ItemAdded(object sender, T Item)
        {
            if (!DragDropBusy)
            {
                this.Items.Add(Item);
                this.RefreshItems();
            }
        }

       

        private void ExtendedListbox_DragDropCompleted(object sender, DragEventArgs e)
        {
            try
            {
                //prevent to do drag drop in the same window - will create an item copy
                if (((extendedListbox<T>)sender).Name != _Source.Name)
                {
                    //change target - step 1 update the items in the box
                    if (!(((extendedListbox<T>)sender).Items.Contains(_Source.SelectedItem)))
                    {
                        ((extendedListbox<T>)sender).Items.Add(_Source.SelectedItem);


                        if (((extendedListbox<T>)sender).ItemAdded != null)
                        {
                            ItemAdded.Invoke((extendedListbox<T>)sender, (T)_Source.SelectedItem);
                        }

                        if (_Source.ItemRemoved != null)
                        {
                            _Source.ItemRemoved.Invoke(_Source, (T)_Source.SelectedItem);
                        }
                        _Source.Items.Remove(_Source.SelectedItem);
                    }
                }
            }
            catch (Exception ee) 
            { 
            }
            DragDropBusy = false;
        }

        private void ExtendedListbox_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void ExtendedListbox_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                DragDropBusy = true;
                _Source = this;
                DoDragDrop(_Source.SelectedItem, DragDropEffects.Move);
            }
            catch (Exception ee)
            {
                DragDropBusy = false;
                Console.WriteLine("error" + ee.ToString());
            }
        }
        void extendedListbox_MouseUp(object sender, MouseEventArgs e)
        {
            DragDropBusy = false; 
        }
    }
}
