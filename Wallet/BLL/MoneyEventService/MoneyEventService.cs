using DAL;
using System.Collections.Generic;

namespace BLL
{
    public class MoneyEventService : IMoneyEventService
    {
        private readonly IBillService billService; 

        public MoneyEventService(IBillService service)
        {
            billService = service; 
        }

        public void AddMoneyEvent(ICategoryService categoryService, string billName, bool expense, string name, string category, double money)
        {
            List<MoneyEvent> moneyEvents = GetMoneyEvents(billName); 
            
            MoneyEvent moneyEvent = CreateNewMoneyEvent(expense, name, category, money);
            moneyEvents.Add(moneyEvent);

            billService.ChangeBillMoney(billService.GetBillByName(billName), moneyEvent);
            billService.ChangeCategories(billName, moneyEvents);

            categoryService.AddCategory(category); 
        }

        public void DeleteMoneyEvent(string billName, string moneyEventName)
        {
            List<MoneyEvent> moneyEvents = GetMoneyEvents(billName); 

            foreach (var c in moneyEvents)
            {
                if (c.name.Equals(moneyEventName))
                {
                    moneyEvents.Remove(c);
                }
            }
        }

        public void ChangeMoneyEvent(string billName, string moneyEvent)
        {

        }

        public MoneyEvent CreateNewMoneyEvent(bool expense, string name, string category, double money)
        {
            return new MoneyEvent(expense, name, category, money);
        }

        public List<MoneyEvent> GetEventsByName(string billName, string moneyEventName)
        {
            List<MoneyEvent> moneyEvents = GetMoneyEvents(billName); 

            foreach (var c in moneyEvents)
            {
                if (c.name.Equals(moneyEventName))
                {
                    moneyEvents.Add(c);
                }
            }
            return moneyEvents; 
        }

        public List<string> GetEventNames(string billName)
        {
            List<MoneyEvent> moneyEvents = GetMoneyEvents(billName);
            List<string> names = new List<string>(); 

            foreach(var m in moneyEvents)
            {
                names.Add(m.ToString()+"\n");
            }
            return names; 
        }

        private List<MoneyEvent> GetMoneyEvents(string billName)
        {
            Bill bill = billService.GetBillByName(billName);
            if (bill == null)
            {
                throw new BillNameInvalidException();
            }
            List<MoneyEvent> moneyEvents = bill.moneyEvents; 
            if(moneyEvents == null)
            {
                moneyEvents = new List<MoneyEvent>(); 
            }
            return moneyEvents; 
        }

    }
}
