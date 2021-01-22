using DAL;
using System;
using System.Collections.Generic;

namespace BLL
{
    public class BillService : IBillService
    {
        private readonly IReadWriteService<Bill> readWriteService;

        public BillService(IReadWriteService<Bill> readWrite)
        {
            readWriteService = readWrite;
        }

        public void AddBill(string name)
        {
            bool isAvailable = IsBillNameAvailable(name);
            if (isAvailable == true)
            {
                Bill bill = CreateNewBill(name);
                List<Bill> data = readWriteService.ReadData();
                data.Add(bill);
                readWriteService.WriteData(data);
            }
            else throw new BillNameInvalidException();
        }
        public void DeleteBill(string name)
        {
            bool isAvailable = IsBillNameAvailable(name);
            if (isAvailable != true)
            {
                List<Bill> data = readWriteService.ReadData();
                Bill bill = GetBillByName(name);
                data.Remove(bill);
                readWriteService.WriteData(data);
            }
            else throw new BillNameInvalidException();
        }
        public void ChangeBillName(string name, string newName)
        {
            bool isAvailable = IsBillNameAvailable(name);
            if (isAvailable != true)
            {
                List<Bill> data = readWriteService.ReadData();
                foreach (var d in data)
                {
                    if (d.Name.ToLower().Equals(name))
                    {
                        d.Name = newName;
                    }
                }
                readWriteService.WriteData(data);
            }
            else throw new BillNameInvalidException();
        }
 
        private bool IsBillNameAvailable(string name)
        {
            Bill billToCheck = GetBillByName(name);
            if(billToCheck == null)
            {
                return true; 
            }
            return false; 
        }

        public Bill GetBillByName(string name)
        {
            List<Bill> data = readWriteService.ReadData();
            foreach (var d in data)
            {
                if (d.Name.Equals(name)) return d;
            }
            return null; 
        }

        private Bill CreateNewBill(string name)
        {
            return new Bill(name, 150);
        }

       
        public void UpdateBillInLIst(Bill bill)
        {
            List<Bill> data = readWriteService.ReadData();
            foreach (var d in data)
            {
                if (d.Name.ToLower().Equals(bill.Name))
                {
                    d.Money = bill.Money;
                    d.moneyEvents = bill.moneyEvents;
                }
            }
            readWriteService.WriteData(data);
        }
        public List<Bill> GetBills()
        {
            List<Bill> data = readWriteService.ReadData();
            if (data.Count > 0)
            {
                return data;
            }
            else throw new BillsNotInitializedException();
        }

        public List<string> GetBillsNames()
        {
            List<Bill> data = readWriteService.ReadData();
            List<string> toReturn = new List<string>(); 
            foreach (var d in data)
            {
                toReturn.Add(d.Name + " " + d.Money); 
            }
            return toReturn; 
        }

        public void TransferMoney(string fName, string sName, double value)
        {
            MoneyEvent expenseEvent = new MoneyEvent(true, $"Transfer to {sName}", "Transfer", value);
            MoneyEvent profitEvent = new MoneyEvent(false, $"Transfer from {fName}", "Transfer", value);

            Bill fBill = GetBillByName(fName);
            Bill sBill = GetBillByName(sName);

            ChangeBillMoney(fBill, expenseEvent);
            ChangeBillMoney(sBill, profitEvent);

            UpdateBillInLIst(fBill);
            UpdateBillInLIst(sBill);
        }

        public void ChangeBillMoney(Bill bill, MoneyEvent moneyEvent)
        {
            if(bill == null)
            {
                throw new BillNameInvalidException();
            }
            if (moneyEvent.isExpense != true)
            {
                double tempMoney = bill.Money;
                tempMoney += moneyEvent.value;
                bill.Money = tempMoney;
            }
            else
            {
                if (bill.Money < moneyEvent.value)
                    throw new InsufficientFundsException();
                else
                {
                    double tempMoney = bill.Money;
                    tempMoney -= moneyEvent.value;
                    bill.Money = tempMoney;
                }
            }
            UpdateBillInLIst(bill);
        }

        public void ChangeCategories(string name, List<MoneyEvent> moneyEvents)
        {
            Bill bill = GetBillByName(name);
            bill.moneyEvents = moneyEvents;
            UpdateBillInLIst(bill);
        }

        public void GetMoneyInRange(Bill bill, DateTime startDate, DateTime endDate, out double profits, out double expenses)
        {
            double tempProfits = 0, tempExpenses = 0;

            foreach (var m in bill.moneyEvents)
            {
                if (DateTime.Compare(startDate, m.Date) < 0 && DateTime.Compare(endDate, m.Date) > 0)
                {
                    if (m.isExpense == false)
                    {
                        tempProfits += m.value;
                    }
                    else
                    {
                        tempExpenses -= m.value;
                    }
                    Console.WriteLine(m.ToString());
                }
                else if (DateTime.Compare(endDate, m.Date) <= 0) break;
            }
            profits = tempProfits;
            expenses = tempExpenses; 
        }
        public void GetMoneyByDate(Bill bill, DateTime date, out double profits, out double expenses)
        {
            double tempProfits = 0, tempExpenses = 0;

            foreach (var m in bill.moneyEvents)
            {
                if (DateTime.Compare(date, m.Date) == 0)
                {
                    if (m.isExpense == false)
                    {
                        tempProfits += m.value;
                    }
                    else
                    {
                        tempExpenses -= m.value;
                    }
                    Console.WriteLine(m.ToString());
                }
                else if (DateTime.Compare(date, m.Date) <= 0) break;
            }
            profits = tempProfits;
            expenses = tempExpenses; 
        }

        //public void GetMoneyByCategory(Bill bill, string name,  out double profits, out double expenses)
        //{
        //    double tempProfits = 0, tempExpenses = 0;

        //    foreach (var c in bill.categories)
        //    {
        //        if(c.Name.Equals(name))
        //        {
        //            foreach(var m in c.moneyEvents)
        //            {
        //                if (m.isExpense == false)
        //                {
        //                    tempProfits += m.Value;
        //                }
        //                else
        //                {
        //                    tempExpenses -= m.Value;
        //                }
        //            }
        //        }    
        //    }

        //    profits = tempProfits;
        //    expenses = tempExpenses; 
        //}
    }
}
