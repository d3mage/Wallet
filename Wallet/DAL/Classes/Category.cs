using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    [Serializable]
    public class Category
    {
        public string Name { get; set; }
        public List<MoneyProfit> MoneyProfits { get; set; }
        public List<MoneyExpense> MoneyExpenses { get; set; }

        public Category() { }
        public Category(string name) => Name = name; 
    }
}
