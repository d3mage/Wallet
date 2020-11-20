using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using BLL;
using DAL; 

namespace Wallet.Tests.BLL.Tests
{
    public class businessHandler
    {
        [Fact]
        public void AddBill_Success()
        {
            Bill testBill = new Bill("work bill", 150);

            var inputServiceMock = new Mock<IGetInputService>();
            inputServiceMock.Setup(x => x.GetVerifiedInput(@"[A-Za-z]{0,20}")).Returns("work bill");

            var billServiceMock = new Mock<IBillService>();
            billServiceMock.Setup(x => x.isBillNameAvailable("work bill")).Returns(true);
            billServiceMock.Setup(x => x.CreateNewBill("work bill", 150)).Returns(testBill);
            billServiceMock.Setup(x => x.AddBill(testBill));

            BillBusinessHandler businessHandler = new BillBusinessHandler(inputServiceMock.Object, billServiceMock.Object);
            businessHandler.AddBill(); 

            billServiceMock.Verify(x => x.CreateNewBill("work bill", 150), Times.Once);
            billServiceMock.Verify(x => x.AddBill(testBill), Times.Once);
        }

        [Fact]
        public void AddBill_ThrowsException()
        {
            Bill testBill = new Bill("work bill", 150);

            var inputServiceMock = new Mock<IGetInputService>();
            inputServiceMock.Setup(x => x.GetVerifiedInput(@"[A-Za-z]{0,20}")).Throws<TooManyFalseAttemptsException>();

            var billServiceMock = new Mock<IBillService>();
            billServiceMock.Setup(x => x.isBillNameAvailable("work bill")).Returns(true);
            billServiceMock.Setup(x => x.CreateNewBill("work bill", 150)).Returns(testBill);
            billServiceMock.Setup(x => x.AddBill(testBill));

            BillBusinessHandler businessHandler = new BillBusinessHandler(inputServiceMock.Object, billServiceMock.Object);
            businessHandler.AddBill();

            billServiceMock.Verify(x => x.CreateNewBill("work bill", 150), Times.Never);
            billServiceMock.Verify(x => x.AddBill(testBill), Times.Never);
        }

        [Fact]
        public void DeleteBill_Success()
        {
            List<Bill> bills = GetList(); 
            Bill testBill = new Bill("work bill", 150);

            var inputServiceMock = new Mock<IGetInputService>();
            inputServiceMock.Setup(x => x.GetVerifiedInput(@"[A-Za-z]{0,20}")).Returns("work bill");

            var billServiceMock = new Mock<IBillService>();
            billServiceMock.Setup(x => x.GetBills()).Returns(bills);
            billServiceMock.Setup(x => x.isBillNameAvailable("work bill")).Returns(false);
            billServiceMock.Setup(x => x.GetBillByName("work bill")).Returns(testBill);

            BillBusinessHandler businessHandler = new BillBusinessHandler(inputServiceMock.Object, billServiceMock.Object);
            businessHandler.DeleteBill();

            billServiceMock.Verify(x => x.DeleteBill(testBill), Times.Once);
        } 
        [Fact]
        public void DeleteBill_NameUnavailable()
        {
            List<Bill> bills = GetList(); 
            Bill testBill = new Bill("work bill", 150);

            var inputServiceMock = new Mock<IGetInputService>();
            inputServiceMock.Setup(x => x.GetVerifiedInput(@"[A-Za-z]{0,20}")).Returns("work bill");

            var billServiceMock = new Mock<IBillService>();
            billServiceMock.Setup(x => x.GetBills()).Returns(bills);
            billServiceMock.Setup(x => x.isBillNameAvailable("work bill")).Returns(true);
            billServiceMock.Setup(x => x.GetBillByName("work bill")).Returns(testBill);

            BillBusinessHandler businessHandler = new BillBusinessHandler(inputServiceMock.Object, billServiceMock.Object);
            businessHandler.DeleteBill();

            billServiceMock.Verify(x => x.DeleteBill(testBill), Times.Never);
        }

        [Fact]
        public void ChangeBill_Success()
        {
            List<Bill> bills = GetList();
            Bill testBill = new Bill("work bill", 150);

            var inputServiceMock = new Mock<IGetInputService>();
            inputServiceMock.SetupSequence(x => x.GetVerifiedInput(@"[A-Za-z]{0,20}"))
                .Returns("work bill")
                .Returns("not work bill");

            var billServiceMock = new Mock<IBillService>();
            billServiceMock.Setup(x => x.GetBills()).Returns(bills);
            billServiceMock.Setup(x => x.isBillNameAvailable("work bill")).Returns(false);

            BillBusinessHandler businessHandler = new BillBusinessHandler(inputServiceMock.Object, billServiceMock.Object);
            businessHandler.ChangeBill(); 

            billServiceMock.Verify(x => x.ChangeBillInfo("work bill", "not work bill"), Times.Once);
        }
        [Fact]
        public void ChangeBill_NameUnavailable()
        {
            List<Bill> bills = GetList();

            var inputServiceMock = new Mock<IGetInputService>();
            inputServiceMock.SetupSequence(x => x.GetVerifiedInput(@"[A-Za-z]{0,20}"))
                .Returns("work bill")
                .Returns("not work bill");

            var billServiceMock = new Mock<IBillService>();
            billServiceMock.Setup(x => x.GetBills()).Returns(bills);
            billServiceMock.Setup(x => x.isBillNameAvailable("work bill")).Returns(true);

            BillBusinessHandler businessHandler = new BillBusinessHandler(inputServiceMock.Object, billServiceMock.Object);
            businessHandler.ChangeBill(); 

            billServiceMock.Verify(x => x.ChangeBillInfo("work bill", "not work bill"), Times.Never);
        }

        [Fact]
        public void ShowCurrentAccounts_Success()
        {
            List<Bill> bills = GetList();

            var billServiceMock = new Mock<IBillService>();
            billServiceMock.Setup(x => x.GetBills()).Returns(bills);

            BillBusinessHandler businessHandler = new BillBusinessHandler(null, billServiceMock.Object);

            int actual = businessHandler.ShowCurrentAccounts();

            Assert.Equal(1, actual);
        }
        [Fact]
        public void ShowCurrentAccounts_Fails()
        {
            List<Bill> bills = GetList();

            var billServiceMock = new Mock<IBillService>();
            billServiceMock.Setup(x => x.GetBills()).Throws<BillsNotInitializedException>();

            BillBusinessHandler businessHandler = new BillBusinessHandler(null, billServiceMock.Object);

            int actual = businessHandler.ShowCurrentAccounts();

            Assert.Equal(-1, actual);
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
