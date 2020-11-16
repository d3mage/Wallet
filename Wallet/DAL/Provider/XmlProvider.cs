using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace DAL.Provider
{
    public class XmlProvider<Bill> : IProvider<Bill>
    {
        public void Write(List<Bill> data, string connection)
        {
            using FileStream fs = new FileStream(connection, FileMode.OpenOrCreate);
            XmlSerializer formatter = new XmlSerializer(data.GetType());
            try
            {
                formatter.Serialize(fs, data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

       public List<Bill> Read(string connection)
        {
            List<Bill> data;
            using (FileStream fs = new FileStream(connection, FileMode.OpenOrCreate))
            {
                XmlSerializer formatter = new XmlSerializer(typeof(List<Bill>));
                try
                {
                    data = (List<Bill>)formatter.Deserialize(fs);
                }
                catch (InvalidOperationException ex)
                {
                    throw ex;
                }
            }
            return data;
        }
    }
}
