using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class MoneyEventNameInvalidException : Exception
    {
        public override string Message => "Money event name is invalid. ";
    }
}
