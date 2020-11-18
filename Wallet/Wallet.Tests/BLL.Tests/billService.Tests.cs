using System.Collections.Generic;
using Xunit;
using BLL;
using DAL;
using Moq;
using System;

namespace Wallet.Tests.BLL.Tests
{
    public class BillService_Tests
    {
        [Fact]
        public void isBillNameAvailable_Break()
        {
            List<Bill> data = GetList();

            var mock = new Mock<IReadWriteService>();
            mock.Setup(x => x.ReadData()).Throws<EmptyListException>(); 

            BillService service = new BillService();

            Assert.Throws<EmptyListException>(() => service.isBillNameAvailable(mock.Object, "name"));
        }

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

            Bill expected = new Bill("work bill", 800); 
            BillService service = new BillService();

            Bill actual = service.GetBillByName(mock.Object, "work bill");

            Assert.Equal(expected.Name, actual.Name);
            Assert.Equal(expected.Money, actual.Money);
        }

        [Fact]
        public void GetBillByName_ArgumentException()
        {
            List<Bill> data = GetList();

            var mock = new Mock<IReadWriteService>();
            mock.Setup(x => x.ReadData()).Returns(data);

            BillService service = new BillService();

            Assert.Throws<ArgumentException>(() => service.GetBillByName(mock.Object, "test bill")); 
        }

        [Fact]
        public void AddBill_Success()
        {
            List<Bill> data = GetList(); 

            var mock = new Mock<IReadWriteService>();
            mock.Setup(x => x.ReadData()).Returns(data);
            mock.Setup(x => x.WriteData(data)).Verifiable();

            Bill testBill = new Bill("work bill", 800);
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
            mock.Setup(x => x.WriteData(data)).Verifiable();

            Bill testBill = new Bill("work bill", 800);
            BillService service = new BillService();

            service.DeleteBill(mock.Object, testBill);

            mock.Verify(x => x.WriteData(data), Times.Once);
            Assert.DoesNotContain(testBill, data);
        }

        [Fact]
        public void ChangeBillInfo_Success()
        {
            List<Bill> data = GetList();
            List<Bill> expectedList = GetList();
            expectedList[0].Name = "test bill"; 

            var mock = new Mock<IReadWriteService>();
            mock.Setup(x => x.ReadData()).Returns(data);
            mock.Setup(x => x.WriteData(data)).Verifiable();

            Bill testBill = new Bill("test bill", 800); 
            BillService service = new BillService();

            service.ChangeBillInfo(mock.Object, "work bill", "test bill");

            mock.Verify(x => x.WriteData(data), Times.Once);
            Assert.Equal(expectedList[0].Name, data[0].Name); 
        }

        [Fact]
        public void GetBills_CountMoreThan0()
        {
            List<Bill> data = GetList();

            var mock = new Mock<IReadWriteService>();
            mock.Setup(x => x.ReadData()).Returns(data);

            BillService service = new BillService();

            List<Bill> actual = service.GetBills(mock.Object);

            Assert.Equal(data, actual);
        }
        [Fact]
        public void GetBills_CountLessThan0()
        {
            List<Bill> data = GetList();

            var mock = new Mock<IReadWriteService>();
            mock.Setup(x => x.ReadData()).Returns(new List<Bill>());

            BillService service = new BillService();

            Assert.Throws<BillsNotInitializedException>(() => service.GetBills(mock.Object));
        }

        public List<Bill> GetList()
        {
            MoneyProfit profit = new MoneyProfit("worked", 300);
            List<MoneyProfit> profits = new List<MoneyProfit> { profit };
            MoneyExpense expense = new MoneyExpense("relaxed", 300);
            List<MoneyExpense> expenses = new List<MoneyExpense> { expense };

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
