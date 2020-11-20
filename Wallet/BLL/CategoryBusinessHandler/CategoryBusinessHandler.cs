using System;
using System.Collections.Generic;
using System.Text;
using DAL; 

namespace BLL
{
    public class CategoryBusinessHandler : ICategoryBusinessHandler
    {
        private IGetInputService inputService;
        private IBillService billService;
        private ICategoryService categoryService;
        public CategoryBusinessHandler(IGetInputService input, IBillService bill, ICategoryService category)
        {
            inputService = input;
            billService = bill;
            categoryService = category; 
        }
        public void AddCategory()
        {
            try
            {
                Console.WriteLine("What's the name of bill:");
                List<Bill> bills = billService.GetBills();
                foreach (var b in bills)
                {
                    Console.WriteLine(b.Name);
                }
                string billName = inputService.GetVerifiedInput(@"[A-Za-z]{0,20}");
                Bill bill = billService.GetBillByName(billName);

                Console.WriteLine("What's the name of category?");
                string categoryName = inputService.GetVerifiedInput(@"[A-Za-z]{0,20}");
                bool available = categoryService.isCategoryNameAvailable(bill, categoryName); 
                if(available != true) { throw new CategoryNameInvalidException();  }

                categoryService.AddCategory(bill, categoryService.CreateNewCategory(categoryName));

                billService.ChangeBillInList(bill); 
            }
            catch (Exception e) when (e is EmptyListException ||
            e is TooManyFalseAttemptsException || e is BillNameInvalidException || 
            e is CategoryNameInvalidException)
            {
                Console.WriteLine(e.Message);
            }
        }
        public void DeleteCategory()
        {
            try
            {
                Console.WriteLine("What's the name of bill:");
                List<Bill> bills = billService.GetBills();
                foreach (var b in bills)
                {
                    Console.WriteLine(b.Name);
                }
                string billName = inputService.GetVerifiedInput(@"[A-Za-z]{0,20}");
                Bill bill = billService.GetBillByName(billName);

                Console.WriteLine("What's the name of category to delete?");
                foreach(var c in bill.categories)
                {
                    Console.WriteLine(c.Name);
                }
                string categoryName = inputService.GetVerifiedInput(@"[A-Za-z]{0,20}");
                Category category = categoryService.GetCategoryByName(bill, categoryName);

                categoryService.DeleteCategory(bill, category); 

                billService.ChangeBillInList(bill);
            }
            catch (Exception e) when (e is EmptyListException ||
            e is TooManyFalseAttemptsException || e is BillNameInvalidException ||
            e is CategoryNameInvalidException)
            {
                Console.WriteLine(e.Message);
            }
        }
        public void ChangeCategory()
        {

        }
        public int ShowCurrentCategories()
        {
            return 0;
        }

    }
}
