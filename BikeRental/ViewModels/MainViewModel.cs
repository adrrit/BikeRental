using BikeRental.Classes;
using BikeRental.Interfaces;
using BikeRental.Messages;
using BikeRental.Notifications;
using BikeRental.POCO;
using BikeRental.ReportViewer;
using Caliburn.Micro;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace BikeRental.ViewModels
{
    public class MainViewModel : Screen
    {
        //uchwyt referencji event aggregatora
        private readonly IEventAggregator _eventAggregator;
        //error logger
        private readonly IErrorLogging _logger;

        /// <summary>
        /// private 
        /// </summary>
        private string _roomNumber;
        private bool _buttonEnableState = true;
        private bool _priceButtonEnableState = false;
        private string _priceToPay;
        private bool _buttonAcceptPriceEnableState = false;
        private bool _toggleSwitchCheckedState = false;
        private string _userLabel;
        private bool _rentalConfirmationbuttonEnableState = false;


        public MainViewModel(IEventAggregator eventAggregator)
        {
            //referencja eventAggregatora
            _eventAggregator = eventAggregator;
            _eventAggregator.Subscribe(this);

            _logger = new FileErrorLogger();

            UserLabel = "Wojciech Kukuczka";           
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
                if (SelectedGuestRoom != null)
                {
                    PriceButtonEnableState = true;
                    RentalConfirmationbuttonEnableState = true;
                }
                else
                {
                    PriceButtonEnableState = false;
                    RentalConfirmationbuttonEnableState = false;
                }
            }
        }
        /// <summary>
        /// drukowanie RP, zapis do bazy
        /// </summary>
        public async void ConfirmPrice()
        {
            try
            {
                #region Print
                //pobranie następnego numeru dokumentu
                var _serialized = new ReadWriteConfiguration<Counter>("counter.xml");
                var _dokNumber = _serialized.ReadConfiguration();

                var _documentList = new List<RentalDocument>();

                var _document = new RentalDocument()
                {
                    Date = DateTime.Now,
                    DocNumber = String.Format("{0}/{1:00000}", "90", _dokNumber.DocumentNumber),
                    GuestName = SelectedGuestRoom.Name,
                    GuestSurname = SelectedGuestRoom.Surname,
                    Price = int.Parse(PriceToPay),
                    RoomNumber = SelectedGuestRoom.RoomNumber,
                    UserName = UserLabel
                };

                _documentList.Add(_document);
                for (int i = 0; i < 2; i++)
                {
                    RPReportWindow _dialog = new RPReportWindow(_documentList, false);
                }
                #endregion

                var _metroMessageBox = new MetroMessageBox();
                var _messageBoxResoult = await _metroMessageBox.ShowMessage("Czy zapisać?",
                    "Sprawdź poprawność wydruku.",
                    "++++++++ OK - zapisz ++++++++",
                    "-------- Anuluj! --------");

                if (_messageBoxResoult == MessageDialogResult.Affirmative)
                {
                    //zapis do bazy
                    try
                    {
                        //zapis rachunku do bazy
                        ReceiptService.InsertReceipt(SelectedGuestRoom, PriceToPay, _document, _logger);

                        //Drukowanie, zapis udany - zwiększ counter
                        _dokNumber.DocumentNumber += 1;
                        var _serialize = new ReadWriteConfiguration<Counter>("counter.xml");
                        _serialize.WriteConfiguration(_dokNumber);

                        ClearNumber(); //Czyść wszystko po poprawnym zapisaniu, drukowaniu
                    }
                    catch(Exception ex)
                    {
                        _logger.LoggError(ex.Message);
                    }
                }
                else
                {
                    //zamknij MessageBox
                }
            }
            catch(Exception ex)
            {
                _logger.LoggError(ex.Message);
            }
        }

        #region public properties
        public string PriceToPay
        {
            get { return _priceToPay; }
            set
            {
                _priceToPay = value;
                NotifyOfPropertyChange(() => PriceToPay);
                int _price = 0;
                if (int.TryParse(PriceToPay, out _price))
                {   
                    if (_price >0)
                        ButtonAcceptPriceEnableState = true;
                }
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
                var message = new MetroMessageBox();
                message.ShowSimpleMessage("Informacja", "Brak meldnuków w wybranym pokoju");
                RoomNumber = "";
            }
            else
            {
                GuestRoom.AddRange(_roomGuests);
            }
        }
        public void RentalHistory()
        {
            var message = new NavigationMessage()
            {
                Destination = "RentalHistoryViewModel"
            };
            _eventAggregator.PublishOnUIThread(message);
        }
        public void RentalConfirmation()
        {
            var _serialied = new ReadWriteConfiguration<List<RentedBikeHistory>>("rentHistory.xml");
            var _rentHistory = _serialied.ReadConfiguration();
            if(_rentHistory == null) //brak historii
            {
                _rentHistory = new List<RentedBikeHistory>();
            }

            var _rentedBike = new RentedBikeHistory()
            {
                GuestNumber = SelectedGuestRoom.GuestNumber,
                GroupNumber = SelectedGuestRoom.GroupNumber,
                GuestType = SelectedGuestRoom.GuestType,
                Name = SelectedGuestRoom.Name,
                Surname = SelectedGuestRoom.Surname,
                RoomNumber = SelectedGuestRoom.RoomNumber,
                User = UserLabel,
                RentalDate = DateTime.Now
            };

            _rentHistory.Add(_rentedBike);

            _serialied.WriteConfiguration(_rentHistory);

            var _rentedBikeToReport = new List<RentedBikeHistory>();
            _rentedBikeToReport.Add(_rentedBike);
            for (int i = 0; i < 2; i++)
            {
                RentalConfirmationReportWindow _window = new RentalConfirmationReportWindow(_rentedBikeToReport, false);
            }
            ClearNumber();
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
        public bool ToggleSwitchCheckedState
        {
            get { return _toggleSwitchCheckedState; }
            set
            {
                _toggleSwitchCheckedState = value;
                NotifyOfPropertyChange(() => ToggleSwitchCheckedState);
                if (ToggleSwitchCheckedState == false)                
                    UserLabel = "Wojciech Kukuczka";                
                else
                    UserLabel = "Michał Jopek";
            }
        }

        public bool RentalConfirmationbuttonEnableState
        {
            get { return _rentalConfirmationbuttonEnableState; }
            set
            {
                _rentalConfirmationbuttonEnableState = value;
                NotifyOfPropertyChange(() => RentalConfirmationbuttonEnableState);
            }
        }
        public string UserLabel
        {
            get { return _userLabel; }
            set
            {
                _userLabel = value;
                NotifyOfPropertyChange(() => UserLabel);
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
            ClearPrice();
            

        }
        public void ClearPrice()
        {
            PriceToPay = "";
        }
        #endregion
    }
}
