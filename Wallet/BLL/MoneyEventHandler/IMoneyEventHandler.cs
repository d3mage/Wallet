namespace BLL
{
    public interface IMoneyEventHandler
    {
        public void AddNewEvent(bool isExpense);
        public void DeleteEvent();
    }
}
