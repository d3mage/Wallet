using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class EmptyListException : Exception
    {
        public override string Message => "You need to add any bills first.";
    }
}
