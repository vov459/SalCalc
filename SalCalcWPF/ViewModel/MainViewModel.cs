using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reactive;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using ReactiveUI;
using SalCalcWpf.Model.Report;

namespace SalCalcWpf
{
    public class MainViewModel:ReactiveObject
    {
        private ObservableCollection<UserControl> _controls;
        private UserControl _main_control;
        public UserControl MainControl
        {
            get => _main_control;
            set => this.RaiseAndSetIfChanged(ref _main_control, value);
        }
        public static ViewModel.EmployeeViewModel EmployeeViewModel { get;private set; }
        private int _current_page;
        //Индекс открытой страныцы для отображения на mainwindow
        public int CurrentPage
        {
            get => _current_page;
            set=>this.RaiseAndSetIfChanged(ref _current_page, value);
        }
        public MainViewModel()
        {
            //Инициализация ViewModel и Model
       
            EmployeeViewModel =new ViewModel.EmployeeViewModel();

            
            //Инициализация коллекций view
            _controls = new ObservableCollection<UserControl> { new View.Employee.EmployeeMain(EmployeeViewModel), new View.Salary.MainSalary(), new View.Report.MainReport()};
            MainControl = _controls[0];//вывод страницы с заказом
            CurrentPage = 1;
        }
        //Комманда открытия странницы
        public ReactiveCommand<string, Unit> OpenPage => ReactiveCommand.Create<string>(OpenPageCommand);
        private void OpenPageCommand(string page_id)
        {
            switch (page_id)
            {

                case "OpenEmployee":
                    MainControl = _controls[0];
                    CurrentPage = 1;
                    break;
                case "Salary":
                    MainControl= _controls[1];
                    CurrentPage = 2;
                    break;
                case "Report":
                    MainControl = _controls[2];
                    CurrentPage = 3;
                    break;
               
                    
            }

        }
    }
}
