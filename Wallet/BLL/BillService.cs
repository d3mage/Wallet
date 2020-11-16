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
                if (d.Name.Equals(name)) return false;
            }
            return true;
        }

        public Bill GetBillByName(IReadWriteService service, string name)
        {
            List<Bill> data = service.ReadData();
            foreach (var d in data) 
            {
                if (d.Name.Equals(name)) return d; 
            }
            throw new ArgumentException("Bill name is invalid.");
        }

        public void AddBill(IReadWriteService service, Bill bill)
        {
            List<Bill> data = service.ReadData();
            data.Add(bill);
            service.WriteData(data);
        }
      
        public void Delete(IReadWriteService service, string name)
        {
            List<Bill> data = service.ReadData();
            foreach(var d in data)
            {
                if(d.Name.Equals(name))
                {
                    data.Remove(d); 
                }
            }
            service.WriteData(data); 
        }

        
        public void ChangeInfo()
        {

        }

        public void ShowAll()
        {

        }
    }
}
