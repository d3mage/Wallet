using BLL;
using Moq;
using PL;
using Xunit;

namespace Wallet.Tests.PAL.Tests
{
    public class menu_Tests
    {
        [Fact]
        public void MenuCall_Exit()
        {
            var mock = new Mock<IGetInputService>();
            mock.Setup(x => x.GetVerifiedInput(@"[A-Za-z]{3,10}")).Returns("exit");

            Menu menu = new Menu(mock.Object, null, null, null);
            int actual = menu.Print();

            Assert.Equal(1, actual);
        }

        [Fact]
        public void MenuCall_AddBill()
        {
            var mock = new Mock<IGetInputService>();
            mock.SetupSequence(x => x.GetVerifiedInput(@"[A-Za-z]{3,10}"))
                .Returns("add")
                .Returns("bill")
                .Returns("exit");

            var billMock = new Mock<IBillBusinessHandler>();
            billMock.Setup(x => x.AddBill()).Verifiable();

            Menu menu = new Menu(mock.Object, billMock.Object, null, null);
            menu.Print();

            billMock.Verify(x => x.AddBill(), Times.Once);
        }


    }
}
