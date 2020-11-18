using System;
using System.Collections.Generic;
using System.Text;
using BLL;

namespace PL
{
    public class SecondaryMenu : ISecondaryMenu
    {
        public void Add(IGetInputService service)
        {
            string func = "";
            Console.WriteLine(addMenu);
            try
            {
                func = service.GetVerifiedInput(@"[A-Za-z]{3,10}");
            }
            catch (TooManyFalseAttemptsException e)
            {
                Console.WriteLine(e.msg);
            }
            switch (func)
            {
                case "bill":
                    break; 
            }
        }

        public void tempAddBill(IGetInputService service)
        {
            string name = service.GetVerifiedInput(@"[A-Za-z]{0,20}"); 

        }

        private string addMenu = "What do you want to add?\nBill\nCategory\nProfit\nExpense"; 
    }
}
