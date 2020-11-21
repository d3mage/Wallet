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

        public bool isBillNameAvailable(string name)
        {
            List<Bill> data;
            try
            {
                data = readWriteService.ReadData();
            }
            catch (EmptyListException e)
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
            List<Bill> data = readWriteService.ReadData();
            foreach (var d in data)
            {
                if (d.Name.ToLower().Equals(name)) return d;
            }
            throw new BillNameInvalidException();
        }

        public Bill CreateNewBill(string name, double money)
        {
            return new Bill(name, money);
        }

        public void AddBill(Bill bill)
        {
            List<Bill> data = new List<Bill>();
            try
            {
                data = readWriteService.ReadData();
            }
            catch (Exception e) { }
            finally
            {
                data.Add(bill);
                readWriteService.WriteData(data);
            }

        }

        public void DeleteBill(Bill bill)
        {
            List<Bill> data = readWriteService.ReadData();
            data.Remove(bill);
            readWriteService.WriteData(data);
        }


        public void ChangeBillInfo(string oldName, string newName)
        {
            List<Bill> data = readWriteService.ReadData();
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
            List<Bill> data = readWriteService.ReadData();
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
        public void PrintBills()
        {
            List<Bill> data = readWriteService.ReadData();
            foreach (var d in data)
            {
                Console.WriteLine(d.Name);
            }
        }

        public void ChangeBillMoney(Bill bill, MoneyEvent moneyEvent)
        {
            if (moneyEvent.isExpense != true)
            {
                Double tempMoney = bill.Money;
                tempMoney += moneyEvent.Value;
                bill.Money = tempMoney;
            }
            else
            {
                if (bill.Money < moneyEvent.Value)
                    throw new InsufficientFundsException();
                else
                {
                    Double tempMoney = bill.Money;
                    tempMoney -= moneyEvent.Value;
                    bill.Money = tempMoney;
                }
            }
        }
    }
}
