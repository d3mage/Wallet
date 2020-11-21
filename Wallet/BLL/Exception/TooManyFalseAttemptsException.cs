using System;

namespace BLL
{
    public class TooManyFalseAttemptsException : Exception
    {
        public override string Message => "You've failed to enter correct data 3 times. Pathetic.";
    }
}
