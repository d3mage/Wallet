using DAL;

namespace BLL
{
    public interface IMoneyEventService
    {
        public void AddMoneyEvent(Category category, MoneyEvent moneyEvent);
        public MoneyEvent CreateNewMoneyExpense(bool expense, string name, double money);
        public MoneyEvent GetEventByName(Category category, string name);
        public void DeleteMoneyEvent(Category category, MoneyEvent moneyEvent);
    }
}
