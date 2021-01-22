using DAL;
using System;
using System.Collections.Generic;

namespace BLL
{
    public class CategoryService : ICategoryService
    {
        private readonly IReadWriteService<string> readWriteService;

        public CategoryService(IReadWriteService<string> readWrite)
        {
            readWriteService = readWrite; 
        }

        public void AddCategory(string name)
        {
            bool isAvailable = isCategoryNameAvailable(name);
            if (isAvailable == true)
            {
                List<string> data = readWriteService.ReadData();
                data.Add(name);
                readWriteService.WriteData(data);
            }
            else throw new CategoryNameInvalidException();
        }

        public void DeleteCategory(string name)
        {
            bool isAvailable = isCategoryNameAvailable(name);
            if (isAvailable != true)
            {
                List<string> data = readWriteService.ReadData();
                data.Remove(name);
                readWriteService.WriteData(data);
            }
            else throw new CategoryNameInvalidException();
        }

        public void ChangeCategory(IBillService billService, string name, string newName)
        {
            bool isAvailable = isCategoryNameAvailable(name);
            if (isAvailable == true)
            {
                List<string> data = readWriteService.ReadData();
                data[data.IndexOf(name)] = newName; 
                readWriteService.WriteData(data);
                List<Bill> bills = billService.GetBills();
                foreach(var bill in bills)
                {
                    List<MoneyEvent> moneyEvents = bill.moneyEvents;
                    foreach(var moneyEvent in moneyEvents)
                    {
                        if(moneyEvent.category.Equals(name))
                        {
                            moneyEvent.category = newName;
                        }
                    }
                }
            }
            else throw new CategoryNameInvalidException();
        }

        public bool isCategoryNameAvailable(string name)
        {
            string toCheck = GetCategoryByName(name);
            if (toCheck == null)
            {
                return true;
            }
            return false;
        }

        public string GetCategoryByName(string name)
        {
            List<string> data = readWriteService.ReadData();
            if (data.Contains(name))
            {
                return name;
            }
            return null;
        }

        public List<string> GetCategories()
        {
            return readWriteService.ReadData(); 
        }
    }
}
