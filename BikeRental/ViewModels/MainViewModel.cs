using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeRental.ViewModels
{
    public class MainViewModel : Screen
    {
        //uchwyt referencji event aggregatora
        private readonly IEventAggregator _eventAggregator;

        public MainViewModel(IEventAggregator eventAggregator)
        {
            //pobranie refernecji z eventAggregatora
            _eventAggregator = eventAggregator;
            _eventAggregator.Subscribe(this);
        }
    }
}
