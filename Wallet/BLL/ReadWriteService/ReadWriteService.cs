using DAL;
using System.Collections.Generic;

namespace BLL
{
    public class ReadWriteService : IReadWriteService
    {
        private IBillContext _billContext;
        public ReadWriteService(IBillContext context)
        {
            _billContext = context;
        }
        public List<Bill> ReadData()
        {
            List<Bill> data;
            try
            {
                data = _billContext.GetData();
            }
            catch (EmptyListException e)
            {
                data = new List<Bill>();
            }
            return data;
        }
        public void WriteData(List<Bill> data) => _billContext.SetData(data);
    }
}
