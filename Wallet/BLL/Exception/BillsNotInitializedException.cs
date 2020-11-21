using System;

namespace BLL
{
    public class BillsNotInitializedException : Exception
    {
        public override string Message => "In order to see bills, you have to create them.";
    }
}
