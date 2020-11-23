﻿using DAL;
using System;
using System.Collections.Generic;

namespace BLL
{
    public class CategoryService : ICategoryService
    {
        public bool isCategoryNameAvailable(Bill bill, string name)
        {
            if (bill.categories == null) return true;
            foreach (var c in bill.categories)
            {
                if (c.Name.Equals(name)) return false;
            }
            return true;
        }

        public Category GetCategoryByName(Bill bill, string name)
        {
            foreach (var b in bill.categories)
            {
                if (b.Name.Equals(name)) return b;
            }
            throw new CategoryNameInvalidException();
        }

        public Category CreateNewCategory(string name)
        {
            return new Category(name);
        }


        public void AddCategory(Bill bill, Category category)
        {
            if (bill.categories == null)
            {
                bill.categories = new List<Category>();
            }
            bill.categories.Add(category);
        }

        public void DeleteCategory(Bill bill, Category category)
        {
            if(bill.categories != null)
            {
                bill.categories.Remove(category);
            }
        }

        public void ChangeCategory(Bill bill, string oldName, string newName)
        {
            foreach (var b in bill.categories)
            {
                if (b.Name.Equals(oldName))
                {
                    b.Name = newName;
                }
            }
        }

        public void ShowCategories(Bill bill)
        {
            foreach (Category c in bill.categories)
            {
                Console.WriteLine(c.Name);
            }
        }
    }
}