﻿namespace BLL
{
    public interface IBillBusinessHandler
    {
        public void AddBill();
        public void DeleteBill();
        public void ChangeNameOfBill();
        public int ShowCurrentAccounts();
    }
}
