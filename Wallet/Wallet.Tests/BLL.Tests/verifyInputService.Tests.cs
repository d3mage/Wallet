using BLL;
using Xunit;

namespace Wallet.Tests.BLL.Tests
{
    public class verifyInputService_Tests
    {
        [Theory]
        [InlineData("string", @"[A-Za-z]{6}", true)]
        [InlineData("string", @"\d{5,8}", false)]
        public void isInputCorrect_Theory(string name, string pattern, bool expected)
        {
            VerifyInputService service = new VerifyInputService();

            bool actual = service.isInputCorrect(name, pattern);

            Assert.Equal(expected, actual);
        }
    }
}
