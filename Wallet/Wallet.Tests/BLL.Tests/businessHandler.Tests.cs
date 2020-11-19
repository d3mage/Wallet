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

            BusinessHandler businessHandler = new BusinessHandler(inputServiceMock.Object, billServiceMock.Object);
            businessHandler.AddBill(); 

            billServiceMock.Verify(x => x.CreateNewBill("work bill", 150), Times.Once);
            billServiceMock.Verify(x => x.AddBill(testBill), Times.Once);
        }
    }
}
