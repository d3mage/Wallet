﻿using BLL;
using DAL;
using Moq;
using System.Collections.Generic;
using Xunit;

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
            var mock = new Mock<IReadWriteService>();
            mock.Setup(x => x.ReadData()).Returns(new List<Bill>());

            BillService service = new BillService(mock.Object);

            Assert.Throws<BillsNotInitializedException>(() => service.GetBills());
        }

        [Theory]
        [InlineData(false, 300, 600)]
        [InlineData(true, 400, 100)]
        public void ChangeBillMoney_Theory(bool isExpense, double money, double expected)
        {
            MoneyEvent moneyEvent = new MoneyEvent(isExpense, "worked", 300);
            Bill bill = new Bill("work bill", money);
            BillService service = new BillService(null);

            service.ChangeBillMoney(bill, moneyEvent);

            Assert.Equal(expected, bill.Money);
        }

        [Fact]
        public void ChangeBillMoney_Exception()
        {
            MoneyEvent moneyEvent = new MoneyEvent(true, "worked", 300);
            Bill bill = new Bill("work bill", 250);
            BillService service = new BillService(null);

            Assert.Throws<InsufficientFundsException>(() => service.ChangeBillMoney(bill, moneyEvent));
        }

        public List<Bill> GetList()
        {
            MoneyEvent profit = new MoneyEvent(false, "worked", 300);
            MoneyEvent expense = new MoneyEvent(true, "relaxed", 300);
            List<MoneyEvent> moneyEvents = new List<MoneyEvent>() { profit, expense };

            Category category = new Category("work");
            category.moneyEvents = moneyEvents;
            List<Category> categories = new List<Category>() { category };

            Bill bill = new Bill("work bill", 800);
            bill.categories = categories;

            List<Bill> toReturn = new List<Bill>() { bill };
            return toReturn;
        }

    }
}
