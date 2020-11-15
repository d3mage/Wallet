using DAL;
using DAL.Provider;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Wallet.Tests.DAL.Tests
{
    public class provider_Tests
    {
        [Fact]
        public void XmlProvider_WriteSuccessfully()
        {
            List<Bill> data = GetList();
            string connection = "test.xml";
            XmlProvider<Bill> provider = new XmlProvider<Bill>();

            provider.Write(data, connection);

            Assert.True(false);
        }

        public List<Bill> GetList()
        {
            MoneyProfit profit = new MoneyProfit("worked", 300);
            List<MoneyProfit> profits = new List<MoneyProfit>();
            profits.Add(profit);
            MoneyExpense expense = new MoneyExpense("relaxed", 300);
            List<MoneyExpense> expenses = new List<MoneyExpense>();
            expenses.Add(expense); 

            Category category = new Category("work");
            List<Category> categories = new List<Category>();
            categories.Add(category);
            category.MoneyProfits = profits;
            category.MoneyExpenses = expenses;

            List<Bill> toReturn = new List<Bill>();
            Bill bill = new Bill("work bill", 800);
            bill.categories = categories;
            toReturn.Add(bill);

            return toReturn;
        }
    }
}
