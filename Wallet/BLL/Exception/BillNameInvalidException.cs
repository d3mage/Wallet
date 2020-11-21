using System;

namespace BLL
{
    public class BillNameInvalidException : Exception
    {
        public override string Message => "Bill name is invalid.";
    }
}
