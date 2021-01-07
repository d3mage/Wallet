using BLL;
using DAL;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace Wallet.Tests.BLL.Tests
{
    public class billBusinessHandler_Tests
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
        public void AddBill_InputThrowsException()
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
        public void AddBill_VerifyThrowsException()
        {
            Bill testBill = new Bill("work bill", 150);

            var inputServiceMock = new Mock<IGetInputService>();
            inputServiceMock.Setup(x => x.GetVerifiedInput(@"[A-Za-z]{0,20}")).Returns("work bill");

            var billServiceMock = new Mock<IBillService>();
            billServiceMock.Setup(x => x.isBillNameAvailable("work bill")).Returns(false);
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
            Bill testBill = new Bill("work bill", 150);

            var inputServiceMock = new Mock<IGetInputService>();
            inputServiceMock.Setup(x => x.GetVerifiedInput(@"[A-Za-z]{0,20}")).Returns("work bill");

            var billServiceMock = new Mock<IBillService>();
            billServiceMock.Setup(x => x.isBillNameAvailable("work bill")).Returns(false);
            billServiceMock.Setup(x => x.GetBillByName("work bill")).Returns(testBill);

            BillBusinessHandler businessHandler = new BillBusinessHandler(inputServiceMock.Object, billServiceMock.Object);
            businessHandler.DeleteBill();

            billServiceMock.Verify(x => x.DeleteBill(testBill), Times.Once);
        }
        [Fact]
        public void DeleteBill_NameUnavailable()
        {
            Bill testBill = new Bill("work bill", 150);

            var inputServiceMock = new Mock<IGetInputService>();
            inputServiceMock.Setup(x => x.GetVerifiedInput(@"[A-Za-z]{0,20}")).Returns("work bill");

            var billServiceMock = new Mock<IBillService>();
            billServiceMock.Setup(x => x.GetBillByName("work bill")).Throws<BillNameInvalidException>();

            BillBusinessHandler businessHandler = new BillBusinessHandler(inputServiceMock.Object, billServiceMock.Object);
            businessHandler.DeleteBill();

            billServiceMock.Verify(x => x.DeleteBill(testBill), Times.Never);
        }

        [Fact]
        public void ChangeBill_Success()
        {
            Bill testBill = new Bill("work bill", 150);

            var inputServiceMock = new Mock<IGetInputService>();
            inputServiceMock.SetupSequence(x => x.GetVerifiedInput(@"[A-Za-z]{0,20}"))
                .Returns("work bill")
                .Returns("not work bill");

            var billServiceMock = new Mock<IBillService>();
            billServiceMock.Setup(x => x.isBillNameAvailable("work bill")).Returns(false);

            BillBusinessHandler businessHandler = new BillBusinessHandler(inputServiceMock.Object, billServiceMock.Object);
            businessHandler.ChangeNameOfBill();

            billServiceMock.Verify(x => x.ChangeBillInfo("work bill", "not work bill"), Times.Once);
        }
        [Fact]
        public void ChangeBill_NameUnavailable()
        {

            var inputServiceMock = new Mock<IGetInputService>();
            inputServiceMock.SetupSequence(x => x.GetVerifiedInput(@"[A-Za-z]{0,20}"))
                .Returns("work bill")
                .Returns("not work bill");

            var billServiceMock = new Mock<IBillService>();
            billServiceMock.Setup(x => x.isBillNameAvailable("work bill")).Returns(true);

            BillBusinessHandler businessHandler = new BillBusinessHandler(inputServiceMock.Object, billServiceMock.Object);
            businessHandler.ChangeNameOfBill();

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

        [Fact]
        public void TransferMoney_Success()
        {
            Bill work = new Bill("work bill", 400);
            Bill notWork = new Bill("not work bill", 100); 

            var inputServiceMock = new Mock<IGetInputService>();
            inputServiceMock.SetupSequence(x => x.GetVerifiedInput(@"[A-Za-z]{0,20}"))
                .Returns("work bill")
                .Returns("not work bill");
            inputServiceMock.Setup(x => x.GetVerifiedInput(@"[0-9]+")).Returns("150"); 

            var billServiceMock = new Mock<IBillService>();
            billServiceMock.Setup(x => x.PrintBills());
            billServiceMock.Setup(x => x.GetBillByName("work bill")).Returns(work); 
            billServiceMock.Setup(x => x.GetBillByName("not work bill")).Returns(notWork);

            BillBusinessHandler billBusinessHandler = new BillBusinessHandler(inputServiceMock.Object, billServiceMock.Object);
            billBusinessHandler.TransferMoney();

            billServiceMock.Verify(x => x.TransferMoney(work, It.IsAny<MoneyEvent>()), Times.Once);
            billServiceMock.Verify(x => x.TransferMoney(notWork, It.IsAny<MoneyEvent>()), Times.Once);
        }

        [Fact]
        public void TransferMoney_ThrowsInputException()
        {
            Bill work = new Bill("work bill", 400);
            Bill notWork = new Bill("not work bill", 100);

            var inputServiceMock = new Mock<IGetInputService>();
            inputServiceMock.Setup(x => x.GetVerifiedInput(@"[A-Za-z]{0,20}")).Throws<EmptyListException>(); 

            var billServiceMock = new Mock<IBillService>();
            billServiceMock.Setup(x => x.PrintBills());
            billServiceMock.Setup(x => x.GetBillByName("work bill")).Returns(work);
            billServiceMock.Setup(x => x.GetBillByName("not work bill")).Returns(notWork);

            BillBusinessHandler billBusinessHandler = new BillBusinessHandler(inputServiceMock.Object, billServiceMock.Object);
            billBusinessHandler.TransferMoney();

            billServiceMock.Verify(x => x.TransferMoney(work, It.IsAny<MoneyEvent>()), Times.Never);
            billServiceMock.Verify(x => x.TransferMoney(notWork, It.IsAny<MoneyEvent>()), Times.Never);
        }

        [Fact]
        public void RangedSearch_Success()
        {
            Bill work = new Bill("work bill", 400);

            var inputServiceMock = new Mock<IGetInputService>();
            inputServiceMock.Setup(x => x.GetVerifiedInput(@"[A-Za-z]{0,20}")).Returns("work bill");
            inputServiceMock.SetupSequence(x => x.GetVerifiedInput(@"(0?[1-9]|[12][0-9]|3[01])-(0?[1-9]|1[0-2])-(\d{4})"))
                .Returns("14-05-2020")
                .Returns("30-8-2021");

            DateTime startDate = new DateTime(2020, 5, 14);
            DateTime endDate = new DateTime(2021, 8, 30);

            double profits = 0, expenses = 0; 

            var billServiceMock = new Mock<IBillService>();
            billServiceMock.Setup(x => x.GetBillByName("work bill")).Returns(work);
            billServiceMock.Setup(x => x.GetMoneyInRange(work, startDate, endDate, out profits, out expenses));

            BillBusinessHandler billBusinessHandler = new BillBusinessHandler(inputServiceMock.Object, billServiceMock.Object);
            billBusinessHandler.RangedSearch();

            billServiceMock.Verify(x => x.GetMoneyInRange(work, startDate, endDate, out profits, out expenses), Times.Once);
        }

        [Fact]
        public void RangedSearch_ThrowsException()
        {
            Bill work = new Bill("work bill", 400);

            var inputServiceMock = new Mock<IGetInputService>();
            inputServiceMock.Setup(x => x.GetVerifiedInput(@"[A-Za-z]{0,20}")).Throws<EmptyListException>();

            DateTime startDate = new DateTime(2020, 5, 14);
            DateTime endDate = new DateTime(2021, 8, 30);

            double profits = 0, expenses = 0;

            var billServiceMock = new Mock<IBillService>();
            billServiceMock.Setup(x => x.GetBillByName("work bill")).Returns(work);
            billServiceMock.Setup(x => x.GetMoneyInRange(work, startDate, endDate, out profits, out expenses));

            BillBusinessHandler billBusinessHandler = new BillBusinessHandler(inputServiceMock.Object, billServiceMock.Object);
            billBusinessHandler.RangedSearch();

            billServiceMock.Verify(x => x.GetMoneyInRange(work, startDate, endDate, out profits, out expenses), Times.Never);
        }

        [Fact]
        public void SearchByDate_Success()
        {
            Bill work = new Bill("work bill", 400);

            var inputServiceMock = new Mock<IGetInputService>();
            inputServiceMock.Setup(x => x.GetVerifiedInput(@"[A-Za-z]{0,20}")).Returns("work bill");
            inputServiceMock.Setup(x => x.GetVerifiedInput(@"(0?[1-9]|[12][0-9]|3[01])-(0?[1-9]|1[0-2])-(\d{4})"))
                .Returns("14-05-2020");

            DateTime date = new DateTime(2020, 5, 14);
            double profits = 0, expenses = 0;

            var billServiceMock = new Mock<IBillService>();
            billServiceMock.Setup(x => x.GetBillByName("work bill")).Returns(work);
            billServiceMock.Setup(x => x.GetMoneyByDate(work, date, out profits, out expenses));

            BillBusinessHandler billBusinessHandler = new BillBusinessHandler(inputServiceMock.Object, billServiceMock.Object);
            billBusinessHandler.SearchByDate();

            billServiceMock.Verify(x => x.GetMoneyByDate(work, date, out profits, out expenses), Times.Once);
        }
        [Fact]
        public void SearchByDate_ThrowsException()
        {
            Bill work = new Bill("work bill", 400);

            var inputServiceMock = new Mock<IGetInputService>();
            inputServiceMock.Setup(x => x.GetVerifiedInput(@"[A-Za-z]{0,20}")).Throws<EmptyListException>();

            DateTime date = new DateTime(2020, 5, 14);
            double profits = 0, expenses = 0;

            var billServiceMock = new Mock<IBillService>();
            billServiceMock.Setup(x => x.GetBillByName("work bill")).Returns(work);
            billServiceMock.Setup(x => x.GetMoneyByDate(work, date, out profits, out expenses));

            BillBusinessHandler billBusinessHandler = new BillBusinessHandler(inputServiceMock.Object, billServiceMock.Object);
            billBusinessHandler.SearchByDate();

            billServiceMock.Verify(x => x.GetMoneyByDate(work, date, out profits, out expenses), Times.Never);
        }

        [Fact]
        public void SearchByCategory()
        {
            Bill work = new Bill("work bill", 400);

            var inputServiceMock = new Mock<IGetInputService>();
            inputServiceMock.SetupSequence(x => x.GetVerifiedInput(@"[A-Za-z]{0,20}"))
                .Returns("work bill")
                .Returns("work");

            double profits = 0, expenses = 0;

            var billServiceMock = new Mock<IBillService>();
            billServiceMock.Setup(x => x.GetBillByName("work bill")).Returns(work);
            billServiceMock.Setup(x => x.GetMoneyByCategory(work, "work", out profits, out expenses));

            BillBusinessHandler billBusinessHandler = new BillBusinessHandler(inputServiceMock.Object, billServiceMock.Object);
            billBusinessHandler.SearchByCategory();

            billServiceMock.Verify(x => x.GetMoneyByCategory(work, "work", out profits, out expenses), Times.Once);
        }

        public List<Bill> GetList()
        {
            DateTime profitTime = new DateTime(2020, 3, 15);
            DateTime expenseTime = new DateTime(2020, 8, 30);
            MoneyEvent profit = new MoneyEvent(false, "worked", 300);
            MoneyEvent expense = new MoneyEvent(true, "relaxed", 300);
            profit.Date = profitTime;
            expense.Date = expenseTime;

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
