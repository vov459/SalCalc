using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SalCalcWpf.View.Employee
{
    /// <summary>
    /// Логика взаимодействия для EmployeeMain.xaml
    /// </summary>
    public partial class EmployeeMain : UserControl
    {
        private ViewModel.EmployeeViewModel EmployeeViewModel;
        public EmployeeMain(ViewModel.EmployeeViewModel EmployeeViewModel)
        {
            this.EmployeeViewModel = EmployeeViewModel;
            InitializeComponent();
            DataContext=this.EmployeeViewModel;
            Loaded+=EmployeeMain_Loaded;
        }

        private void EmployeeMain_Loaded(object sender, RoutedEventArgs e)
        {
            EmployeeViewModel.UpdateEmployee();
        }
    }
}
