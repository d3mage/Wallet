﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class BillsNotInitializedException : Exception
    {
        public override string Message => "In order to create see bills, you have to create them.";
    }
}