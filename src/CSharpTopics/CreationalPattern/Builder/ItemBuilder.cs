using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreationalPattern.Builder
{
    public class ItemBuilder
    {
        private Item _item;
        public ItemBuilder() 
        {
            _item = new Item();
        }
        public void SetValue1(string itemValue)
        {
            //Check various logic, before setting a value
            _item.Value1 = itemValue;
        }
        public void SetValue2(string itemValue)
        {
            _item.Value2 = itemValue;
        }
        public void SetValue3(string itemValue)
        {
            _item.Value3 = itemValue;
        }
        public void SetValue4(string itemValue)
        {
            _item.Value4 = itemValue;
        }

        public void SetValue5(string itemValue)
        {
            _item.Value5 = itemValue;
        }
        public Item GetItem()
        {
            return _item;
        }
    }
}
