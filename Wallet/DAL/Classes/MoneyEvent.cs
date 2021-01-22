using System;

namespace DAL
{
    [Serializable]
    public class MoneyEvent
    {
        public bool isExpense { get; set; }
        public string name { get; set; }
        public double value { get; set; }
        public string category { get; set; }
        public DateTime Date { get; set; }

        public MoneyEvent() { }
        public MoneyEvent(bool expense, string description, string category, double value)
        {
            isExpense = expense;
            name = description;
            this.value = value;
            this.category = category;
            Date = DateTime.Today;
        }

        public override string ToString()
        {
            return $"Date: {Date}\nEvent: {name}\nValue: {value}\nCategory: {category}";
        }
    }


}
