using System;
using System.IO;
using Xunit;
using Moq;
using PL;
using BLL;

namespace Wallet.Tests.PAL.Tests
{
    public class menu_Tests
    {
        [Fact]
       public void MenuCall_Exit()
        {
            var mock = new Mock<IGetInputService>();
            mock.Setup(x => x.GetVerifiedInput(@"[A-Za-z]{3,10}")).Returns("exit");

            Menu menu = new Menu(mock.Object, null);
            int actual = menu.Print();

            Assert.Equal(1, actual);
        }
        
        [Fact]
        public void MenuCall_CallsSecondaryMenyAdd()
        {
            var inputMock = new Mock<IGetInputService>();
            inputMock.SetupSequence(x => x.GetVerifiedInput(@"[A-Za-z]{3,10}"))
                .Returns("add")
                .Returns("bill");

            var handlerMock = new Mock<IBusinessHandler>();
            handlerMock.Setup(x => x.AddBill()); 

            Menu menu = new Menu(inputMock.Object, handlerMock.Object);

            menu.Print();

            handlerMock.Verify(x => x.AddBill(), Times.Once); 
        }
    }
}
