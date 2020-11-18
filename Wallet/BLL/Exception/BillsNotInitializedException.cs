using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class BillsNotInitializedException : Exception
    {
        public string msg = "In order to create see bills, you have to create them.";
    }
}
