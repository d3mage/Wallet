using DAL;
using DAL.Provider;
using System;
using System.Collections.Generic;
using Xunit;

namespace Wallet.Tests.DAL.Tests
{
    public class provider_Tests
    {
        XmlProvider<Bill> provider = new XmlProvider<Bill>();
        string connection = "test.xml";

        [Fact]
        public void XmlProvider_Write_Read_Successfully()
        {
            List<Bill> expected = GetList();

            provider.Write(expected, connection);

            var actual = provider.Read(connection);

            Assert.True(actual != null);
            Assert.Equal(expected.Count, actual.Count);

            for (int i = 0; i < expected.Count; i++)
            {
                Assert.Equal(expected[i].Name, actual[i].Name);
                Assert.Equal(expected[i].Money, actual[i].Money);
            }
        }
        [Fact]
        public void XmlProvider_CatchException_Read()
        {
            string corruptedConnection = "corrupted.xml";

            List<Bill> data = GetList();

            Assert.Throws<InvalidOperationException>(() => provider.Read(corruptedConnection));
        }

        public List<Bill> GetList()
        {
            MoneyEvent profit = new MoneyEvent(false, "worked", "work", 300);
            MoneyEvent expense = new MoneyEvent(true, "relaxed", "work", 300);
            List<MoneyEvent> moneyEvents = new List<MoneyEvent>() { profit, expense };

            Bill bill = new Bill("work bill", 800);
            bill.moneyEvents = moneyEvents;

            List<Bill> toReturn = new List<Bill>() { bill };
            return toReturn;
        }
    }
}
