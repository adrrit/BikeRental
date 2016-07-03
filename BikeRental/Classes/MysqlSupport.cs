using BikeRental.Interfaces;
using BikeRental.POCO;
using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace BikeRental.Classes
{
    public class MysqlSupport : IDisposable
    {
        private MysqlConfig _configuration;
        private readonly IErrorLogging _logger;
       
        public MysqlSupport()
        {
            _logger = new FileErrorLogger();

            try
            {
                var _serialized = new ReadWriteConfiguration<MysqlConfig>("mysqlConfig.xml");
                _configuration = _serialized.ReadConfiguration();

                Connect(); //implement exception
                       
            }
            catch(Exception ex)
            {
                _logger.LoggError(ex.Message);
            }
        }
        public string Query { get; set; }

        public List<Dictionary<string, object>> ExecuteQuery()
        {
                MySqlCommand command = new MySqlCommand(Query, connection);
                MySqlDataReader reader = command.ExecuteReader();
                List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                while (reader.Read())
                {
                    Dictionary<string, object> row = new Dictionary<string, object>();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        row.Add(reader.GetName(i), reader.GetValue(i));
                    }
                    rows.Add(row);
                }
                reader.Close();                
                return rows;
        }
        public void ExecuteNonQuery()
        {
            MySqlCommand command = new MySqlCommand(Query, connection);
            command.ExecuteNonQuery();
        }

        private MySqlConnection connection = null;
        public MySqlConnection Connection
        {
            get { return connection; }
        }
        private bool Connect()
        {
            bool result = true;
            if (Connection == null)
            {
                try
                {
                    string connstring = string.Format("Server={0}; database={1}; UID={2}; password={3}",
                                                            _configuration.ServerAddress,
                                                            _configuration.DatabaseName,
                                                            _configuration.UserName,
                                                            _configuration.Password);
                    connection = new MySqlConnection(connstring);
                    connection.Open();
                    result = true;
                }
                catch(Exception ex)
                {
                    _logger.LoggError(ex.Message);
                    result = false;
                }
            }
            return result;
        }
        private void Close()
        {
            connection.Close();
        }

        public void Dispose() // IDisposable implementation
        {
            Close();
        }
    }
}
