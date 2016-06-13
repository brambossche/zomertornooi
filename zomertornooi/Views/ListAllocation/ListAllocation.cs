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
using Marb.Draganddrop;
using Marb.Bindinglist;
using structures;

namespace structures.Views
{

    //Don't start GUI from a different thread as is causes big issues for drag and drop which must be 
    //in as single state appartment and must run on the main UI thread!!!!
    //oherwise <AllowDrop = true> will cause citical error
    //[STAThread] must be assigned when you want somewhere drag and drop
    //http://stackoverflow.com/questions/3185861/system-invalidoperationexception-in-vs-2005
             

    public partial class UC_ListAllocation : UserControl
    {
        /// <summary>
        /// connection to the binding input source - changing this will cause Itemchanged event - wathch out!!
        /// </summary>
        private ExtBindingList<Ploeg> _BindingSourceList = new ExtBindingList<Ploeg>();

        public ExtBindingList<Ploeg> BindingSourceList
        {
            get { return _BindingSourceList; }
            set { _BindingSourceList = value; }
        }
        /// <summary>
        /// internal list which can be used to change and adapt items without the Itemchanged event gest fired
        /// </summary>
        private ExtBindingList<Ploeg> _inputlist = new ExtBindingList<Ploeg>();
        private int _NrOfOutputLists;

        private extendedListbox<Ploeg> _inputbox;
        private List<extendedListbox<Ploeg>> _outputboxes = new List<extendedListbox<Ploeg>>();
        public UC_ListAllocation()
        {
            InitializeComponent();
        }

        public UC_ListAllocation(ExtBindingList<Ploeg> BindingSourceList, int NrOfOuutputLists = 2)
            : this()
        {
            _NrOfOutputLists = NrOfOuutputLists;
            _BindingSourceList = BindingSourceList;
            //this creates a copy !!!! - bindig removed
            _inputlist = new ExtBindingList<Ploeg>(_BindingSourceList.ToList());            
        }


        public int NrOfOuutputLists
        {
            get
            {
                return _NrOfOutputLists;
            }

            set
            {
                _NrOfOutputLists = value;
                CreateBoxes();
            }
        }

        private void CreateBoxes()
        {

            //Create output boxes
            if (_NrOfOutputLists > _outputboxes.Count)
            {
                //_outputboxes.Reverse();
                for (int i = _outputboxes.Count; i < _NrOfOutputLists; i++)
                {
                    extendedListbox<Ploeg> _out = new extendedListbox<Ploeg>(new ExtBindingList<Ploeg>())
                    {
                        Dock = DockStyle.Top,
                        Name = _BindingSourceList.Name + "_Reeks_" + (i + 1).ToString(),
                        AllowDrop = true, 
                    };
                    _outputboxes.Add(_out);
                    _out.ItemAdded += ItemAdded;
                }
            }                                
            //removing a box - set back the items to the input box and list
            else if (_NrOfOutputLists < _outputboxes.Count)
            {
                //_outputboxes.Reverse();
                int AantalTeVerwijderen = _outputboxes.Count - _NrOfOutputLists;

                for (int i = 0; i < AantalTeVerwijderen; i++)
                {
                    extendedListbox<Ploeg> removebox = _outputboxes.Last();
                    foreach (Ploeg item in removebox.Items)
                    {
                        _inputlist.Add(item);
                    }
                    splitContainer1.Panel2.Controls.Remove(removebox);
                    splitContainer1.Panel2.Refresh();
                    _outputboxes.Remove(removebox);
                    removebox.Dispose();
                    removebox = null;
                }

            }

            //remove all box controls and labels
            splitContainer1.Panel2.Controls.Clear();
            //fill up the boxes
            _outputboxes.Reverse();
            foreach (extendedListbox<Ploeg> box in _outputboxes)
            {
                //add items in outputlist and clean input list
                foreach (Ploeg pl in _inputlist)
                {
                    if (pl.Reeksnaam == box.Name)
                    {
                        box.Items.Add(pl);
                    }
                }
                //clean the input 
                foreach (Ploeg pl in box.Items)
                {
                    _inputlist.Remove(pl);
                }
                splitContainer1.Panel2.Controls.Add(box);
                splitContainer1.Panel2.Controls.Add(new Label() { Text = box.Name, Dock = DockStyle.Top });
                
            }

            

            

            
            splitContainer1.Panel2.Refresh();

            ///input as last step becuase depending on items already in an output list, the input list gets filtered
            splitContainer1.Panel1.Controls.Clear();
            _inputbox = new extendedListbox<Ploeg>(_inputlist);
            //naam van de reeks
            _inputbox.Name = _BindingSourceList.Name;
            _inputbox.ItemAdded += ItemAdded;
            _inputbox.AutoSize = true;
            _inputbox.Dock = DockStyle.Top;
            Label _inputlabel = new Label() { Text = _inputbox.Name, Dock = DockStyle.Top};
            splitContainer1.Panel1.Controls.Add(_inputbox);
            splitContainer1.Panel1.Controls.Add(_inputlabel);



            _outputboxes.Reverse();
        }


        void ItemAdded(object sender, Ploeg Item)
        {
            if (((extendedListbox<Ploeg>)sender).Name == this.Name)
            {
                _BindingSourceList.Where(x => x.Ploegnaam == Item.Ploegnaam).First().Reeksnaam = "";
            }
            else
            {
                _BindingSourceList.Where(x => x.Ploegnaam == Item.Ploegnaam).First().Reeksnaam = ((extendedListbox<Ploeg>)sender).Name;
            }

        }



        public void DistributesInputOverOutput()
        {
            int i = 0;
            for (int ploegindex = 0; ploegindex < _inputlist.Count; ploegindex++)
            {
                if (i >= _outputboxes.Count())
                {
                    i = 0;
                }
                _outputboxes[i].Items.Add(_inputlist[ploegindex]);
                //this one actually writes to the binding list 
                _inputlist[ploegindex].Reeksnaam = _outputboxes[i].Name;
                _inputbox.Items.Remove(_inputlist[ploegindex]);
                i++;
            }
            _inputlist.Clear();
            _inputbox.Items.Clear();
        }

        public void SetItemsToInputList()
        {
            _inputbox.Items.Clear();
            for (int i = 0; i < _BindingSourceList.Count;i++ )
            {
                _BindingSourceList[i].Reeksnaam = "";
                if (!(_inputlist.Contains(_BindingSourceList[i])))
                {
                    _inputlist.Add(_BindingSourceList[i]);
                }
                if (!(_inputbox.Items.Contains(_BindingSourceList[i])))
                {
                    _inputbox.Items.Add(_BindingSourceList[i]);
                }


            }           
            foreach (extendedListbox<Ploeg> _out in _outputboxes)
            {
                _out.Items.Clear();
                _out.Refresh();
            }
            _inputbox.Refresh();                         
        }
    }
}
