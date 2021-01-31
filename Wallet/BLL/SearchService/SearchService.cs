using System;
using System.Collections.Generic;
using System.Text;
using DAL; 

namespace BLL.SearchService
{
    class SearchService : ISearchService
    {
        private IBillService billService;

        public SearchService(IBillService service)
        {
            billService = service;
        }

        public void GetMoneyInRange(DateTime startDate, DateTime endDate, out double profits, out double expenses)
        {
            double tempProfits = 0, tempExpenses = 0;

            foreach (var m in bill.moneyEvents)
            {
                if (DateTime.Compare(startDate, m.Date) < 0 && DateTime.Compare(endDate, m.Date) > 0)
                {
                    if (m.isExpense == false)
                    {
                        tempProfits += m.value;
                    }
                    else
                    {
                        tempExpenses -= m.value;
                    }
                    Console.WriteLine(m.ToString());
                }
                else if (DateTime.Compare(endDate, m.Date) <= 0) break;
            }
            profits = tempProfits;
            expenses = tempExpenses;
        }
        public void GetMoneyByDate(DateTime date, out double profits, out double expenses)
        {
            double tempProfits = 0, tempExpenses = 0;

            foreach (var m in bill.moneyEvents)
            {
                if (DateTime.Compare(date, m.Date) == 0)
                {
                    if (m.isExpense == false)
                    {
                        tempProfits += m.value;
                    }
                    else
                    {
                        tempExpenses -= m.value;
                    }
                    Console.WriteLine(m.ToString());
                }
                else if (DateTime.Compare(date, m.Date) <= 0) break;
            }
            profits = tempProfits;
            expenses = tempExpenses;
        }

        public void GetMoneyByCategory(string categoryName, out double profits, out double expenses)
        {
            double tempProfits = 0, tempExpenses = 0;



            foreach (var c in bill.categories)
            {
                if (c.Name.Equals(categoryName))
                {
                    foreach (var m in c.moneyEvents)
                    {
                        if (m.isExpense == false)
                        {
                            tempProfits += m.Value;
                        }
                        else
                        {
                            tempExpenses -= m.Value;
                        }
                    }
                }
            }

            profits = tempProfits;
            expenses = tempExpenses;
        }
        
        private void GetBills()
        {
            List<Bill> bills = billService.GetBills(); 

        }
    }
}
