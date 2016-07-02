using Caliburn.Micro;
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
        private string _roomNumber;

        public MainViewModel(IEventAggregator eventAggregator)
        {
            //pobranie refernecji z eventAggregatora
            _eventAggregator = eventAggregator;
            _eventAggregator.Subscribe(this);
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
                }
            }
        }      

        private bool _buttonEnableState = true;
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
    }
}
