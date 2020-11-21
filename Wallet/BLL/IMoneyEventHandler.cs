using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public interface IMoneyEventHandler
    {
        public void AddNewEvent(bool isExpense);
        public void DeleteEvent();
    }
}
