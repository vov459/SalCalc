namespace SalCalc.Models.Report;


//Итоговый отчет с пользователями и расчетом значений
public class ReportList
{
    public   string Id { get; set; }
    public List<EmployeeReport> EmployeeReports { get; set; }
    public  double MinSalary { get; set; }
    public  double MaxSalary { get; set; }
    public  double MidSalary { get; set; }

    public void AddNewReport(Employee employee, int year, int countMonth, ICalcSalary calcSalary)
    {
        EmployeeReports??= new List<EmployeeReport>();
        EmployeeReports.Add(new EmployeeReport(employee, year,countMonth,calcSalary));
        MinSalary = EmployeeReports.Select(p => p.AllSalary).Min(p => p);
        MaxSalary=EmployeeReports.Select(p => p.AllSalary).Max(p => p);
        MidSalary = EmployeeReports.Select(p => p.AllSalary).Median(p => p);
    }
}