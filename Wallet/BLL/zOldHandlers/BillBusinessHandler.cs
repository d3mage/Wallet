//using DAL;
//using System;
//using System.Collections.Generic;
//using System.Globalization;

//namespace BLL
//{
//    public class BillBusinessHandler 
//    {
//        private IGetInputService inputService;
//        private IBillService billService;

//        public BillBusinessHandler(IGetInputService input, IBillService bill)
//        {
//            inputService = input;
//            billService = bill;
//        }

       
//        public int ShowCurrentAccounts()
//        {
//            try
//            {
//                List<Bill> bills = billService.GetBills();
//                foreach (var b in bills)
//                {
//                    Console.WriteLine($"Name of bill: {b.Name}");
//                    Console.WriteLine($"Money on bill: {b.Money}");
//                }
//                return 1;
//            }
//            catch (Exception e) when (e is EmptyListException || e is BillsNotInitializedException)
//            {
//                Console.WriteLine(e.Message);
//            }
//            return -1;
//        }


//        public void RangedSearch()
//        {
//            Console.WriteLine("Enter the bill you want to get ranged stats about:  ");
//            try
//            {
//                string name = inputService.GetVerifiedInput(@"[A-Za-z]{0,20}");
//                Bill bill = billService.GetBillByName(name);

//                Console.WriteLine("Range first date: [dd-mm-yyyy]");
//                string date = inputService.GetVerifiedInput(@"(0?[1-9]|[12][0-9]|3[01])-(0?[1-9]|1[0-2])-(\d{4})");
//                DateTime startDate = DateTime.ParseExact(date, "dd-M-yyyy", CultureInfo.InvariantCulture);

//                Console.WriteLine("Range second date: [dd-mm-yyyy]");
//                date = inputService.GetVerifiedInput(@"(0?[1-9]|[12][0-9]|3[01])-(0?[1-9]|1[0-2])-(\d{4})");
//                DateTime endDate = DateTime.ParseExact(date, "dd-M-yyyy", CultureInfo.InvariantCulture);

//                double profits, expenses;
//                billService.GetMoneyInRange(bill, startDate, endDate, out profits, out expenses);

//                Console.WriteLine($"Since {startDate} to {endDate}:\nYou've spent: {expenses}" +
//                    $"\nYou've earned: {profits}");
//            }
//            catch (Exception e) when (e is EmptyListException ||
//            e is TooManyFalseAttemptsException || e is BillNameInvalidException ||
//            e is CategoryNameInvalidException || e is MoneyEventNameInvalidException
//            || e is InsufficientFundsException)
//            {
//                Console.WriteLine(e.Message);
//            }
//        }

//        public void SearchByDate()
//        {
//            Console.WriteLine("Enter the bill you want to day stats about:  ");
//            try
//            {
//                string name = inputService.GetVerifiedInput(@"[A-Za-z]{0,20}");
//                Bill bill = billService.GetBillByName(name);

//                Console.WriteLine("Enter date: [dd-mm-yyyy]");
//                string date = inputService.GetVerifiedInput(@"(0?[1-9]|[12][0-9]|3[01])-(0?[1-9]|1[0-2])-(\d{4})");
//                DateTime startDate = DateTime.ParseExact(date, "dd-M-yyyy", CultureInfo.InvariantCulture);

//                double profits, expenses;
//                billService.GetMoneyByDate(bill, startDate, out profits, out expenses);

//                Console.WriteLine($"On {date}:\nYou've spent: {expenses}" +
//                    $"\nYou've earned: {profits}");
//            }
//            catch (Exception e) when (e is EmptyListException ||
//            e is TooManyFalseAttemptsException || e is BillNameInvalidException ||
//            e is CategoryNameInvalidException || e is MoneyEventNameInvalidException
//            || e is InsufficientFundsException)
//            {
//                Console.WriteLine(e.Message);
//            }
//        }
//        public void SearchByCategory()
//        {
//            Console.WriteLine("Enter the bill you want to day stats about:  ");
//            try
//            {
//                string name = inputService.GetVerifiedInput(@"[A-Za-z]{0,20}");
//                Bill bill = billService.GetBillByName(name);

//                Console.WriteLine("Enter category: ");
//                string categoryName = inputService.GetVerifiedInput(@"[A-Za-z]{0,20}");

//                double profits, expenses;
//                billService.GetMoneyByCategory(bill, categoryName, out profits, out expenses); 

//                Console.WriteLine($"Category {categoryName}:\nYou've spent: {expenses}" +
//                    $"\nYou've earned: {profits}");
//            }
//            catch (Exception e) when (e is EmptyListException ||
//            e is TooManyFalseAttemptsException || e is BillNameInvalidException ||
//            e is CategoryNameInvalidException || e is MoneyEventNameInvalidException
//            || e is InsufficientFundsException)
//            {
//                Console.WriteLine(e.Message);
//            }
//        }
//    }
//}
