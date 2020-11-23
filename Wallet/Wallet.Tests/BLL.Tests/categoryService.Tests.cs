using System;
using System.Collections.Generic;
using System.Text;
using BLL;
using DAL;
using Moq;
using Xunit; 

namespace Wallet.Tests.BLL.Tests
{
    public class categoryService_Tests
    {
        [Fact]
        public void isCategoryNameAvailable_CategoriesNull()
        {
            Bill bill = new Bill("work bill", 800);
            bill.categories = null;

            CategoryService categoryService = new CategoryService();
            bool actual = categoryService.isCategoryNameAvailable(bill, "work");

            Assert.True(actual); 
        }

        [Fact]
        public void isCategoryNameAvailable_False()
        {
            Bill bill = new Bill("work bill", 800);
            Category category = new Category("work");
            bill.categories = new List<Category>() { category };

            CategoryService categoryService = new CategoryService();
            bool actual = categoryService.isCategoryNameAvailable(bill, "work");

            Assert.False(actual);
        }

        [Fact]
        public void isCategoryNameAvailable_True()
        {
            Bill bill = new Bill("work bill", 800);
            Category category = new Category("not work");
            bill.categories = new List<Category>() { category };

            CategoryService categoryService = new CategoryService();
            bool actual = categoryService.isCategoryNameAvailable(bill, "work");

            Assert.True(actual);
        }

        [Fact]
        public void GetCategoryByName_Success()
        {
            Bill bill = new Bill("work bill", 800);
            Category expected = new Category("work");
            bill.categories = new List<Category>() { expected };

            CategoryService categoryService = new CategoryService();
            Category actual = categoryService.GetCategoryByName(bill, "work");

            Assert.Equal(expected, actual);
            Assert.Equal(expected.Name, actual.Name); 
        }

        [Fact]
        public void GetCategoryByName_Exception()
        {
            Bill bill = new Bill("work bill", 800);
            Category expected = new Category("not work");
            bill.categories = new List<Category>() { expected };

            CategoryService categoryService = new CategoryService();

            Assert.Throws<CategoryNameInvalidException>(() => categoryService.GetCategoryByName(bill, "work"));
        }

        [Fact]
        public void AddCategory_Success()
        {
            Bill bill = new Bill("work bill", 800);
            Category category = new Category("work");
            Category expected = new Category("not work");
            bill.categories = new List<Category>() { category, expected };

            Category toAdd = new Category("maybe work");

            CategoryService categoryService = new CategoryService();
            categoryService.AddCategory(bill, toAdd);

            Assert.Contains(toAdd, bill.categories);
        }

        [Fact]
        public void AddCategory_NullList()
        {
            Bill bill = new Bill("work bill", 800);

            Category toAdd = new Category("maybe work");

            CategoryService categoryService = new CategoryService();
            categoryService.AddCategory(bill, toAdd);

            Assert.Contains(toAdd, bill.categories);
        }

        [Fact]
        public void DeleteCategory_Success()
        {
            Bill bill = new Bill("work bill", 800);
            Category toDelete = new Category("maybe work");
            bill.categories = new List<Category>() { toDelete };

            CategoryService categoryService = new CategoryService();
            categoryService.DeleteCategory(bill, toDelete);

            Assert.DoesNotContain(toDelete, bill.categories);
        }

        [Fact]
        public void ChangeCategory_Success()
        {
            Bill bill = new Bill("work bill", 800);
            Category toChange = new Category("maybe work");
            bill.categories = new List<Category>() { toChange };

            CategoryService categoryService = new CategoryService();
            categoryService.ChangeCategory(bill, "maybe work", "work");

            Assert.Equal("work", bill.categories[0].Name);
        }
    }
}
