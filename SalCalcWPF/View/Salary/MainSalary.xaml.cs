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

namespace SalCalcWpf.View.Salary
{
    /// <summary>
    /// Логика взаимодействия для MainSalary.xaml
    /// </summary>
    public partial class MainSalary : UserControl
    {
        private ViewModel.SalaryViewModel SalaryViewModel;
        public MainSalary()
        {
            InitializeComponent();
            SalaryViewModel=new ViewModel.SalaryViewModel();
            DataContext=SalaryViewModel;
            Loaded+=MainSalary_Loaded;
        }

        private void MainSalary_Loaded(object sender, RoutedEventArgs e)
        {
            SalaryViewModel.UpdateSalary();
        }
    }
}
