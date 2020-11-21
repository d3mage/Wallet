using BLL;
using DAL;
using Moq;
using System.Collections.Generic;
using Xunit;

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
            List<Category> categories = new List<Category>() { category };
            Bill testBill = new Bill("work bill", 150);
            testBill.categories = categories;
            List<Bill> bills = new List<Bill>() { testBill };

            var inputServiceMock = new Mock<IGetInputService>();
            inputServiceMock.SetupSequence(x => x.GetVerifiedInput(@"[A-Za-z]{0,20}"))
                .Returns("work bill")
                .Returns("work");

            var billServiceMock = new Mock<IBillService>();
            billServiceMock.Setup(x => x.GetBills()).Returns(bills);
            billServiceMock.Setup(x => x.GetBillByName("work bill")).Returns(testBill);

            var categoryServiceMock = new Mock<ICategoryService>();
            categoryServiceMock.Setup(x => x.GetCategoryByName(testBill, "work")).Returns(category);

            CategoryBusinessHandler categoryBusinessHandler = new CategoryBusinessHandler(inputServiceMock.Object,
                billServiceMock.Object, categoryServiceMock.Object);

            categoryBusinessHandler.DeleteCategory();

            categoryServiceMock.Verify(x => x.DeleteCategory(testBill, category), Times.Once);
        }

        [Fact]
        public void ChangeCategories_Success()
        {
            Category category = new Category("work");
            List<Category> categories = new List<Category>() { category };
            Bill testBill = new Bill("work bill", 150);
            testBill.categories = categories;
            List<Bill> bills = new List<Bill>() { testBill };

            var inputServiceMock = new Mock<IGetInputService>();
            inputServiceMock.SetupSequence(x => x.GetVerifiedInput(@"[A-Za-z]{0,20}"))
                .Returns("work bill")
                .Returns("work")
                .Returns("not work");

            var billServiceMock = new Mock<IBillService>();
            billServiceMock.Setup(x => x.GetBills()).Returns(bills);
            billServiceMock.Setup(x => x.GetBillByName("work bill")).Returns(testBill);

            var categoryServiceMock = new Mock<ICategoryService>();
            categoryServiceMock.Setup(x => x.isCategoryNameAvailable(testBill, "work bill")).Returns(false);

            CategoryBusinessHandler categoryBusinessHandler = new CategoryBusinessHandler(inputServiceMock.Object,
                billServiceMock.Object, categoryServiceMock.Object);

            categoryBusinessHandler.ChangeCategory();

            categoryServiceMock.Verify(x => x.ChangeCategory(testBill, "work", "not work"), Times.Once);
        }

        [Fact]
        public void ShowCurrentCategories_Success()
        {
            Category category = new Category("work");
            List<Category> categories = new List<Category>() { category };
            Bill testBill = new Bill("work bill", 150);
            testBill.categories = categories;
            List<Bill> bills = new List<Bill>() { testBill };

            var inputServiceMock = new Mock<IGetInputService>();
            inputServiceMock.SetupSequence(x => x.GetVerifiedInput(@"[A-Za-z]{0,20}")).Returns("work bill");

            var billServiceMock = new Mock<IBillService>();
            billServiceMock.Setup(x => x.GetBills()).Returns(bills);
            billServiceMock.Setup(x => x.GetBillByName("work bill")).Returns(testBill);

            var categoryServiceMock = new Mock<ICategoryService>();
            categoryServiceMock.Setup(x => x.ShowCategories(testBill)).Verifiable();

            CategoryBusinessHandler categoryBusinessHandler = new CategoryBusinessHandler(inputServiceMock.Object,
                billServiceMock.Object, categoryServiceMock.Object);

            categoryBusinessHandler.ShowCurrentCategories();

            categoryServiceMock.Verify(x => x.ShowCategories(testBill), Times.Once);
        }
    }
}
