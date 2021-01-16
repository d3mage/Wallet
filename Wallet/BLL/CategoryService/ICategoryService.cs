using DAL;
using System.Collections.Generic;

namespace BLL
{
    public interface ICategoryService
    {
        public void AddCategory(string name);
        public void DeleteCategory(string name);
        public void ChangeCategory(IBillService billService, string name, string newName);

        public bool isCategoryNameAvailable(string name);
        public string GetCategoryByName(string name);

        public List<string> GetCategories();
    }
}
