using BLL;
using System;
using System.Collections.Generic;

namespace PL
{
    public class Menu
    {
        readonly IGetInputService inputService;
        readonly IBillService billService;
        readonly ICategoryService categoryService;
        readonly IMoneyEventService moneyEventService; 

        public Menu(IGetInputService input, IBillService bill, ICategoryService category, IMoneyEventService money)
        {
            inputService = input; 
            billService = bill;
            categoryService = category;
            moneyEventService = money;
        }

        public int Print()
        {
            string func = "";

            while (!func.Equals("exit"))
            {
                Console.WriteLine(menuEntry);
                try
                {
                    func = inputService.GetVerifiedInput(@"[A-Za-z]{3,10}");
                }
                catch (TooManyFalseAttemptsException e)
                {
                    Console.WriteLine(e.Message);
                }
                if (func.Equals("add"))
                {
                    AddMenu(); 
                }
                else if (func.Equals("delete"))
                {
                    DeleteMenu(); 
                }
                else if (func.Equals("change"))
                {
                    ChangeMenu(); 
                }
                else if (func.Equals("stats"))
                {
                    StatsMenu(); 
                }
                else if(func.Equals("transfer"))
                {
                    TransferMenu(); 
                }
            }
            return 1;
        }

        private void AddMenu()
        {
            string func = ""; 
            Console.WriteLine(addMenu);
            try
            {
                func = inputService.GetVerifiedInput(@"[A-Za-z]{3,10}");
            }
            catch (TooManyFalseAttemptsException e)
            {
                Console.WriteLine(e.Message);
            }
            if(func.Equals("bill"))
            {
                try
                {
                    Console.WriteLine("Enter name of bill: ");
                    string name = inputService.GetVerifiedInput(@"[A-Za-z]{0,20}");

                    billService.AddBill(name);
                }
                catch (Exception e) when (e is TooManyFalseAttemptsException
                || e is BillNameInvalidException)
                {
                    Console.WriteLine(e.Message);
                }
            }
            else if(func.Equals("profit"))
            {
               AddMoneyEvent("profit", false);
            }
            else if (func.Equals("expense"))
            {
                AddMoneyEvent("expense", true); 
            }
            else if(func.Equals(""))
            {
                return; 
            }
        }
        private void AddMoneyEvent(string eventName, bool isExpense)
        {
            Printer.Print(billService.GetBillsNames());

            Console.WriteLine("Enter name of bill: ");
            string billName = inputService.GetVerifiedInput(@"[A-Za-z]{0,20}");

            Console.WriteLine("Enter name of {0}: ", eventName);
            string name = inputService.GetVerifiedInput(@"[A-Za-z]{0,20}");

            Console.WriteLine("Enter category: ");
            string category = inputService.GetVerifiedInput(@"[A-Za-z]{0,20}");

            Console.WriteLine("Enter ammount of money: ");
            double money = Convert.ToDouble(inputService.GetVerifiedInput(@"^([1-9]{1}[0-9]{0,2}(\,[0-9]{3})*(\.[0-9]{0,2})?
                   |[1-9]{1}[0-9]{0,}(\.[0-9]{0,2})?
                   |0(\.[0-9]{0,2})?|(\.[0-9]{1,2})?)"));

            moneyEventService.AddMoneyEvent(categoryService, billName, isExpense, name, category, money);

        }

        private void DeleteMenu()
        {
            string func = "";
            Console.WriteLine(deleteMenu);
            try
            {
                func = inputService.GetVerifiedInput(@"[A-Za-z]{3,10}");
            }
            catch (TooManyFalseAttemptsException e)
            {
                Console.WriteLine(e.Message);
            }
            if (func.Equals("bill"))
            {
                try
                {
                    Printer.Print(billService.GetBillsNames());

                    Console.WriteLine("Enter name of bill: ");
                    string name = inputService.GetVerifiedInput(@"[A-Za-z]{0,20}");

                    billService.DeleteBill(name);
                }
                catch (Exception e) when (e is TooManyFalseAttemptsException 
                || e is BillNameInvalidException)
                {
                    Console.WriteLine(e.Message);
                }
            }
            else if(func.Equals("category"))
            {
                Printer.Print(categoryService.GetCategories());

                Console.WriteLine("Enter name of category: ");
                string name = inputService.GetVerifiedInput(@"[A-Za-z]{0,20}");

                categoryService.DeleteCategory(name);
            }
            else if (func.Equals("event"))
            {
                Printer.Print(billService.GetBillsNames());

                Console.WriteLine("Enter name of bill: ");
                string billName = inputService.GetVerifiedInput(@"[A-Za-z]{0,20}");

                Printer.Print(moneyEventService.GetEventNames(billName));
                Console.WriteLine("Enter name of event: ");
                string name = inputService.GetVerifiedInput(@"[A-Za-z]{0,20}");

                moneyEventService.DeleteMoneyEvent(billName, name);
            }
            else if (func.Equals(""))
            {
                return;
            }
        }
        private void ChangeMenu()
        {
            string func = "";
            Console.WriteLine(changeMenu);
            try
            {
                func = inputService.GetVerifiedInput(@"[A-Za-z]{3,10}");
            }
            catch (TooManyFalseAttemptsException e)
            {
                Console.WriteLine(e.Message);
            }
            if (func.Equals("bill"))
            {
                try
                {
                    Printer.Print(billService.GetBillsNames());

                    Console.WriteLine("Enter name of bill you want to change: : ");
                    string oldName = inputService.GetVerifiedInput(@"[A-Za-z]{0,20}");

                    Console.WriteLine("Enter new name: ");
                    string newName = inputService.GetVerifiedInput(@"[A-Za-z]{0,20}");

                    billService.ChangeBillName(oldName, newName);
                }
                catch (Exception e) when (e is TooManyFalseAttemptsException
                || e is BillNameInvalidException)
                {
                    Console.WriteLine(e.Message);
                }
            }
            else if (func.Equals("category"))
            {
                Printer.Print(categoryService.GetCategories());

                Console.WriteLine("Enter name of category you want to change: : ");
                string oldName = inputService.GetVerifiedInput(@"[A-Za-z]{0,20}");

                Console.WriteLine("Enter new name: ");
                string newName = inputService.GetVerifiedInput(@"[A-Za-z]{0,20}");

                categoryService.ChangeCategory(billService, oldName, newName);
            }
            //else if (func.Equals("event"))
            //{
            //    Printer.Print(billService.GetBillsNames());

            //    Console.WriteLine("Enter name of bill: ");
            //    string billName = inputService.GetVerifiedInput(@"[A-Za-z]{0,20}");

            //    Printer.Print(moneyEventService.GetEventNames(billName));
            //}
            else if (func.Equals(""))
            {
                return;
            }
        }
        private void StatsMenu()
        {
            string func = "";
            Console.WriteLine(statsMenu);
            try
            {
                func = inputService.GetVerifiedInput(@"[A-Za-z]{3,10}");
            }
            catch (TooManyFalseAttemptsException e)
            {
                Console.WriteLine(e.Message);
            }
            if (func.Equals("range"))
            {
            }
            else if (func.Equals("day"))
            {
            }
            else if (func.Equals("category"))
            {
            }
            else if (func.Equals(""))
            {
                return;
            }
        }
        private void TransferMenu()
        {
            try
            {
                List<string> currentBills = billService.GetBillsNames();
                Printer.Print(currentBills);

                Console.WriteLine("Enter name of bill you want to transfer money from: ");
                string firstBillName = inputService.GetVerifiedInput(@"[A-Za-z]{0,20}");

                Console.WriteLine("Enter name of bill you want to transfer money to: ");
                string secondBillName = inputService.GetVerifiedInput(@"[A-Za-z]{0,20}");

                Console.WriteLine("Enter ammount of money you want to transfer: ");
                double ammount = Convert.ToDouble(inputService.GetVerifiedInput(@"[0-9]+"));

                billService.TransferMoney(firstBillName, secondBillName, ammount);
            }
            catch (Exception e) when (e is TooManyFalseAttemptsException
            || e is BillNameInvalidException || e is InsufficientFundsException)
            {
                Console.WriteLine(e.Message);
            }
        }

        private string menuEntry = "\nWhat do you want to do?\n\"Add\"\n\"Delete\"\n\"Change\" info\nGenerate data \"stats\"\n\"Transfer\" money between bills\n";
        private string addMenu = "\nWhat do you want to add?\nBill\nProfit\nExpense\n";
        private string deleteMenu = "\nWhat do you want to delete?\nBill\nCategory\nEvent\n";
        private string changeMenu = "\nWhat do you want to change?\nBill\nCategory\nProfit\nExpense\n";
        private string statsMenu = "\nWhat stats do you want to get?\nStats by date \"range\"\nStats by \"day\"\nStats by \"category\"\n";
    }
}
