using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public interface IBusinessHandler
    {
        public void AddBill();
        public void DeleteBill();
        public void ChangeBill();
        public int ShowCurrentAccounts();
    }
}
