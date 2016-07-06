using Caliburn.Micro;
using System;

namespace BikeRental.POCO
{
    public class RentedBikeHistory : Guest
    {

        private string _user;
        private DateTime _rentalDate;

        public DateTime RentalDate
        {
            get { return _rentalDate; }
            set
            {
                if(_rentalDate != value)
                {
                    _rentalDate = value;
                    NotifyOfPropertyChange(() => RentalDate);
                }
            }
        }

        public string User
        {
            get { return _user; }
            set
            {
                if (_user != value)
                {
                    _user = value;
                    NotifyOfPropertyChange(() => User);
                }
            }
        }
    }
}
