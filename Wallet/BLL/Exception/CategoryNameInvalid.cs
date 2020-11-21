using System;

namespace BLL
{
    public class CategoryNameInvalidException : Exception
    {
        public override string Message => "Category name is invalid. ";
    }
}
