using System;
using System.Collections.Generic;

namespace Coolpc_PriceTracker.Client.Models
{
  public  class ItemListModel
    {
        //public List<>
        //private List<ItemObject> ItemObjectList;
        public ItemClasses itemClasses = new ItemClasses();
        // public List<List<List<ItemObject>>> ItemClass;
        // public List<List<List<ItemObject>>> ItemClass;
        public class ItemObject
        {
            public string ItemName { get; set; }
            public bool IsHotItem { get; set; }
            public int Price { get; set; }
        }
        public class ItemGroup
        {
            public List<ItemObject> itemGroup = new List<ItemObject>();
            public string groupName { get; set; }
        }
        public class ItemClass
        {
            public List<ItemGroup> itemGroups = new List<ItemGroup>();
            public string className { get; set; }
        }
        public class ItemClasses
        {
            public DateTime dataUpdatedTime = new DateTime();

            public List<ItemClass> itemClasses = new List<ItemClass>();
        }
    }
}

