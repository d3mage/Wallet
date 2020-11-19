using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class BillNameInvalidException : Exception
    {
        public override string Message => "Bill name is invalid.";
    }
}
