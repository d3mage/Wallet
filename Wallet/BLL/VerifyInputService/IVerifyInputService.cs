namespace BLL
{
    public interface IVerifyInputService
    {
        public bool isInputCorrect(string input, string pattern);
    }
}
