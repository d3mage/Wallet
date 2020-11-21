using DAL;
using System;
using System.Collections.Generic;

namespace BLL
{
    public class BillBusinessHandler : IBillBusinessHandler
    {
        private IGetInputService inputService;
        private IBillService billService;

        public BillBusinessHandler(IGetInputService input, IBillService bill)
        {
            inputService = input;
            billService = bill;
        }

        public void AddBill()
        {
            double money = 150;
            Console.WriteLine("Enter name of bill: ");
            try
            {
                string name = inputService.GetVerifiedInput(@"[A-Za-z]{0,20}");
                bool available = billService.isBillNameAvailable(name);
                if (available != true) throw new BillNameInvalidException();
                billService.AddBill(billService.CreateNewBill(name, money));
            }
            catch (Exception e) when (e is EmptyListException ||
            e is TooManyFalseAttemptsException || e is BillNameInvalidException)
            {
                Console.WriteLine(e.Message);
            }
        }
        public void DeleteBill()
        {
            Console.WriteLine("Enter the name of bill you want to delete: ");
            try
            {
                billService.PrintBills();
                string name = inputService.GetVerifiedInput(@"[A-Za-z]{0,20}");
                bool available = billService.isBillNameAvailable(name);
                if (available == true) throw new BillNameInvalidException();
                billService.DeleteBill(billService.GetBillByName(name));
            }
            catch (Exception e) when (e is EmptyListException ||
            e is TooManyFalseAttemptsException || e is BillNameInvalidException)
            {
                Console.WriteLine(e.Message);
            }
        }
        public void ChangeNameOfBill()
        {
            Console.WriteLine("Enter name of bill you want to change: ");
            try
            {
                billService.PrintBills();
                string oldName = inputService.GetVerifiedInput(@"[A-Za-z]{0,20}");
                bool available = billService.isBillNameAvailable(oldName);
                if (available == true) throw new BillNameInvalidException();

                Console.WriteLine("Enter new name: ");
                string newName = inputService.GetVerifiedInput(@"[A-Za-z]{0,20}");
                billService.ChangeBillInfo(oldName, newName);
            }
            catch (Exception e) when (e is EmptyListException ||
            e is TooManyFalseAttemptsException || e is BillNameInvalidException)
            {
                Console.WriteLine(e.Message);
            }
        }
        public int ShowCurrentAccounts()
        {
            try
            {
                List<Bill> bills = billService.GetBills();
                foreach (var b in bills)
                {
                    Console.WriteLine($"Name of bill: {b.Name}");
                    Console.WriteLine($"Money on bill: {b.Money}");
                }
                return 1;
            }
            catch (BillsNotInitializedException e)
            {
                Console.WriteLine(e.Message);
            }
            return -1;
        }


    }
}
