using BikeRental.Classes;
using Microsoft.Reporting.WinForms;
using System.Windows;

namespace BikeRental.ReportViewer
{
    /// <summary>
    /// Interaction logic for RPReportWindow.xaml
    /// </summary>
    public partial class RPReportWindow : Window
    {
        public RPReportWindow(bool printWithPreview)
        {
            InitializeComponent();

            ReportDataSource reportDataSource = new ReportDataSource();
            reportDataSource.Name = "listaObecnosci"; // Name of the DataSet we set in .rdlc
            //reportDataSource.Value = czasPracyGastronomias;
            reportViewer.LocalReport.ReportPath = "*.rdlc"; // Path of the rdlc file
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
