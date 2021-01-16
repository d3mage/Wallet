using DAL;
using System;
using System.Collections.Generic;

namespace BLL
{
    public interface IBillService
    {
        public void AddBill(string name);
        public void DeleteBill(string name);
        public void ChangeBillName(string name, string newName);
        public void ChangeBillInList(Bill bill);

        public Bill GetBillByName(string name);
        public List<Bill> GetBills();
        public List<string> GetBillsNames();

        public void ChangeBillMoney(Bill bill, MoneyEvent moneyEvent)
        public void TransferMoney(string fBill, string sBill, double ammount);

        public void GetMoneyInRange(Bill bill, DateTime startDate, DateTime endDate, out double profits, out double expenses);
        public void GetMoneyByDate(Bill bill, DateTime date, out double profits, out double expenses);
        //public void GetMoneyByCategory(Bill bill, string name, out double profits, out double expenses);
    }
}
