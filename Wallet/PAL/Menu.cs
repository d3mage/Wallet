using BLL;
using System;
using System.Collections.Generic;

namespace PL
{
    public class Menu
    {
        IGetInputService inputService;

        IBillService billService;
        ICategoryService categoryService;
        IMoneyEventService moneyEventService; 

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
                Console.WriteLine(_menuEntry);
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
                catch (Exception e) when (/*e is EmptyListException ||*/
                 e is TooManyFalseAttemptsException || e is BillNameInvalidException)
                {
                    Console.WriteLine(e.Message);
                }
            }
            else if(func.Equals("profit"))
            {
            }
            else if (func.Equals("expense"))
            {
            }
            else if(func.Equals(""))
            {
                return; 
            }
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
                catch (Exception e) when (/*e is EmptyListException ||*/
                 e is TooManyFalseAttemptsException || e is BillNameInvalidException)
                {
                    Console.WriteLine(e.Message);
                }
            }
            else if(func.Equals("category"))
            {
            }
            else if (func.Equals("event"))
            {
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
                catch (Exception e) when (/*e is EmptyListException ||*/
                 e is TooManyFalseAttemptsException || e is BillNameInvalidException)
                {
                    Console.WriteLine(e.Message);
                }
            }
            else if (func.Equals("category"))
            {
            }
            else if (func.Equals("event"))
            {
               
            }
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
            catch (Exception e) when (/*e is EmptyListException ||*/
            e is TooManyFalseAttemptsException || e is BillNameInvalidException)
            {
                Console.WriteLine(e.Message);
            }
        }

        private string _menuEntry = "What do you want to do?\n\"Add\"\n\"Delete\"\n\"Change\" info\nGenerate data \"stats\"\nTransfer money between bills";
        private string addMenu = "What do you want to add?\nBill\nCategory\nProfit\nExpense";
        private string deleteMenu = "What do you want to delete?\nBill\nCategory\nProfit\nExpense";
        private string changeMenu = "What do you want to change?\nBill\nCategory\nProfit\nExpense";
        private string statsMenu = "What stats do you want to get?\nStats by date \"range\"\nStats by \"day\"\nStats by \"category\"";
    }
}
