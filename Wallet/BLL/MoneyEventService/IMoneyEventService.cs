using DAL;
using System.Collections.Generic;

namespace BLL
{
    public interface IMoneyEventService
    {
        public void AddMoneyEvent(IBillService billService, string billName, bool expense, string name, string category, double money);
        public void DeleteMoneyEvent(IBillService billService, string billName, string moneyEventName);

        public MoneyEvent CreateNewMoneyEvent(bool expense, string name, string category, double money);
        public List<MoneyEvent> GetEventsByName(IBillService billService, string billName, string moneyEventName);
    }
}
