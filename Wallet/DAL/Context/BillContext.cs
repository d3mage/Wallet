using System;
using System.Collections.Generic;
using DAL.Provider;

namespace DAL
{
    public class BillContext : IBillContext
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
                        throw new EmptyListException(); 
                    }
                    return _storedData;
                }
            }
            else throw new ProviderException(); 
        }

        public void SetData(List<Bill> data)
        {
            if (DataProvider != null)
            {
                DataProvider.Write(data, ConnectionString);
                _storedData = data;
            }
            else throw new ProviderException();
        }
    }
}
