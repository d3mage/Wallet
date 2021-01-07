using DAL;
using System;
using System.Collections.Generic;

namespace BLL
{
    public class BillService : IBillService
    {
        public IReadWriteService readWriteService;
        public BillService(IReadWriteService readWrite)
        {
            readWriteService = readWrite;
        }

        private List<Bill> ReadList()
        {
            List<Bill> data; 
            try
            {
                data = readWriteService.ReadData(); 
            }
            catch (EmptyListException e)
            {
                data = new List<Bill>(); 
            }
            return data; 
        }

        public bool isBillNameAvailable(string name)
        {
            List<Bill> data = ReadList();

           if(data.Count == 0)
            {
                return true; 
            }
            foreach (var d in data)
            {
                if (d.Name.ToLower().Equals(name)) return false;
            }
            return true;
        }


        public Bill GetBillByName(string name)
        {
            List<Bill> data = ReadList();
            foreach (var d in data)
            {
                if (d.Name.Equals(name)) return d;
            }
            throw new BillNameInvalidException();
        }

        public Bill CreateNewBill(string name, double money)
        {
            return new Bill(name, money);
        }

        public void AddBill(Bill bill)
        {
            List<Bill> data = ReadList();
            data.Add(bill);
            readWriteService.WriteData(data);
        }

        public void DeleteBill(Bill bill)
        {
            List<Bill> data = ReadList(); 
            data.Remove(bill);
            readWriteService.WriteData(data);
        }


        public void ChangeBillInfo(string oldName, string newName)
        {
            List<Bill> data = ReadList();
            foreach (var d in data)
            {
                if (d.Name.ToLower().Equals(oldName))
                {
                    d.Name = newName;
                }
            }
            readWriteService.WriteData(data);
        }
        public void ChangeBillInList(Bill bill)
        {
            List<Bill> data = ReadList();
            foreach (var d in data)
            {
                if (d.Name.ToLower().Equals(bill.Name))
                {
                    d.Money = bill.Money;
                    d.categories = bill.categories;
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

        public List<string> GetBillsToPrint()
        {
            List<Bill> data = readWriteService.ReadData();
            List<string> toReturn = new List<string>(); 
            foreach (var d in data)
            {
                toReturn.Add(d.Name); 
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
        }

        private void ChangeBillMoney(Bill bill, MoneyEvent moneyEvent)
        {
            if (moneyEvent.isExpense != true)
            {
                double tempMoney = bill.Money;
                tempMoney += moneyEvent.Value;
                bill.Money = tempMoney;
            }
            else
            {
                if (bill.Money < moneyEvent.Value)
                    throw new InsufficientFundsException();
                else
                {
                    double tempMoney = bill.Money;
                    tempMoney -= moneyEvent.Value;
                    bill.Money = tempMoney;
                }
            }
        }

        public void GetMoneyInRange(Bill bill, DateTime startDate, DateTime endDate, out double profits, out double expenses)
        {
            double tempProfits = 0, tempExpenses = 0;

            foreach (var c in bill.categories)
            {
                foreach (var m in c.moneyEvents)
                {
                    if (DateTime.Compare(startDate, m.Date) < 0 && DateTime.Compare(endDate, m.Date) > 0)
                    {
                        if (m.isExpense == false)
                        {
                            tempProfits += m.Value;
                        }
                        else
                        {
                            tempExpenses -= m.Value;
                        }
                        Console.WriteLine(m.ToString());
                    }
                    else if (DateTime.Compare(endDate, m.Date) <= 0) break;
                }
            }
            profits = tempProfits;
            expenses = tempExpenses; 
        }
        
        public void GetMoneyByDate(Bill bill, DateTime date, out double profits, out double expenses)
        {
            double tempProfits = 0, tempExpenses = 0;

            foreach (var c in bill.categories)
            {
                foreach (var m in c.moneyEvents)
                {
                    if (DateTime.Compare(date, m.Date) == 0)
                    {
                        if (m.isExpense == false)
                        {
                            tempProfits += m.Value;
                        }
                        else
                        {
                            tempExpenses -= m.Value;
                        }
                        Console.WriteLine(m.ToString());
                    }
                    else if (DateTime.Compare(date, m.Date) <= 0) break;
                }
            }
            profits = tempProfits;
            expenses = tempExpenses; 
        }

        public void GetMoneyByCategory(Bill bill, string name,  out double profits, out double expenses)
        {
            double tempProfits = 0, tempExpenses = 0;

            foreach (var c in bill.categories)
            {
                if(c.Name.Equals(name))
                {
                    foreach(var m in c.moneyEvents)
                    {
                        if (m.isExpense == false)
                        {
                            tempProfits += m.Value;
                        }
                        else
                        {
                            tempExpenses -= m.Value;
                        }
                    }
                }    
            }

            profits = tempProfits;
            expenses = tempExpenses; 
        }
    }
}
