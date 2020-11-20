using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class CategoryNameInvalidException : Exception
    {
        public override string Message => "Category name is invalid. ";
    }
}
