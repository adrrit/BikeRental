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
    public class MetroMessageBox
    {
        public async void ShowSimpleMessage(string _header, string _message)
        {
            MetroWindow metroWindow = Application.Current.MainWindow as MetroWindow;
            metroWindow.MetroDialogOptions.ColorScheme = MetroDialogColorScheme.Accented; // set the theme
            await metroWindow.ShowMessageAsync(_header, _message);
        }

        public async Task<MessageDialogResult> ShowMessage(string _header, string _message, string _acceptButton, string _cancelButton)
        {

            MetroWindow metroWindow = Application.Current.MainWindow as MetroWindow;
            metroWindow.MetroDialogOptions.ColorScheme = MetroDialogColorScheme.Accented; // set the theme
            var mySettings = new MetroDialogSettings()
            {
                AffirmativeButtonText = _acceptButton,
                AnimateShow = true,
                NegativeButtonText = _cancelButton

            };
            return await metroWindow.ShowMessageAsync(_header, _message, MessageDialogStyle.AffirmativeAndNegative, mySettings);

        }
    }
}
