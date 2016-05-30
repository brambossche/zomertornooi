using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace structures
{

    /// <summary>
    /// Inheritance from this class enbales to raise an event in the binding list class which can tell 
    /// the ID of the item which has been changed
    /// The content is already changed and must not be passed via this event!!! It's only a trigger
    /// Giving the ID gives a direct access in the list to search the item to be updated
    /// </summary>
    public class Structurebase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;


        public virtual void NotifyPropertyChanged(int id)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(id.ToString()));
            }
        }
    }


}
