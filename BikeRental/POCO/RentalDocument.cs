using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeRental.POCO
{
    public class RentalDocument
    {
        public DateTime Date { get; set; }        
        public string DocNumber { get; set; }
        public string RoomNumber { get; set; }
        public int Price { get; set; }
        public string GuestName { get; set; }
        public string GuestSurname { get; set; }
        public string UserName { get; set; }


    }
}
