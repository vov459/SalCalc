using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using ReactiveUI;
using SalCalcWpf.Model;
using SalCalcWpf.Model.Employee;
using SalCalcWpf.View.Employee;

namespace SalCalcWpf.ViewModel
{
    public class EmployeeViewModel:ReactiveObject
    {
        private bool _is_new;
        //Вывод страницы с новым пользователем
        public bool IsNew
        {
            get => _is_new;
            set
            {
                if (value)
                    MainControl = _controls[0];
                else
                    MainControl=_controls[1];
                this.RaiseAndSetIfChanged(ref _is_new,value);
            }
        }
        private UserControl _main_control;

        public UserControl MainControl
        {
            get => _main_control;
            set => this.RaiseAndSetIfChanged(ref _main_control, value);
        }
        
        private Model.Employee.Employee _add_employee;
        public Model.Employee.Employee AddEmployee
        {
            get => _add_employee;
            set=>this.RaiseAndSetIfChanged(ref _add_employee, value);
        }
        private ObservableCollection<Model.Employee.Employee> _employee_table;
        public ObservableCollection<Model.Employee.Employee> EmployeeTable
        {
            get => _employee_table;
            set=>this.RaiseAndSetIfChanged(ref _employee_table, value);
        }
        private ObservableCollection<UserControl> _controls;

        public EmployeeViewModel()
        {
            _controls = new ObservableCollection<UserControl> {new View.Employee.AddEmployee(), new View.Employee.EmployeeTable() };
            IsNew = false;
            AddEmployee=new Model.Employee.Employee();
            UpdateEmployee();
            
        }
        public void UpdateEmployee()
        {
            ICommand command = new EmployeeCommand<Employee>();
            EmployeeTable = command.GetDataList<Employee>();
        }
        //переключение окна добавления и таблицы
        public ReactiveCommand<Unit, Unit> IsNewCommand => ReactiveCommand.Create(() => { IsNew = !IsNew; });
        //Добавить нового пользователя
        public ReactiveCommand<Unit, Unit> AddEmployeeCommand => ReactiveCommand.Create(() => {

            var emp_valid = AddEmployee.IsVarEmployee();
            if (emp_valid!="")
            {
                MessageBox.Show(emp_valid);
                return;
            }
            ICommand command = new EmployeeCommand<Employee>(AddEmployee);
            command.Add();
            AddEmployee=new Employee();

        });
        //отменить обновление пользователей
        public ReactiveCommand<Unit, Unit> CanselEmployee => ReactiveCommand.Create(() => {
            ICommand command = new EmployeeCommand<Employee>();
            EmployeeTable = command.GetDataList<Employee>();
        });
        //Обновить пользваетелей
        public ReactiveCommand<Unit, Unit> UpdateEmployeeCommand => ReactiveCommand.Create(() => {
            ICommand command = new EmployeeCommand<ObservableCollection<Employee>>(EmployeeTable);
            command.Update();
        });

        public ReactiveCommand<Model.Employee.Employee, Unit> DeleteEmployee => ReactiveCommand.Create<Model.Employee.Employee>(DeleteUserCommand);
        private void DeleteUserCommand(Model.Employee.Employee user)
        {
            if (user==null)
                return;
            
            ICommand command = new EmployeeCommand<int>(user.Id);
            command.Remove();
            UpdateEmployee();
        }

    }
}
