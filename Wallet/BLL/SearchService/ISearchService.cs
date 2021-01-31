using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.SearchService
{
    public interface ISearchService
    {
        public void GetMoneyInRange(DateTime startDate, DateTime endDate, out double profits, out double expenses);
        public void GetMoneyByDate(DateTime date, out double profits, out double expenses);
        public void GetMoneyByCategory(string categoryName, out double profits, out double expenses);
    }
}
