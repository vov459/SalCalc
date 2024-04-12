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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SalCalcWpf.View.Report
{
    /// <summary>
    /// Логика взаимодействия для MainReport.xaml
    /// </summary>
    public partial class MainReport : UserControl
    {
        private ViewModel.ReportViewModel ReportViewModel { get; set; }
        public MainReport()
        {
            InitializeComponent();

            ReportViewModel= new ViewModel.ReportViewModel();
            DataContext= ReportViewModel;
            Loaded+=MainReport_Loaded;

        }

        private void MainReport_Loaded(object sender, RoutedEventArgs e)
        {
            ReportViewModel.UpdateReport();
        }
    }
}
