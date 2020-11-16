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
        XmlProvider<Bill> provider = new XmlProvider<Bill>();
        string connection = "test.xml";

        [Fact]
        public void XmlProvider_Write_Read_Successfully()
        {
            List<Bill> expected = GetList();

            provider.Write(expected, connection);
            var actual = provider.Read(connection);

            Assert.True(actual != null);
            Assert.Equal(expected.Count, actual.Count);

            for (int i = 0; i < expected.Count; i++)
            {
                Assert.Equal(expected[i].Name, actual[i].Name);
                Assert.Equal(expected[i].Money, actual[i].Money);
            }
        }
        [Fact]
        public void XmlProvider_CatchException_Read()
        {
            string corruptedConnection = "corrupted.xml"; 
            List<Bill> data = GetList();

            Assert.Throws<InvalidOperationException>(() => provider.Read(corruptedConnection));
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
