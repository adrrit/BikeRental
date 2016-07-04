using BikeRental.Classes;
using BikeRental.Notifications;
using BikeRental.POCO;
using Caliburn.Micro;

namespace BikeRental.ViewModels
{
    public class MainViewModel : Screen
    {
        //uchwyt referencji event aggregatora
        private readonly IEventAggregator _eventAggregator;

        /// <summary>
        /// private 
        /// </summary>
        private string _roomNumber;
        private bool _buttonEnableState = true;
        private bool _priceButtonEnableState = false;
        private string _priceToPay;
        private bool _buttonAcceptPriceEnableState = false;


        public MainViewModel(IEventAggregator eventAggregator)
        {
            //referencja eventAggregatora
            _eventAggregator = eventAggregator;
            _eventAggregator.Subscribe(this);
            
        }

        private BindableCollection<Guest> _guestRoom = new BindableCollection<Guest>();
        public BindableCollection<Guest> GuestRoom
        {
            get { return _guestRoom; }
            set
            {
                _guestRoom = value;
                NotifyOfPropertyChange(() => GuestRoom);
            }
        }
        private Guest _selectedGuestRoom;
        public Guest SelectedGuestRoom //null jeśli nie ma wybranych wierszy
        {
            get { return _selectedGuestRoom; }
            set
            {
                _selectedGuestRoom = value;
                NotifyOfPropertyChange(() => SelectedGuestRoom);
                if (SelectedGuestRoom !=null)                
                    PriceButtonEnableState = true;                
                else
                    PriceButtonEnableState = false;                
            }
        }
        public void ConfirmPrice()
        {
            //drukowanie RP, zapis do bazy
        }

        #region public properties
        public string PriceToPay
        {
            get { return _priceToPay; }
            set
            {
                _priceToPay = value;
                NotifyOfPropertyChange(() => PriceToPay);
                if (PriceToPay.Length > 0)
                    ButtonAcceptPriceEnableState = true;
                else
                    ButtonAcceptPriceEnableState = false;
            }
        }
        public string RoomNumber
        {
            get
            {
                return _roomNumber;
            }
            set
            {
                if(_roomNumber!=value)
                {
                    _roomNumber = value;
                    NotifyOfPropertyChange(() => RoomNumber);  
                    if(_roomNumber.Length!=3)
                    {
                        ButtonEnableState = true;
                    }
                    else
                    {
                        ButtonEnableState = false;                        
                        SelectRoom();
                    }
                }
            }
        }   
          
        //jeśli wprowadzono 3 cyfry pobierz meldunki pokoju
        private void SelectRoom()
        {
            var _roomGuests = GuestService.GetGuests(RoomNumber); //meldunki pokoju z Mysql
            if (_roomGuests.Count == 0)
            {
                var message = new SimpleMessageBox();
                message.ShowMessage("Informacja", "Brak meldnuków w wybranym pokoju");
                RoomNumber = "";
            }
            else
            {
                GuestRoom.AddRange(_roomGuests);
            }
        }

        
        public bool ButtonEnableState
        {
            get
            {
                return _buttonEnableState;
            }
            set
            {
                _buttonEnableState = value;
                NotifyOfPropertyChange(() => ButtonEnableState);
            }
        }

        public bool PriceButtonEnableState
        {
            get
            {
                return _priceButtonEnableState;
            }
            set
            {
                _priceButtonEnableState = value;
                NotifyOfPropertyChange(() => PriceButtonEnableState);
            }
        }

        public bool ButtonAcceptPriceEnableState
        {
            get
            {
                return _buttonAcceptPriceEnableState;
            }
            set
            {
                _buttonAcceptPriceEnableState = value;
                NotifyOfPropertyChange(() => ButtonAcceptPriceEnableState);
            }
        }
        #endregion
        #region button bindings
        public void AddNumber(string number)
        {           
            if (number.Length>0)
            {
                RoomNumber += number;
            }           
        }
        public void AddPrice(string number)
        {
            if (number.Length > 0)
            {
                PriceToPay += number;
            }
        }
        public void ClearNumber()
        {            
            RoomNumber = "";
            GuestRoom.Clear();            
        }
        public void ClearPrice()
        {
            PriceToPay = "";
        }
        #endregion
    }
}
