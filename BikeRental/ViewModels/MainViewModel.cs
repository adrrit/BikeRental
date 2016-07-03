using BikeRental.Classes;
using BikeRental.Notifications;
using BikeRental.POCO;
using Caliburn.Micro;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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
              

        public MainViewModel(IEventAggregator eventAggregator)
        {
            //referencja eventAggregatora
            _eventAggregator = eventAggregator;
            _eventAggregator.Subscribe(this);

            GuestRoom = new BindableCollection<Guest>();

            //using (var mysql = new MysqlSupport())
            //{
            //    //for testing
            //    mysql.Query = "select nrp from prac_table limit 2;";
            //    foreach(var row in mysql.ExecuteQuery())
            //    {
            //        MessageBox.Show(row["nrp"].ToString());
            //    }
            //}


        }

        private BindableCollection<Guest> _guestRoom;
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
        public Guest SelectedGuestRoom
        {
            get { return _selectedGuestRoom; }
            set
            {
                _selectedGuestRoom = value;
                NotifyOfPropertyChange(() => SelectedGuestRoom);
            }
        }
        #region public properties
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
          
        private void SelectRoom()
        {
            var _simpleMessage = new SimpleMessageBox();
            _simpleMessage.ShowMessage("Hi", "OK");

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
        #endregion
        #region button bindings
        public void AddNumber(string number)
        {           
            if (number.Length>0)
            {
                RoomNumber += number;
            }           
        }
        public void ClearNumber()
        {
            RoomNumber = "";
            
            GuestRoom.Add(new Guest
            {//tmp testing
                RoomNumber = "77070",
                Name = "asd",
                Surname = "asdasd"
            });
        }
        #endregion
    }
}
