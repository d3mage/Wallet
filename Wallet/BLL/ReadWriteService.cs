using System.Collections.Generic;
using DAL;

namespace BLL
{
    public class ReadWriteService
    {
        private IBillContext _billContext; 
        public ReadWriteService(IBillContext context)
        {
            _billContext = context; 
        }
        public List<Bill> ReadData() => _billContext.GetData();
        public void WriteData(List<Bill> data) => _billContext.SetData(data); 
    }
}
