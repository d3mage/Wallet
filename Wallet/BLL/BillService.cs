using System;
using System.Collections.Generic;
using System.Text;
using DAL;

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
            List<Bill> data;
            try
            {
                data = readWriteService.ReadData();
            }
            catch (EmptyListException e)
            {
                data = new List<Bill>(); 
            }
            data.Add(bill);
            readWriteService.WriteData(data);
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

        public List<Bill> GetBills()
        {
            List<Bill> data = readWriteService.ReadData();
            if (data.Count > 0)
            {
                return data;
            }
            else throw new BillsNotInitializedException(); 
        }
    }
}
