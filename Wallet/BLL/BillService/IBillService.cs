using System;
using System.Collections.Generic;
using System.Text;
using DAL; 

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
    }
}
