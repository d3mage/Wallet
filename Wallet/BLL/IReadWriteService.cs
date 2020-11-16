using System;
using System.Collections.Generic;
using System.Text;
using DAL;

namespace BLL
{
    public interface IReadWriteService
    {
        public List<Bill> ReadData();
        public void WriteData(List<Bill> data); 
    }
}
