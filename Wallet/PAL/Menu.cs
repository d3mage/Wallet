using BLL;
using System;
using System.Collections.Generic;

namespace PL
{
    public class Menu
    {
        IGetInputService inputService;

        IBillBusinessHandler businessHandler;
        ICategoryBusinessHandler categoryHandler;
        IMoneyEventHandler moneyHandler;

        IBillService billService;

        //public Menu(IGetInputService getInput, IBillBusinessHandler business, ICategoryBusinessHandler category, IMoneyEventHandler money)
        //{
        //    getInputService = getInput;
        //    businessHandler = business;
        //    categoryHandler = category;
        //    moneyHandler = money;
        //}

        public Menu(IGetInputService input, IBillService bill)
        {
            inputService = input; 
            billService = bill; 
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
                Console.WriteLine("Enter name of bill: ");
                string name = inputService.GetVerifiedInput(@"[A-Za-z]{0,20}");

                businessHandler.AddBill();
            }
            else if(func.Equals("profit"))
            {
                moneyHandler.AddNewEvent(false);
            }
            else if (func.Equals("expense"))
            {
                moneyHandler.AddNewEvent(true);
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
                businessHandler.DeleteBill();
            }
            else if(func.Equals("category"))
            {
                categoryHandler.DeleteCategory();
            }
            else if (func.Equals("event"))
            {
                moneyHandler.DeleteEvent();
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
                businessHandler.ChangeNameOfBill();
            }
            else if (func.Equals("category"))
            {
                categoryHandler.ChangeCategory();
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
                businessHandler.RangedSearch();
            }
            else if (func.Equals("day"))
            {
                businessHandler.SearchByDate();
            }
            else if (func.Equals("category"))
            {
                businessHandler.SearchByCategory();
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
                List<string> currentBills = billService.GetBillsToPrint();
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
