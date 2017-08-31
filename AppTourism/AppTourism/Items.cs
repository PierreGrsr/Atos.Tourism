using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppTourism
{
    public class Items : ICollection<Item>
    {
        private List<Item> itemList;

        public Items ()
        {
            ItemList = new List<Item>();
        }




        public int Count => ((ICollection<Item>)ItemList).Count;

        public bool IsReadOnly => ((ICollection<Item>)ItemList).IsReadOnly;

        public List<Item> ItemList
        {
            get
            {
                return itemList;
            }

            set
            {
                itemList = value;
            }
        }

        public IEnumerator GetEnumerator()
        {
            return new ItemsEnum(this);
        }

        public string[] convertToString()
        {
            string[] itemString = new string[ItemList.Count];
            for(int i=0; i <= ItemList.Count - 1; i++)
            {
                itemString[i] = ItemList[i].Name;
            }
            return itemString;
        }

        IEnumerator<Item> IEnumerable<Item>.GetEnumerator()
        {
            return ((IEnumerable<Item>)ItemList).GetEnumerator();
        }

        public void Add(Item item)
        {
            ((ICollection<Item>)ItemList).Add(item);
        }

        public void Clear()
        {
            ((ICollection<Item>)ItemList).Clear();
        }

        public bool Contains(Item item)
        {
            return ((ICollection<Item>)ItemList).Contains(item);
        }

        public void CopyTo(Item[] array, int arrayIndex)
        {
            ((ICollection<Item>)ItemList).CopyTo(array, arrayIndex);
        }

        public bool Remove(Item item)
        {
            return ((ICollection<Item>)ItemList).Remove(item);
        }
    }
}
