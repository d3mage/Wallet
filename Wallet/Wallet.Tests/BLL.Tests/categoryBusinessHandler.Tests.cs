using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Moq;
using BLL;
using DAL;

namespace Wallet.Tests.BLL.Tests
{
    public class categoryBusinessHandler_Tests
    {
        [Fact]
        public void AddCategory_Success()
        {
            Category category = new Category("work");

            Bill testBill = new Bill("work bill", 150);
            List<Bill> bills = new List<Bill>() { testBill };

            var inputServiceMock = new Mock<IGetInputService>();
            inputServiceMock.SetupSequence(x => x.GetVerifiedInput(@"[A-Za-z]{0,20}"))
                .Returns("work bill")
                .Returns("work");

            var billServiceMock = new Mock<IBillService>();
            billServiceMock.Setup(x => x.GetBills()).Returns(bills);
            billServiceMock.Setup(x => x.isBillNameAvailable("work bill")).Returns(true);
            billServiceMock.Setup(x => x.GetBillByName("work bill")).Returns(testBill);

            var categoryServiceMock = new Mock<ICategoryService>();
            categoryServiceMock.Setup(x => x.isCategoryNameAvailable(testBill, "work")).Returns(true);
            categoryServiceMock.Setup(x => x.CreateNewCategory("work")).Returns(category);

            CategoryBusinessHandler categoryBusinessHandler = new CategoryBusinessHandler(inputServiceMock.Object,
                billServiceMock.Object, categoryServiceMock.Object);

            categoryBusinessHandler.AddCategory();

            categoryServiceMock.Verify(x => x.CreateNewCategory("work"), Times.Once);
            categoryServiceMock.Verify(x => x.AddCategory(testBill, category), Times.Once);
        }

        [Fact]
        public void DeleteCategory_Success()
        {
            Category category = new Category("work");

            Bill testBill = new Bill("work bill", 150);
            List<Bill> bills = new List<Bill>() { testBill };

            var inputServiceMock = new Mock<IGetInputService>();
            inputServiceMock.SetupSequence(x => x.GetVerifiedInput(@"[A-Za-z]{0,20}"))
                .Returns("work bill")
                .Returns("work");

            var billServiceMock = new Mock<IBillService>();
            billServiceMock.Setup(x => x.GetBills()).Returns(bills);
            billServiceMock.Setup(x => x.GetBillByName("work bill")).Returns(testBill);

            var categoryServiceMock = new Mock<ICategoryService>();

            CategoryBusinessHandler categoryBusinessHandler = new CategoryBusinessHandler(inputServiceMock.Object,
                billServiceMock.Object, categoryServiceMock.Object);

            categoryBusinessHandler.DeleteCategory();


        }
    }
}
