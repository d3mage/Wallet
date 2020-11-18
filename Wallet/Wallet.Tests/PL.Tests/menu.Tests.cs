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
        private string _menuEntry = "What do you want to do?\nAdd new money event\nChange info\nGenerate data info";

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
            inputMock.Setup(x => x.GetVerifiedInput(@"[A-Za-z]{3,10}")).Returns("add");

            var secondaryMenuMock = new Mock<ISecondaryMenu>();
            secondaryMenuMock.Setup(x => x.Add(inputMock.Object)); 

            Menu menu = new Menu(inputMock.Object, secondaryMenuMock.Object);

            menu.Print();

            secondaryMenuMock.Verify(x => x.Add(inputMock.Object), Times.Once); 
        }
    }
}
