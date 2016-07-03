using Caliburn.Micro;

namespace BikeRental.POCO
{
    public class Guest : PropertyChangedBase
    {
        private string _roomNumber;
        private string _name;
        private string _surname;

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
    }
}
