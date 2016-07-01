using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeRental.ViewModels
{    
    public class ShellViewModel : Conductor<object>
    {
        private readonly IEventAggregator _eventAggregator;

        public ShellViewModel()
        {
            _eventAggregator = new EventAggregator();
            _eventAggregator.Subscribe(this);

            // Show MainView on app startup
            ShowMainView(); 
        }

        private void ShowMainView()
        {
            ActivateItem(new MainViewModel(_eventAggregator));
        }
    }
}
