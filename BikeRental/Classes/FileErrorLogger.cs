using BikeRental.Interfaces;
using BikeRental.Notifications;
using System;
using System.IO;

namespace BikeRental.Classes
{
    public class FileErrorLogger : IErrorLogging
    {
        public void LoggError(string message)
        {
            try
            {
                var dateOfException = DateTime.Now;
                using (var streamWriter = new StreamWriter("errorLog.log", true))
                {
                    streamWriter.WriteLine(dateOfException + " : " + message + "\n");
                }
            }
            catch (FileNotFoundException)
            {
                var _simpleNotification = new SimpleMessageBox();
                _simpleNotification.ShowMessage("File Error Logger", "Nie można zapisać do pliku informacji:" + message);
            }

        }
    }
}
