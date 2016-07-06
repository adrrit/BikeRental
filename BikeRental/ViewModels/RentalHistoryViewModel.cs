using BikeRental.Classes;
using BikeRental.Interfaces;
using BikeRental.Messages;
using BikeRental.POCO;
using Caliburn.Micro;
using System.Collections.Generic;

namespace BikeRental.ViewModels
{
    public class RentalHistoryViewModel : Screen
    {
        //uchwyt referencji event aggregatora
        private readonly IEventAggregator _eventAggregator;
        //error logger
        private readonly IErrorLogging _logger;
        private BindableCollection<RentedBikeHistory> _rentalHistoryGrid = new BindableCollection<RentedBikeHistory>();
        public RentalHistoryViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.Subscribe(this);
            _logger = new FileErrorLogger();

            GetRentalHistory();
        }
        public void RentBike()
        {
            var message = new NavigationMessage()
            {
                Destination = "MainViewModel"
            };
            _eventAggregator.PublishOnUIThread(message);
        }
        private void GetRentalHistory()
        {
            var _serialized = new ReadWriteConfiguration<List<RentedBikeHistory>>("rentHistory.xml");

            RentalHistoryGrid.AddRange(_serialized.ReadConfiguration());
        }
        public BindableCollection<RentedBikeHistory> RentalHistoryGrid
        {
            get { return _rentalHistoryGrid; }
            set
            {
                _rentalHistoryGrid = value;
                NotifyOfPropertyChange(() => _rentalHistoryGrid);
            }
        }
    }
}
