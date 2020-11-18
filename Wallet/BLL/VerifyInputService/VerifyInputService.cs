using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace BLL
{
    public class VerifyInputService : IVerifyInputService
    {
        public bool isInputCorrect(string input, string pattern)
        {
            return Regex.IsMatch(input, pattern); 
        }
    }
}
