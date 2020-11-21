using BLL;
using Moq;
using Xunit;

namespace Wallet.Tests.BLL.Tests
{
    public class GetInputService_Tests
    {
        [Fact]
        public void GetInputService_Success()
        {
            string expected = "data";

            var inputMock = new Mock<IReadUserInputService>();
            inputMock.Setup(x => x.ReadInput()).Returns(expected);

            var verifyMock = new Mock<IVerifyInputService>();
            verifyMock.Setup(x => x.isInputCorrect("data", "")).Returns(true);

            GetInputService inputService = new GetInputService(inputMock.Object, verifyMock.Object);

            string actual = inputService.GetVerifiedInput("");

            inputMock.Verify(x => x.ReadInput(), Times.Once);
            verifyMock.Verify(x => x.isInputCorrect("data", ""), Times.Once);
            Assert.Equal(expected, actual);
        }
        [Fact]
        public void GetInputService_FirstInputFalse()
        {
            string expected = "data";

            var inputMock = new Mock<IReadUserInputService>();
            inputMock.Setup(x => x.ReadInput()).Returns(expected);

            var verifyMock = new Mock<IVerifyInputService>();
            verifyMock.Setup(x => x.isInputCorrect("data", "")).Returns(false);

            GetInputService inputService = new GetInputService(inputMock.Object, verifyMock.Object);

            Assert.Throws<TooManyFalseAttemptsException>(() => inputService.GetVerifiedInput(""));
        }
    }
}
