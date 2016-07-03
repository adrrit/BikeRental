using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BikeRental.Notifications
{
    public class SimpleMessageBox
    {
        public async void ShowMessage(string _header, string _message)
        {
            MetroWindow metroWindow = Application.Current.MainWindow as MetroWindow;
            metroWindow.MetroDialogOptions.ColorScheme = MetroDialogColorScheme.Accented; // set the theme
            await metroWindow.ShowMessageAsync(_header, _message);
        }
    }
}
