using System.Collections.Generic;

namespace DAL
{
    public interface IBillContext
    {
        public List<Bill> GetData();
        public void SetData(List<Bill> data);
    }
}
