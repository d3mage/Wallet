using DAL;
using System.Collections.Generic;

namespace BLL
{
    public interface IReadWriteService
    {
        public List<Bill> ReadData();
        public void WriteData(List<Bill> data);
    }
}
