using Caliburn.Micro;

namespace BikeRental.POCO
{
    public class Guest : PropertyChangedBase
    {
        private string _roomNumber;
        private string _name;
        private string _surname;
        private string _guestNumber;
        private string _guestType;
        private string _groupNumber;

        public string RoomNumber
        {
            get { return _roomNumber; }
            set
            {
                if (_roomNumber != value)
                {
                    _roomNumber = value;
                    NotifyOfPropertyChange(() => RoomNumber);
                }
            }
        }
        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    NotifyOfPropertyChange(() => Name);
                }
            }
        }
        public string Surname
        {
            get { return _surname; }
            set
            {
                if (_surname != value)
                {
                    _surname = value;
                    NotifyOfPropertyChange(() => Surname);
                }
            }
        }
        public string GuestNumber
        {
            get { return _guestNumber; }
            set
            {
                if(_guestNumber != value)
                {
                    _guestNumber = value;
                    NotifyOfPropertyChange(() => GuestNumber);
                }
            }
        }
        public string GuestType
        {
            get { return _guestType; }
            set
            {
                if(_guestType != value)
                {
                    _guestType = value;
                    NotifyOfPropertyChange(() => GuestType);
                }
            }
        }
        public string GroupNumber
        {
            get { return _groupNumber; }
            set
            {
                if( _groupNumber != value)
                {
                    _groupNumber = value;
                    NotifyOfPropertyChange(() => GroupNumber);
                }
            }
        }
    }
}
