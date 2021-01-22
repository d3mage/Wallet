using DAL;
using System.Collections.Generic;

namespace BLL
{
    public interface IMoneyEventService
    {
        public void AddMoneyEvent(ICategoryService categoryService, string billName, bool expense, string name, string category, double money);
        public void DeleteMoneyEvent(string billName, string moneyEventName);

        public MoneyEvent CreateNewMoneyEvent(bool expense, string name, string category, double money);

        public List<MoneyEvent> GetEventsByName(string billName, string moneyEventName);
        public List<string> GetEventNames(string billName);
    }
}
