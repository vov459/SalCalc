using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using SalCalcWpf.Model;
using SalCalcWpf.Model.Employee;

namespace SalCalcWpf.ViewModel
{
    public class SalaryViewModel:ReactiveObject
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
                this.RaiseAndSetIfChanged(ref _is_new, value);
            }
        }
        private UserControl _main_control;

        public UserControl MainControl
        {
            get => _main_control;
            set => this.RaiseAndSetIfChanged(ref _main_control, value);
        }

        private Model.Salary _add_Salary;
        public Model.Salary AddSalary
        {
            get => _add_Salary;
            set => this.RaiseAndSetIfChanged(ref _add_Salary, value);
        }
        private ObservableCollection<Model.Salary> _Salary_table;
        public ObservableCollection<Model.Salary> SalaryTable
        {
            get => _Salary_table;
            set => this.RaiseAndSetIfChanged(ref _Salary_table, value);
        }
        private ObservableCollection<Model.Employee.Employee> _employee_table;
        public ObservableCollection<Model.Employee.Employee> EmployeeTable
        {
            get => _employee_table;
            set => this.RaiseAndSetIfChanged(ref _employee_table, value);
        }
        public List<Month> Months { get; set; }
        public List<SystemSalary> SystemSalaries { get; set; }
        public List<TypeSalary> TypeSalaries { get; set; }
        public List<int> Years { get; set; }
        private ObservableCollection<UserControl> _controls;

        public SalaryViewModel()
        {
            _controls = new ObservableCollection<UserControl> { new View.Salary.SalaryAdd(), new View.Salary.SalaryTable() };
            IsNew = false;
            AddSalary=new Model.Salary();
            SystemSalaries=SystemSalary.GetSystemSalary();
            Months=Month.GetMonth();
            TypeSalaries=TypeSalary.GetTypeSalary();
            Years=new List<int> { 2022, 2023, 2024 };
            UpdateSalary();
        }
        public void UpdateSalary()
        {
            ICommand command = new SalaryCommand<Salary>();
            SalaryTable = command.GetDataList<Salary>();
            command=new EmployeeCommand<Employee>();
            EmployeeTable=command.GetDataList<Employee>();
              AddSalary=new Model.Salary();
        }
        //переключение окна добавления и таблицы
        public ReactiveCommand<Unit, Unit> IsNewCommand => ReactiveCommand.Create(() => { IsNew = !IsNew; });
        //Добавить нового пользователя
        public ReactiveCommand<Unit, Unit> AddSalaryCommand => ReactiveCommand.Create(() => {

            ICommand command = new SalaryCommand<Salary>(AddSalary);
            command.Add();
            AddSalary=new Salary();
            UpdateSalary();

        });
        //отменить обновление пользователей
        public ReactiveCommand<Unit, Unit> CanselSalary => ReactiveCommand.Create(() => {
            ICommand command = new SalaryCommand<Salary>();
            SalaryTable = command.GetDataList<Salary>();
        });
        //Обновить пользваетелей
        public ReactiveCommand<Unit, Unit> UpdateSalaryCommand => ReactiveCommand.Create(() => {
            ICommand command = new SalaryCommand<ObservableCollection<Salary>>(SalaryTable);
            command.Update();
            UpdateSalary();
        });

        public ReactiveCommand<Model.Salary, Unit> DeleteSalary => ReactiveCommand.Create<Model.Salary>(DeleteSalaryCommand);
        private void DeleteSalaryCommand(Model.Salary user)
        {
            if (user==null)
                return;

            ICommand command = new SalaryCommand<int>(user.Id);
            command.Remove();
            UpdateSalary();
        }
    }
}
