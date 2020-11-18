using System;

namespace BLL
{
    public class ReadUserInputService : IReadUserInputService
    {
        public string ReadInput()
        {
            return Console.ReadLine();
        }
    }
}
