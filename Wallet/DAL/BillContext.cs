using System;
using System.Collections.Generic;
using System.Text;
using DAL.Provider;
namespace DAL
{
    class BillContext
    {
        private List<Bill> _storedData; 

        public string ConnectionString { get; }
        public IProvider<Bill> DataProvider { get; set; }

        public BillContext(IProvider<Bill> provider, string connection)
        {
            DataProvider = provider; 
            ConnectionString = connection; 
        }

        public List<Bill> GetData()
        {
            if (DataProvider != null)
            {
                if (_storedData != null)
                {
                    return _storedData;
                }
                else
                {
                    try
                    {
                        _storedData = DataProvider.Read(ConnectionString);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    return _storedData;
                }
            }
            else throw new InvalidOperationException("Data provider is unavailable");
        }
    }
}
