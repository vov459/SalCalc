using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System.Collections.ObjectModel;
using System.Linq;
using SalCalcWpf.Model.Employee;

namespace SalCalcWpf.Model.Report

{

    //Итоговый отчет с пользователями и расчетом значений
    public class ReportList:ReactiveObject
    {
        [Reactive]
        public ObservableCollection<EmployeeReport> EmployeeReports { get; set; }
        [Reactive]
        public double MinSalary { get; set; }
        [Reactive]
        public double MaxSalary { get; set; }
        [Reactive]
        public double MidSalary { get; set; }

        public void AddNewReport(Employee.Employee employee, int year, int countMonth, ICalcSalary calcSalary)
        {
            EmployeeReports??= new ObservableCollection<EmployeeReport>();
            EmployeeReports.Add(new EmployeeReport(employee, year, countMonth, calcSalary));
            MinSalary = EmployeeReports.Select(p => p.AllSalary).Min(p => p);
            MaxSalary=EmployeeReports.Select(p => p.AllSalary).Max(p => p);
            MidSalary = EmployeeReports.Select(p => p.AllSalary).Median(p => p);
        }
    }
}