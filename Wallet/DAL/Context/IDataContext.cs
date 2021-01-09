using System.Collections.Generic;

namespace DAL
{
    public interface IDataContext<T>
    {
        public List<T> GetData();
        public void SetData(List<T> data);
    }
}
