using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class ProviderException : Exception
    {
        public string msg;
        public ProviderException() : base() { msg = "Provider is null."}
    }
}
