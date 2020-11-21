using BLL;
using DAL;
using Moq;
using System.Collections.Generic;
using Xunit;

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
