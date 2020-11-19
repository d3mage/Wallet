using System;
using System.Collections.Generic;
using System.Text;
using DAL;

namespace BLL
{
    public class BusinessHandler
    {
        private IGetInputService inputService;
        private IBillService billService;

        public BusinessHandler(IGetInputService input, IBillService bill)
        {
            inputService = input;
            billService = bill; 
        }

        public void AddBill()
        {
            string name = "";
            double money = 150;
            bool verify = false;
            Console.WriteLine("Enter name of bill: ");
            try
            {
                name = inputService.GetVerifiedInput(@"[A-Za-z]{0,20}");
                verify = billService.isBillNameAvailable(name);
            }
            catch (Exception e) when (e is EmptyListException ||
            e is TooManyFalseAttemptsException || e is BillNameInvalidException)
            {
                Console.WriteLine(e.Message);
            }
            if (verify == true)
            {
                billService.AddBill(billService.CreateNewBill(name, money));
            }
        }
    }
}
