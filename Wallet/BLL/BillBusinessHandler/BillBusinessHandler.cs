using DAL;
using System;
using System.Collections.Generic;
using System.Globalization;

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
                Bill bill = billService.GetBillByName(name);
                billService.DeleteBill(bill);
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
            catch (Exception e) when (e is EmptyListException || e is BillsNotInitializedException)
            {
                Console.WriteLine(e.Message);
            }
            return -1;
        }

        public void TransferMoney()
        {
            Console.WriteLine("Enter name of bill you want to transfer money from: ");
            try
            {
                billService.PrintBills();
                string firstBillName = inputService.GetVerifiedInput(@"[A-Za-z]{0,20}");
                Bill transferFrom = billService.GetBillByName(firstBillName);

                Console.WriteLine("Enter name of bill you want to transfer money to: ");
                string secondBillName = inputService.GetVerifiedInput(@"[A-Za-z]{0,20}");
                Bill transferTo = billService.GetBillByName(secondBillName);

                Console.WriteLine("Enter ammount of money you want to transfer: ");
                double ammount = Convert.ToDouble(inputService.GetVerifiedInput(@"[0-9]+"));

                billService.TransferMoney(transferFrom, new MoneyEvent(true, "transfer money", ammount));
                billService.TransferMoney(transferTo, new MoneyEvent(false, "transfer money", ammount));

            }
            catch (Exception e) when (e is EmptyListException ||
            e is TooManyFalseAttemptsException || e is BillNameInvalidException)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void RangedSearch()
        {
            Console.WriteLine("Enter the bill you want to get ranged stats about:  ");
            try
            {
                billService.PrintBills();
                string name = inputService.GetVerifiedInput(@"[A-Za-z]{0,20}");
                Bill bill = billService.GetBillByName(name);

                Console.WriteLine("Range first date: [dd-mm-yyyy]");
                string date = inputService.GetVerifiedInput(@"(0?[1-9]|[12][0-9]|3[01])-(0?[1-9]|1[0-2])-(\d{4})");
                DateTime startDate = DateTime.ParseExact(date, "dd-M-yyyy", CultureInfo.InvariantCulture);

                Console.WriteLine("Range second date: [dd-mm-yyyy]");
                date = inputService.GetVerifiedInput(@"(0?[1-9]|[12][0-9]|3[01])-(0?[1-9]|1[0-2])-(\d{4})");
                DateTime endDate = DateTime.ParseExact(date, "dd-M-yyyy", CultureInfo.InvariantCulture);

                double profits, expenses;
                billService.GetMoneyInRange(bill, startDate, endDate, out profits, out expenses);

                Console.WriteLine($"Since {startDate} to {endDate}:\nYou've spent: {expenses}" +
                    $"\nYou've earned: {profits}");
            }
            catch (Exception e) when (e is EmptyListException ||
            e is TooManyFalseAttemptsException || e is BillNameInvalidException ||
            e is CategoryNameInvalidException || e is MoneyEventNameInvalidException
            || e is InsufficientFundsException)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void SearchByDate()
        {
            Console.WriteLine("Enter the bill you want to day stats about:  ");
            try
            {
                billService.PrintBills();
                string name = inputService.GetVerifiedInput(@"[A-Za-z]{0,20}");
                Bill bill = billService.GetBillByName(name);

                Console.WriteLine("Enter date: [dd-mm-yyyy]");
                string date = inputService.GetVerifiedInput(@"(0?[1-9]|[12][0-9]|3[01])-(0?[1-9]|1[0-2])-(\d{4})");
                DateTime startDate = DateTime.ParseExact(date, "dd-M-yyyy", CultureInfo.InvariantCulture);

                double profits, expenses;
                billService.GetMoneyByDate(bill, startDate, out profits, out expenses);

                Console.WriteLine($"On {date}:\nYou've spent: {expenses}" +
                    $"\nYou've earned: {profits}");
            }
            catch (Exception e) when (e is EmptyListException ||
            e is TooManyFalseAttemptsException || e is BillNameInvalidException ||
            e is CategoryNameInvalidException || e is MoneyEventNameInvalidException
            || e is InsufficientFundsException)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void SearchByCategory()
        {
            Console.WriteLine("Enter the bill you want to day stats about:  ");
            try
            {
                billService.PrintBills();
                string name = inputService.GetVerifiedInput(@"[A-Za-z]{0,20}");
                Bill bill = billService.GetBillByName(name);

                Console.WriteLine("Enter category: ");
                string categoryName = inputService.GetVerifiedInput(@"[A-Za-z]{0,20}");

                double profits, expenses;
                billService.GetMoneyByCategory(bill, categoryName, out profits, out expenses); 

                Console.WriteLine($"Category {categoryName}:\nYou've spent: {expenses}" +
                    $"\nYou've earned: {profits}");
            }
            catch (Exception e) when (e is EmptyListException ||
            e is TooManyFalseAttemptsException || e is BillNameInvalidException ||
            e is CategoryNameInvalidException || e is MoneyEventNameInvalidException
            || e is InsufficientFundsException)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
