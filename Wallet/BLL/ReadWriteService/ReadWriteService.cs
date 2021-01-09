using DAL;
using System.Collections.Generic;

namespace BLL
{
    public class ReadWriteService<T> : IReadWriteService<T>
    {
        private  IDataContext<T> _dataContext;
        public ReadWriteService(IDataContext<T> context)
        {
            _dataContext = context;
        }
        public List<T> ReadData()
        {
            List<T> data;
            try
            {
                data = _dataContext.GetData();
            }
            catch (EmptyListException e)
            {
                data = new List<T>();
            }
            return data;
        }
        public void WriteData(List<T> data) => _dataContext.SetData(data);
    }
}
