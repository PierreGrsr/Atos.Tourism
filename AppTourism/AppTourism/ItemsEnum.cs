using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppTourism
{
    public class ItemsEnum : IEnumerator
    {
        int position = -1;
        public Items items;


        public ItemsEnum(Items list)
        {
            items = list;
        }

        public object Current
        {
            get
            {
                return CurrentItem;
            }
        }

        public Item CurrentItem
        {
            get
            {
                try
                {
                    return items.ItemList[position];
                }
                catch(IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }

        public bool MoveNext()
        {
            position++;
            return (position < items.ItemList.Count);
        }

        public void Reset()
        {
            position = -1;
        }
    }
}
