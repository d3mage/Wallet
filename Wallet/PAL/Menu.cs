﻿using BLL;
using System;

namespace PL
{
    public class Menu
    {
        IGetInputService getInputService;
        IBillBusinessHandler businessHandler;
        ICategoryBusinessHandler categoryHandler;
        IMoneyEventHandler moneyHandler;

        public Menu(IGetInputService getInput, IBillBusinessHandler business, ICategoryBusinessHandler category, IMoneyEventHandler money)
        {
            getInputService = getInput;
            businessHandler = business;
            categoryHandler = category;
            moneyHandler = money;
        }

        public int Print()
        {
            string func = "";

            while (!func.Equals("exit"))
            {
                Console.WriteLine(_menuEntry);
                try
                {
                    func = getInputService.GetVerifiedInput(@"[A-Za-z]{3,10}");
                }
                catch (TooManyFalseAttemptsException e)
                {
                    Console.WriteLine(e.Message);
                }
                if (func.Equals("add"))
                {
                    Console.WriteLine(addMenu);
                    try
                    {
                        func = getInputService.GetVerifiedInput(@"[A-Za-z]{3,10}");
                    }
                    catch (TooManyFalseAttemptsException e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    switch (func)
                    {
                        case "bill":
                            businessHandler.AddBill();
                            break;
                        case "category":
                            categoryHandler.AddCategory();
                            break;
                        case "profit":
                            moneyHandler.AddNewEvent(false);
                            break;
                        case "expense":
                            moneyHandler.AddNewEvent(true);
                            break;
                    }
                }
                else if (func.Equals("delete"))
                {
                    Console.WriteLine(deleteMenu);
                    try
                    {
                        func = getInputService.GetVerifiedInput(@"[A-Za-z]{3,10}");
                    }
                    catch (TooManyFalseAttemptsException e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    switch (func)
                    {
                        case "bill":
                            businessHandler.DeleteBill();
                            break;
                        case "category":
                            categoryHandler.DeleteCategory();
                            break;
                        case "event":
                            moneyHandler.DeleteEvent();
                            break;
                    }
                }
                else if (func.Equals("change"))
                {
                    Console.WriteLine(changeMenu);
                    try
                    {
                        func = getInputService.GetVerifiedInput(@"[A-Za-z]{3,10}");
                    }
                    catch (TooManyFalseAttemptsException e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    switch (func)
                    {
                        case "bill":
                            businessHandler.ChangeNameOfBill();
                            break;
                        case "category":
                            categoryHandler.ChangeCategory();
                            break;
                        case "event":
                            break;
                    }
                }
                else if (func.Equals("stats"))
                {
                    Console.WriteLine(statsMenu);
                    try
                    {
                        func = getInputService.GetVerifiedInput(@"[A-Za-z]{3,10}");
                    }
                    catch (TooManyFalseAttemptsException e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    switch (func)
                    {
                        case "range":
                            businessHandler.RangedSearch(); 
                            break;
                        case "day":
                            businessHandler.SearchByDate(); 
                            break;
                        case "category":
                            businessHandler.SearchByCategory(); 
                            break;
                    }
                }
                else if(func.Equals("transfer"))
                {
                    businessHandler.TransferMoney(); 
                }
            }
            return 1;
        }


        private string _menuEntry = "What do you want to do?\n\"Add\"\n\"Delete\"\n\"Change\" info\nGenerate data \"stats\"\nTransfer money between bills";
        private string addMenu = "What do you want to add?\nBill\nCategory\nProfit\nExpense";
        private string deleteMenu = "What do you want to delete?\nBill\nCategory\nProfit\nExpense";
        private string changeMenu = "What do you want to change?\nBill\nCategory\nProfit\nExpense";
        private string statsMenu = "What stats do you want to get?\nStats by date \"range\"\nStats by \"day\"\nStats by \"category\"";
    }
}
