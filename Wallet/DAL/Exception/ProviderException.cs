using System;

namespace DAL
{
    public class ProviderException : Exception
    {
        public override string Message => "Provider is null.";
    }
}
