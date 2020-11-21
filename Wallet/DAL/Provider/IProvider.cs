using System.Collections.Generic;

namespace DAL.Provider
{
    public interface IProvider<Bill>
    {
        void Write(List<Bill> data, string connection);
        List<Bill> Read(string connection);
    }
}
