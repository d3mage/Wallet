using System;

namespace DAL
{
    public class MoneyEvent
    {
        public bool isExpense { get; set; }
        public string Description { get; set; }
        public double Value { get; set; }
        public DateTime Date { get; set; }

        public MoneyEvent() { }
        public MoneyEvent(bool expense, string description, double value)
        {
            isExpense = expense;
            Description = description;
            Value = value;
            Date = DateTime.Now;
        }
    }


}
