using System.Collections.Generic;
using Xunit;
using Moq;
using DAL;
using BLL;

namespace Wallet.Tests.BLL.Tests
{
    public class readWriteService
    {
        [Fact]
        public void ReadData_Success()
        {
            var mock = new Mock<IBillContext>();
            mock.Setup(x => x.GetData()).Returns(GetList());
            var readWriteService = new ReadWriteService(mock.Object);

            var expected = GetList();
            var actual = readWriteService.ReadData();

            Assert.True(actual != null);
            Assert.Equal(expected.Count, actual.Count);

            for (int i = 0; i < expected.Count; i++)
            {
                Assert.Equal(expected[i].Name, actual[i].Name);
                Assert.Equal(expected[i].Money, actual[i].Money);
            }
        }

        [Fact]
        public void WriteData_Success()
        {
            List<Bill> data = GetList(); 
            var mock = new Mock<IBillContext>();
            mock.Setup(x => x.SetData(data)).Verifiable();
            var readWriteService = new ReadWriteService(mock.Object);

            readWriteService.WriteData(data);

            mock.Verify(x => x.SetData(data), Times.Once);
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
