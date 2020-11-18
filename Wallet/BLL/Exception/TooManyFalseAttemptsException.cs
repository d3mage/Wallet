using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class TooManyFalseAttemptsException : Exception
    {
        public string msg = "You've failed to enter correct data 3 times. Pathetic."; 
    }
}
