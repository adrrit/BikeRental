using BikeRental.Interfaces;
using System;
using System.IO;
using System.Xml.Serialization;

namespace BikeRental.Classes
{
    public class ReadWriteConfiguration<T> where T : class
    {
        private readonly IErrorLogging _logger;
        private readonly string _path;

        public ReadWriteConfiguration(string path)
        {
            _logger = new FileErrorLogger();
            _path = path;
        }

        public void WriteConfiguration(T serializedClass)
        {
            try
            {
                XmlSerializer x = new XmlSerializer(serializedClass.GetType());
                using (var sw = new StreamWriter(_path))
                {
                    x.Serialize(sw, serializedClass);
                }
            }
            catch (Exception ex)
            {
                _logger.LoggError(ex.Message);
            }
        }
        public T ReadConfiguration()
        {
            try
            {
                XmlSerializer x = new XmlSerializer(typeof(T));
                using (var sr = new StreamReader(_path))
                {
                    return (T)x.Deserialize(sr);
                }
            }
            catch (Exception ex)
            {
                _logger.LoggError(ex.Message);
                return null;
            }
        }
    }
}
