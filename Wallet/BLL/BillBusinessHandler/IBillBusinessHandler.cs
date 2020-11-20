using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public interface IBillBusinessHandler
    {
        public void AddBill();
        public void DeleteBill();
        public void ChangeBill();
        public int ShowCurrentAccounts();
    }
}
