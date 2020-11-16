using System.Collections.Generic;
using Xunit;
using BLL;
using DAL;
using Moq;

namespace Wallet.Tests.BLL.Tests
{
    public class BillService_Tests
    {
        [Theory]
        [InlineData("work bill", false)]
        [InlineData("new bill", true)]
        public void isBillNameAvailable_Theory(string name, bool expected)
        {
            List<Bill> data = GetList();

            var mock = new Mock<IReadWriteService>();
            mock.Setup(x => x.ReadData()).Returns(data);

            BillService service = new BillService();

            bool actual = service.isBillNameAvailable(mock.Object, name);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetBillByName_Success()
        {
            List<Bill> data = GetList();

            var mock = new Mock<IReadWriteService>();
            mock.Setup(x => x.ReadData()).Returns(data);

            Bill testBill = new Bill("work bill", 1500); 
            BillService service = new BillService();

        }

        [Fact]
        public void AddBill_Success()
        {
            List<Bill> data = GetList(); 

            var mock = new Mock<IReadWriteService>();
            mock.Setup(x => x.ReadData()).Returns(data);
            mock.Setup(x => x.WriteData(data)).Verifiable();

            Bill testBill = new Bill("work bill", 1500);
            BillService service = new BillService();

            service.AddBill(mock.Object, testBill);

            mock.Verify(x => x.WriteData(data), Times.Once);
            Assert.Contains(testBill, data); 
        }

        [Fact]
        public void DeleteBill_Success()
        {
            List<Bill> data = GetList();

            var mock = new Mock<IReadWriteService>();
            mock.Setup(x => x.ReadData()).Returns(data);

            BillService service = new BillService();



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
