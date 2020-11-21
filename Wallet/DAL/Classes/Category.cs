using System;
using System.Collections.Generic;

namespace DAL
{
    [Serializable]
    public class Category
    {
        public string Name { get; set; }
        public List<MoneyEvent> moneyEvents;

        public Category() { }
        public Category(string name) => Name = name;
    }
}
