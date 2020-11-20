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
        public void isBillNameAvailable_False()
        {
            List<Bill> data = GetList();

            var mock = new Mock<IReadWriteService>();
            mock.Setup(x => x.ReadData()).Throws<EmptyListException>(); 

            BillService service = new BillService(mock.Object);

            bool actual = service.isBillNameAvailable("");

            Assert.True(actual); 
        }

        [Theory]
        [InlineData("work bill", false)]
        [InlineData("new bill", true)]
        public void isBillNameAvailable_Theory(string name, bool expected)
        {
            List<Bill> data = GetList();

            var mock = new Mock<IReadWriteService>();
            mock.Setup(x => x.ReadData()).Returns(data);

            BillService service = new BillService(mock.Object);

            bool actual = service.isBillNameAvailable(name);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetBillByName_Success()
        {
            List<Bill> data = GetList();

            var mock = new Mock<IReadWriteService>();
            mock.Setup(x => x.ReadData()).Returns(data);

            Bill expected = new Bill("work bill", 800); 
            BillService service = new BillService(mock.Object);

            Bill actual = service.GetBillByName("work bill");

            Assert.Equal(expected.Name, actual.Name);
            Assert.Equal(expected.Money, actual.Money);
        }

        [Fact]
        public void GetBillByName_ArgumentException()
        {
            List<Bill> data = GetList();

            var mock = new Mock<IReadWriteService>();
            mock.Setup(x => x.ReadData()).Returns(data);

            BillService service = new BillService(mock.Object);

            Assert.Throws<BillNameInvalidException>(() => service.GetBillByName("test bill")); 
        }

        [Fact]
        public void AddBill_Success()
        {
            List<Bill> data = GetList(); 

            var mock = new Mock<IReadWriteService>();
            mock.Setup(x => x.ReadData()).Returns(data);
            mock.Setup(x => x.WriteData(data)).Verifiable();

            Bill testBill = new Bill("work bill", 800);
            BillService service = new BillService(mock.Object);

            service.AddBill(testBill);

            mock.Verify(x => x.WriteData(data), Times.Once);
            Assert.Contains(testBill, data); 
        }

        [Fact]
        public void AddBill_ThrowsException()
        {
            List<Bill> data = new List<Bill>();

            var mock = new Mock<IReadWriteService>();
            mock.Setup(x => x.ReadData()).Throws<EmptyListException>();
            mock.Setup(x => x.WriteData(data)).Verifiable();

            Bill testBill = new Bill("work bill", 800);
            BillService service = new BillService(mock.Object);
            data.Add(testBill);

            service.AddBill(testBill);

            mock.Verify(x => x.WriteData(data), Times.Once);
        }

        [Fact]
        public void DeleteBill_Success()
        {
            List<Bill> data = GetList();

            var mock = new Mock<IReadWriteService>();
            mock.Setup(x => x.ReadData()).Returns(data);
            mock.Setup(x => x.WriteData(data)).Verifiable();

            Bill testBill = new Bill("work bill", 800);
            BillService service = new BillService(mock.Object);

            service.DeleteBill(testBill);

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
            BillService service = new BillService(mock.Object);

            service.ChangeBillInfo("work bill", "test bill");

            mock.Verify(x => x.WriteData(data), Times.Once);
            Assert.Equal(expectedList[0].Name, data[0].Name); 
        }

        [Fact]
        public void ChangeBillInList_Success()
        {
            Bill testBill = new Bill("test bill", 800);
            List<Bill> data = new List<Bill>();
            data.Add(testBill); 

            var mock = new Mock<IReadWriteService>();
            mock.Setup(x => x.ReadData()).Returns(data);
            mock.Setup(x => x.WriteData(data)).Verifiable();

            Bill changeBill = new Bill("test bill", 300); 
            BillService service = new BillService(mock.Object);

            service.ChangeBillInList(changeBill);

            Assert.Equal(changeBill.Name, data[0].Name);
            Assert.Equal(changeBill.Money, data[0].Money);
            Assert.Equal(changeBill.categories, data[0].categories);
        }

        [Fact]
        public void GetBills_CountMoreThan0()
        {
            List<Bill> data = GetList();

            var mock = new Mock<IReadWriteService>();
            mock.Setup(x => x.ReadData()).Returns(data);

            BillService service = new BillService(mock.Object);

            List<Bill> actual = service.GetBills();

            Assert.Equal(data, actual);
        }
        [Fact]
        public void GetBills_CountLessThan0()
        {
            List<Bill> data = GetList();

            var mock = new Mock<IReadWriteService>();
            mock.Setup(x => x.ReadData()).Returns(new List<Bill>());

            BillService service = new BillService(mock.Object);

            Assert.Throws<BillsNotInitializedException>(() => service.GetBills());
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
