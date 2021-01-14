//using DAL;
//using System;

//namespace BLL
//{
//    public class CategoryBusinessHandler : ICategoryBusinessHandler
//    {
//        private IGetInputService inputService;
//        private IBillService billService;
//        private ICategoryService categoryService;
//        public CategoryBusinessHandler(IGetInputService input, IBillService bill, ICategoryService category)
//        {
//            inputService = input;
//            billService = bill;
//            categoryService = category;
//        }
//        public void AddCategory()
//        {
//            try
//            {
//                Console.WriteLine("What's the name of bill:");
//                billService.PrintBills();
//                string billName = inputService.GetVerifiedInput(@"[A-Za-z]{0,20}");
//                Bill bill = billService.GetBillByName(billName);

//                Console.WriteLine("What's the name of category?");
//                string categoryName = inputService.GetVerifiedInput(@"[A-Za-z]{0,20}");
//                bool available = categoryService.isCategoryNameAvailable(bill, categoryName);
//                if (available != true) { throw new CategoryNameInvalidException(); }

//                categoryService.AddCategory(bill, categoryService.CreateNewCategory(categoryName));

//                billService.ChangeBillInList(bill);
//            }
//            catch (Exception e) when (e is EmptyListException ||
//            e is TooManyFalseAttemptsException || e is BillNameInvalidException ||
//            e is CategoryNameInvalidException)
//            {
//                Console.WriteLine(e.Message);
//            }
//        }
//        public void DeleteCategory()
//        {
//            try
//            {
//                Console.WriteLine("What's the name of bill:");
//                billService.PrintBills();
//                string billName = inputService.GetVerifiedInput(@"[A-Za-z]{0,20}");
//                Bill bill = billService.GetBillByName(billName);

//                Console.WriteLine("What's the name of category to delete?");
//                categoryService.ShowCategories(bill);

//                string categoryName = inputService.GetVerifiedInput(@"[A-Za-z]{0,20}");

//                categoryService.DeleteCategory(bill, categoryService.GetCategoryByName(bill, categoryName));

//                billService.ChangeBillInList(bill);
//            }
//            catch (Exception e) when (e is EmptyListException ||
//            e is TooManyFalseAttemptsException || e is BillNameInvalidException ||
//            e is CategoryNameInvalidException)
//            {
//                Console.WriteLine(e.Message);
//            }
//        }
//        public void ChangeCategory()
//        {
//            try
//            {
//                Console.WriteLine("What's the name of bill:");
//                billService.PrintBills();

//                string billName = inputService.GetVerifiedInput(@"[A-Za-z]{0,20}");
//                Bill bill = billService.GetBillByName(billName);

//                Console.WriteLine("What's the name of category to change?");
//                categoryService.ShowCategories(bill);

//                string oldName = inputService.GetVerifiedInput(@"[A-Za-z]{0,20}");
//                bool available = categoryService.isCategoryNameAvailable(bill, oldName);
//                if (available == true) { throw new CategoryNameInvalidException(); }

//                Console.WriteLine("Enter new name of category");
//                string newName = inputService.GetVerifiedInput(@"[A-Za-z]{0,20}");

//                categoryService.ChangeCategory(bill, oldName, newName);

//                billService.ChangeBillInList(bill);
//            }
//            catch (Exception e) when (e is EmptyListException ||
//            e is TooManyFalseAttemptsException || e is BillNameInvalidException ||
//            e is CategoryNameInvalidException)
//            {
//                Console.WriteLine(e.Message);
//            }
//        }

//        public void ShowCurrentCategories()
//        {
//            try
//            {
//                Console.WriteLine("What's the name of bill:");
//                billService.PrintBills();
//                string billName = inputService.GetVerifiedInput(@"[A-Za-z]{0,20}");
//                Bill bill = billService.GetBillByName(billName);

//                categoryService.ShowCategories(bill);
//            }
//            catch (Exception e) when (e is EmptyListException ||
//            e is TooManyFalseAttemptsException || e is BillNameInvalidException ||
//            e is CategoryNameInvalidException)
//            {
//                Console.WriteLine(e.Message);
//            }
//        }
//    }
//}
