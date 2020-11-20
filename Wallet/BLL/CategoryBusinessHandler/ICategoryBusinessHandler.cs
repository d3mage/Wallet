using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public interface ICategoryBusinessHandler
    {
        public void AddCategory();
        public void DeleteCategory();
        public void ChangeCategory();
        public int ShowCurrentCategories(); 
    }
}
