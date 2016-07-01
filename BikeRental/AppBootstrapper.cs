using BikeRental.ViewModels;
using Caliburn.Micro;

namespace BikeRental
{
    public class AppBootstrapper : BootstrapperBase
    {

        public AppBootstrapper()
        {
            Initialize();
        }
        protected override void OnStartup(object sender, System.Windows.StartupEventArgs e)
        {
            DisplayRootViewFor<ShellViewModel>();
        }
        private readonly SimpleContainer _container =
           new SimpleContainer();

        // ... Other Bootstrapper Config

        protected override void Configure()
        {
            _container.Singleton<IEventAggregator, EventAggregator>();
        }
    }
}
