using System;
using System.Collections.Generic;
using System.Text;
using BLL;

namespace PL
{
    public class Menu
    {
        IGetInputService getInputService;
        IBusinessHandler businessHandler;

       public Menu(IGetInputService getInput, IBusinessHandler handler)
        {
            businessHandler = handler; 
            getInputService = getInput;
        }

        public int Print()
        {
            string func = "";
           
            while(!func.Equals("exit"))
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
                    }
                    break; 
                }
                else if(func.Equals("delete"))
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
                            businessHandler.DeleteBill(); 
                            break;
                    }
                    break;
                }
            }
            return 1;
         }


        private string _menuEntry  = "What do you want to do?\nAdd\nDelete\nChange info\nGenerate data stats";
        private string addMenu = "What do you want to add?\nBill\nCategory\nProfit\nExpense";
    }
}
