using System;

namespace BLL
{
    public class MoneyEventNameInvalidException : Exception
    {
        public override string Message => "Money event name is invalid. ";
    }
}
