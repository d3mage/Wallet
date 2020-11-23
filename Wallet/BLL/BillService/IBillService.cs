using DAL;
using System;
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

        public void GetMoneyInRange(Bill bill, DateTime startDate, DateTime endDate, out double profits, out double expenses);
        public void GetMoneyByDate(Bill bill, DateTime date, out double profits, out double expenses);
        public void GetMoneyByCategory(Bill bill, string name, out double profits, out double expenses);
    }
}
