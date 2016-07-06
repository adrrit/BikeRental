using BikeRental.Classes;
using BikeRental.Interfaces;
using BikeRental.Messages;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeRental.ViewModels
{    
    public class ShellViewModel : Conductor<object>, IHandle<NavigationMessage>
    {
        private readonly IEventAggregator _eventAggregator;

        public ShellViewModel()
        {
            _eventAggregator = new EventAggregator();
            _eventAggregator.Subscribe(this);

            // Show MainView on app startup
            ShowMainView(); 
        }

        public void Handle(NavigationMessage message)
        {
            try
            {
                if (message.Destination == "RentalHistoryViewModel")
                {
                    
                   ActivateItem(new RentalHistoryViewModel(_eventAggregator));
                }
                if (message.Destination == "MainViewModel")
                {

                    ActivateItem(new MainViewModel(_eventAggregator));
                }               

            }
            catch (Exception ex)
            {
                IErrorLogging logger = new FileErrorLogger(); //logowanie błędów do pliku
                logger.LoggError(ex.Message);
            }
        }

        private void ShowMainView()
        {
            ActivateItem(new MainViewModel(_eventAggregator));
        }
    }
}
