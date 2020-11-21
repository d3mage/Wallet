using DAL;

namespace BLL
{
    public interface ICategoryService
    {
        public bool isCategoryNameAvailable(Bill bill, string name);
        public Category GetCategoryByName(Bill bill, string name);
        public Category CreateNewCategory(string name);
        public void AddCategory(Bill bill, Category category);
        public void DeleteCategory(Bill bill, Category name);
        public void ChangeCategory(Bill bill, string oldName, string newName);
        public void ShowCategories(Bill bill);
    }
}
