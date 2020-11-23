using DAL;
using System;
using System.Collections.Generic;

namespace BLL
{
    public class MoneyEventHandler : IMoneyEventHandler
    {
        private IGetInputService inputService;
        private IBillService billService;
        private ICategoryService categoryService;
        private IMoneyEventService moneyService;

        public MoneyEventHandler(IGetInputService input, IBillService bill, ICategoryService category, IMoneyEventService money)
        {
            inputService = input;
            billService = bill;
            categoryService = category;
            moneyService = money;
        }

        public void AddNewEvent(bool isExpense)
        {
            try
            {
                Console.WriteLine("What's the name of bill:");
                billService.PrintBills();
                string billName = inputService.GetVerifiedInput(@"[A-Za-z]{0,20}");
                Bill bill = billService.GetBillByName(billName);

                Console.WriteLine("What's the name of category?");
                categoryService.ShowCategories(bill);
                string categoryName = inputService.GetVerifiedInput(@"[A-Za-z]{0,20}");
                Category category = categoryService.GetCategoryByName(bill, categoryName);

                Console.WriteLine("What's the name of event?");
                string profitName = inputService.GetVerifiedInput(@"[A-Za-z]{0,20}");
                Console.WriteLine("What's the ammount of money? ");
                double money;
                Double.TryParse(inputService.GetVerifiedInput(@"[A-Za-z]{0,20}"), out money);

                MoneyEvent moneyEvent = moneyService.GetNewMoneyExpense(isExpense, profitName, money);
                moneyService.AddMoneyEvent(category, moneyEvent);

                billService.ChangeBillMoney(bill, moneyEvent);
                billService.ChangeBillInList(bill);

            }
            catch (Exception e) when (e is EmptyListException ||
            e is TooManyFalseAttemptsException || e is BillNameInvalidException ||
            e is CategoryNameInvalidException)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void DeleteEvent()
        {
            try
            {
                Console.WriteLine("What's the name of bill:");
                billService.PrintBills();
                string billName = inputService.GetVerifiedInput(@"[A-Za-z]{0,20}");
                Bill bill = billService.GetBillByName(billName);

                Console.WriteLine("What's the name of category?");
                categoryService.ShowCategories(bill);
                string categoryName = inputService.GetVerifiedInput(@"[A-Za-z]{0,20}");
                Category category = categoryService.GetCategoryByName(bill, categoryName);

                Console.WriteLine("What's the name of event?");
                string profitName = inputService.GetVerifiedInput(@"[A-Za-z]{0,20}");

                MoneyEvent moneyEvent = moneyService.GetEventByName(category, profitName);
                moneyService.DeleteMoneyEvent(category, moneyEvent);

                billService.ChangeBillInList(bill);
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
