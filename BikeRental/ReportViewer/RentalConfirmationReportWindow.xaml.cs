using BikeRental.Classes;
using BikeRental.POCO;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BikeRental.ReportViewer
{
    /// <summary>
    /// Interaction logic for RentalConfirmationReportWindow.xaml
    /// </summary>
    public partial class RentalConfirmationReportWindow : Window
    {
        public RentalConfirmationReportWindow(List<RentedBikeHistory> _document, bool printWithPreview)
        {
            InitializeComponent();
            ReportDataSource reportDataSource = new ReportDataSource();
            reportDataSource.Name = "Document"; // Name of the DataSet we set in .rdlc
            reportDataSource.Value = _document;
            reportViewer.LocalReport.ReportPath = "RentalConfirmationReport.rdlc"; // Path of the rdlc file
            reportViewer.LocalReport.DataSources.Add(reportDataSource);
            if (printWithPreview == true)
            {
                reportViewer.SetDisplayMode(DisplayMode.PrintLayout);
                reportViewer.RefreshReport();
            }
            else
            {
                AutoPrintCls autoprintme = new AutoPrintCls(reportViewer.LocalReport);
                autoprintme.Print();

                this.Close();
            }
        }
        private void reportViewer_RenderingComplete(object sender, Microsoft.Reporting.WinForms.RenderingCompleteEventArgs e)
        {

        }
    }
}
