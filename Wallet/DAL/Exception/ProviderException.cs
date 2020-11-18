using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class ProviderException : Exception
    {
        public override string Message => "Provider is null.";
    }
}
