using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using SalCalcWpf.Model;
using SalCalcWpf.Model.Employee;
using SalCalcWpf.Model.Report;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace SalCalcWpf.ViewModel
{
    public class ReportViewModel : ReactiveObject
    {

        [Reactive]
        public Model.Report.ReportList ReportList { get; set; }
        [Reactive]
        public ObservableCollection<Model.Employee.Employee> EmployeeTable { get; set; }
        public List<Month> Months { get; set; }
        public List<int> Years { get; set; }
        [Reactive]
        public Employee Emp { get; set; }
        [Reactive]
        public int MonthId { get; set; }
        [Reactive]
        public int YearId { get; set; }
        [Reactive]
        public bool IsMonth { get; set; }
        public ReportViewModel() {

            UpdateReport();
            Months=Month.GetMonth();
            Years=new List<int> { 2022, 2023, 2024 };
        }
        public void UpdateReport() {
            ReportList=new Model.Report.ReportList();
            var command = new EmployeeCommand<Employee>();
            EmployeeTable=command.GetDataList<Employee>();
        }
        public ReactiveCommand<Unit, Unit> AddReport => ReactiveCommand.Create(() => {

            if (Emp==null)
                return;
            if (IsMonth == false)
                MonthId = 12;
            ReportList.AddNewReport(Emp, YearId, MonthId, getCalc(IsMonth));
        });

        private ICalcSalary getCalc(bool isMonth)
        {
            if (isMonth)
                return new CalcSalaryWithMonth();
            return new CalcSalaryWithoutMonth();
        }

    }
}
