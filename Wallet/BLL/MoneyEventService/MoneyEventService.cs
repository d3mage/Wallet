using DAL;
using System.Collections.Generic;

namespace BLL
{
    public class MoneyEventService : IMoneyEventService
    {
        public void AddMoneyEvent(Category category, MoneyEvent moneyEvent)
        {
            if (category.moneyEvents == null)
            {
                category.moneyEvents = new List<MoneyEvent>();
            }
            category.moneyEvents.Add(moneyEvent);
        }

        public MoneyEvent GetNewMoneyExpense(bool expense, string name, double money)
        {
            return new MoneyEvent(expense, name, money);
        }

        public MoneyEvent GetEventByName(Category category, string name)
        {
            foreach (var c in category.moneyEvents)
            {
                if (c.Description.Equals(name)) return c;
            }
            throw new MoneyEventNameInvalidException();
        }
        public void DeleteMoneyEvent(Category category, MoneyEvent moneyEvent)
        {
            category.moneyEvents.Remove(moneyEvent);
        }

    }
}
