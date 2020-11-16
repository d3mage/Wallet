using System;
using System.Collections.Generic;
using System.Text;
using DAL;

namespace BLL
{
    public class BillService
    {
        public bool isBillNameAvailable(IReadWriteService service, string name)
        {
            List<Bill> data = service.ReadData();
            foreach (var d in data)
            {
                if (d.Name.ToLower().Equals(name)) return false;
            }
            return true;
        }

        public Bill GetBillByName(IReadWriteService service, string name)
        {
            List<Bill> data = service.ReadData();
            foreach (var d in data) 
            {
                if (d.Name.ToLower().Equals(name)) return d; 
            }
            throw new ArgumentException("Bill name is invalid.");
        }

        public void AddBill(IReadWriteService service, Bill bill)
        {
            List<Bill> data = service.ReadData();
            data.Add(bill);
            service.WriteData(data);
        }
      
        public void DeleteBill(IReadWriteService service, Bill bill)
        {
            List<Bill> data = service.ReadData();
            data.Remove(bill);
            service.WriteData(data); 
        }

        
        public void ChangeBillInfo(IReadWriteService service, string oldName, string newName)
        {
            List<Bill> data = service.ReadData();
            foreach (var d in data)
            {
                if (d.Name.ToLower().Equals(oldName))
                {
                    d.Name = newName;
                }
            }
            service.WriteData(data);
        }

        public List<Bill> GetBills(IReadWriteService service)
        {
            List<Bill> data = service.ReadData();
            if (data.Count > 0)
            {
                return data;
            }
            else throw new BillsNotInitializedException(); 
        }
    }
}
