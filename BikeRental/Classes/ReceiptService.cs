using BikeRental.Interfaces;
using BikeRental.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeRental.Classes
{
    public static class ReceiptService
    {       
        public static void InsertReceipt(Guest _selectedGuest, string _priceToPay, RentalDocument _document, IErrorLogging _logger)
        {
            
            try
            {
                using (var _database = new MysqlSupport())
                {
                  
                    _database.Query = string.Format("insert into rachpok_table "
                                        + "(nrp, nrmel, rodzmel, nrg, datar, npx1, nru, "
                                        + "czasu, cenajed, znizka, cenatot, czyzap, zapl, nrp2, nrrach, pozkas, komen, "
                                        + "wys, nrrez, nrzal, npx2, nroz, wiger, ilzw, warzw) values "
                                        + "('{0}', {1}, '{2}', '{3}', '{4}', '', 534, "
                                        + " 1, {5}, 0, {5}, 'N', 0, '','', 0, '{6}',"
                                        + " 0, '', 0, '', 0, 0, 0, 0)",
                                        _selectedGuest.RoomNumber,
                                        _selectedGuest.GuestNumber,
                                        _selectedGuest.GuestType,
                                        _selectedGuest.GroupNumber,
                                        DateTime.Now.ToString(),
                                        _priceToPay,
                                        "WR " + _document.DocNumber
                                        );
                    _database.ExecuteNonQuery();

                }
            }
            catch (Exception ex)
            {
                _logger.LoggError(ex.Message);
            }
        }
    }
}
