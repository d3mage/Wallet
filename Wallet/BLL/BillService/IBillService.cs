using DAL;
using System.Collections.Generic;

namespace BLL
{
    public interface IBillService
    {
        public bool isBillNameAvailable(string name);
        public Bill GetBillByName(string name);
        public Bill CreateNewBill(string name, double money);
        public void AddBill(Bill bill);
        public void DeleteBill(Bill bill);
        public void ChangeBillInfo(string oldName, string newName);
        public void ChangeBillInList(Bill bill);
        public List<Bill> GetBills();
        public void PrintBills();
        public void ChangeBillMoney(Bill bill, MoneyEvent moneyEvent);
    }
}
