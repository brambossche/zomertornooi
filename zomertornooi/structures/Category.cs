using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Lib.ExtendToolboxCtrl;
using ProgramDefinitions;

namespace structures
{
    public class Category : IDGV_ListBoxItem
    {
        private static List<Category> _Categories;
        private static List<string> _CategoryItems;
        private static bool _constructing = false;
        public Category()
        {
            if (!_constructing)
            {
                _constructing = true;
                _Categories = new List<Category>();
                _CategoryItems = new List<string>();
                Category _Category;
                foreach (var geslacht in Enum.GetValues(typeof(Geslacht)).Cast<Geslacht>())
                {
                    foreach (var niveau in Enum.GetValues(typeof(Niveau)).Cast<Niveau>())
                    {
                        _Category = new Category();
                        _Category._Geslacht = geslacht;
                        _Category._Niveau = niveau;
                        _Categories.Add(_Category);
                        _CategoryItems.Add(_Category.Categorynaam);
                    }
                }
               
            }
        }

        public string Categorynaam 
        {
            get 
            {
                return _Geslacht.ToString() + "_" + _Niveau.ToString();             
            }         
        }

        private Geslacht _Geslacht = Geslacht.Dames;

        [ReadOnly(true)]
        public Geslacht Geslacht
        {
            get { return _Geslacht; }
            set { _Geslacht = value; }
        }

        private Niveau _Niveau = Niveau.A;

        [ReadOnly(true)]
        public Niveau Niveau
        {
            get { return _Niveau; }
            set { _Niveau = value; }
        }

        public override string ToString()
        {
            return Categorynaam;
        }
        public static List<Category> Categories
        {
            get
            {
                return _Categories;
            }
        }

        /// <summary>
        /// Cast from string to Category class
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static implicit operator Category (string input)
        {
            try
            {
                Category temp = new Category();
                string[] splitted = input.Split('_');
                if (splitted.Count() == 2)
                {
                    try
                    {
                        temp._Geslacht = (Geslacht)Enum.Parse(typeof(Geslacht), splitted[0]);
                        temp._Niveau = (Niveau)Enum.Parse(typeof(Niveau), splitted[1]);
                        return temp;
                    }
                    catch
                    {
                        return new Category();
                    }
                }
                return new Category();
            }
            catch (InvalidCastException  e)
            {
                Console.WriteLine("cast exception" + e);
                return null;
            }
        }





        public object CastToType(string Item)
        {

            return ((Category)Item);
        }

        [Browsable(false)]
        [ReadOnly(true)]
        public List<string> Items
        {
            get
            {
                return _CategoryItems;
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }
}
