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
            mock.Setup(x => x.GetVerifiedInput("exit")).Returns("exit");

            Menu menu = new Menu(mock.Object);
            menu.Print(); 

        }
    }
}
