using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    [Serializable]
    public abstract class MoneyEvent
    {
        public string Description { get; set; }
        public double Value { get; set; }
        public DateTime Date { get; set; }

        public MoneyEvent() { }
        public MoneyEvent(string description, double value)
        {
            Description = description;
            Value = value;
            Date = DateTime.Now;
        }
    }

    [Serializable]
    public class MoneyProfit : MoneyEvent
    {
        public MoneyProfit() : base () { }
        public MoneyProfit(string description, double value) : base(description, value) { }
    }
    [Serializable]
    public class MoneyExpense : MoneyEvent
    {
        public MoneyExpense() : base () { }
        public MoneyExpense(string description, double value) : base(description, value) { }
    }
}
