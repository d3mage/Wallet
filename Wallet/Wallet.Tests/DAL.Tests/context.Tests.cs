using System.Collections.Generic;
using Xunit;
using Moq;
using DAL;
using DAL.Provider;
using System;

namespace Wallet.Tests.DAL.Tests
{
    public class context_Tests
    {
        string conn = "test.xml";

        [Fact]
        public void GetData_AddSuccessfully()
        {
            var mock = new Mock<IProvider<Bill>>();
            mock.Setup(x => x.Read(conn)).Returns(GetList());

            var context = new BillContext(mock.Object, conn);

            var expected = GetList();
            var actual = context.GetData();

            Assert.True(actual != null);
            Assert.Equal(expected.Count, actual.Count);

            for (int i = 0; i < expected.Count; i++)
            {
                Assert.Equal(expected[i].Name, actual[i].Name);
                Assert.Equal(expected[i].Money, actual[i].Money);
            }
        }

        [Fact]
        public void GetData_ProviderNull()
        {
            IProvider<Bill> provider = null;

            var context = new BillContext(provider, conn);

            Assert.Throws<ProviderException>(() => context.GetData()); 
        }

        [Fact]
        public void GetData_ReadingException()
        {
            var mock = new Mock<IProvider<Bill>>();
            mock.Setup(x => x.Read(conn)).Throws<Exception>();

            var context = new BillContext(mock.Object, conn);

            Assert.Throws<EmptyListException>(() => context.GetData());
        }

        [Fact]
        public void SetData_AddSuccessfully()
        {
            IProvider<Bill> provider = new XmlProvider<Bill>();
            BillContext context = new BillContext(provider, conn);

            var expected = GetList();
            context.SetData(GetList());
            var actual = context.GetData();

            Assert.True(actual != null);
            Assert.Equal(expected.Count, actual.Count);

            for (int i = 0; i < expected.Count; i++)
            {
                Assert.Equal(expected[i].Name, actual[i].Name);
                Assert.Equal(expected[i].Money, actual[i].Money);
            }
        }

        [Fact]
        public void SetData_ProviderNull()
        {
            IProvider<Bill> provider = null;

            var context = new BillContext(provider, conn);

            Assert.Throws<ProviderException>(() => context.SetData(GetList()));
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
