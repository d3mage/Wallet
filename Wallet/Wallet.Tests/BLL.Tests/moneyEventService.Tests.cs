using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Moq;
using DAL;
using BLL;

namespace Wallet.Tests.BLL.Tests
{
    public class moneyEventService
    {
        [Fact]
        public void AddMoneyEvent()
        {
            Category category = new Category("category");
            category.moneyEvents = null;
            MoneyEvent moneyEvent = new MoneyEvent(false, "yes", 300);

            MoneyEventService moneyEventService = new MoneyEventService();
            moneyEventService.AddMoneyEvent(category, moneyEvent); 

            Assert.Contains(category.moneyEvents, moneyEvent); 
        }
    }
}
