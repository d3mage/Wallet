using DAL.Provider;
using System;
using System.Collections.Generic;

namespace DAL
{
    public class DataContext<T> : IDataContext<T>
    {
        private List<T> _storedData;

        public string ConnectionString { get; }
        public IProvider<T> DataProvider { get; set; }

        public DataContext(IProvider<T> provider, string connection)
        {
            DataProvider = provider;
            ConnectionString = connection;
        }

        public List<T> GetData()
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

        public void SetData(List<T> data)
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
