using System;
using System.Collections.Generic;
using System.Text;
using DAL;

namespace DAL.Provider
{
   public interface IProvider<Bill>
    {
        void Write(List<Bill> data, string connection);
        List<Bill> Read(string connection);
    }
}
