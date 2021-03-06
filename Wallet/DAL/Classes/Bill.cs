﻿using System;
using System.Collections.Generic;

namespace DAL
{
    [Serializable]
    public class Bill
    {
        public string Name { get; set; }
        public double Money { get; set; }

        public List<MoneyEvent> moneyEvents { get; set; }

        public Bill() { }
        public Bill(string name, double money)
        {
            Name = name;
            Money = money;
        }
    }
}
