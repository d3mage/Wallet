using System;

namespace BLL
{
    public class InsufficientFundsException : Exception
    {
        public override string Message => "Insufficent funds on this bill";
    }
}
