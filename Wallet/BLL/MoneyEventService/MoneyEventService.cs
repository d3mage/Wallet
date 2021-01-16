using DAL;
using System.Collections.Generic;

namespace BLL
{
    public class MoneyEventService : IMoneyEventService
    {
        public void AddMoneyEvent(IBillService billService, string billName, bool expense, string name, string category, double money)
        {
            Bill bill = billService.GetBillByName(billName);
            if (bill == null)
            {
                throw new BillNameInvalidException();
            }
            if (bill.moneyEvents == null)
            {
                bill.moneyEvents = new List<MoneyEvent>();
            }
            MoneyEvent moneyEvent = CreateNewMoneyEvent(expense, name, category, money);
            bill.moneyEvents.Add(moneyEvent);
        }
        public void DeleteMoneyEvent(IBillService billService, string billName, string moneyEventName)
        {
            Bill bill = billService.GetBillByName(billName);
            if (bill == null)
            {
                throw new BillNameInvalidException();
            }
            foreach (var c in bill.moneyEvents)
            {
                if (c.Description.Equals(moneyEventName))
                {
                    bill.moneyEvents.Remove(c);
                }
            }
        }

        public MoneyEvent CreateNewMoneyEvent(bool expense, string name, string category, double money)
        {
            return new MoneyEvent(expense, name, category, money);
        }

        public List<MoneyEvent> GetEventsByName(IBillService billService, string billName, string moneyEventName)
        {
            Bill bill = billService.GetBillByName(billName);
            if (bill == null)
            {
                throw new BillNameInvalidException();
            }
            List<MoneyEvent> moneyEvents = new List<MoneyEvent>(); 
            foreach (var c in bill.moneyEvents)
            {
                if (c.Description.Equals(moneyEventName))
                {
                    moneyEvents.Add(c);
                }
            }
            return moneyEvents; 
        }
        

    }
}
