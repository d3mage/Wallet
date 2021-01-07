using System;
using System.Collections.Generic;

namespace DAL
{
    [Serializable]
    public class Category
    {
        public string Name { get; set; }

        public Category() { }
        public Category(string name) => Name = name;
    }
}
