using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public interface IVerifyInputService
    {
        public bool isInputCorrect(string input, string pattern); 
    }
}
