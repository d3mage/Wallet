using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public interface IBillContext
    {
        public List<Bill> GetData();
        public void SetData(List<Bill> data);
    }
}
