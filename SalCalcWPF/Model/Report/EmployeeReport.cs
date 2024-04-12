using System.Linq;
using SalCalcWpf.Data;
using SalCalcWpf.Model.Employee;

namespace SalCalcWpf.Model.Report;



//Реализация интерфейса для паттерна стратег
public interface ICalcSalary
{
    public double AllSalary(int empId, int year, int monthCount);
    public double MidSalary(int empId, int year, int monthCount);
}
//Реализация подчета среднего и максимального значения зарплаты пользователей без месяцев
public class CalcSalaryWithoutMonth : ICalcSalary
{
    private ConnectDB db = new ConnectDB();
    public double AllSalary(int empId, int year, int monthCount)
    {
        var sal = db.Salaries.Where(p => p.EmployeeId == empId && p.Year == year);
        if (!sal.Any())
            return 0;
        return sal.Sum(p => p.SalaryReceived);
    }

    public double MidSalary(int empId, int year, int monthCount)
    {
        var sal = db.Salaries.Where(p => p.EmployeeId == empId && p.Year == year);
        if (!sal.Any())
            return 0;
        return sal.Average(p => p.SalaryReceived);
    }
}
//Реализация подчета среднего и максимального значения зарплаты пользователей с добвлением месяцев
public class CalcSalaryWithMonth : ICalcSalary
{
    private ConnectDB db = new ConnectDB();
    public double AllSalary(int empId, int year, int monthCount)
    {
        
        var sal = db.Salaries.Where(p => p.EmployeeId == empId && p.Year == year &&  p.MonthId<=monthCount);
        if (!sal.Any())
            return 0;
        return sal.Sum(p => p.SalaryReceived);
    }

    public double MidSalary(int empId, int year, int monthCount)
    {
        var sal = db.Salaries.Where(p => p.EmployeeId == empId && p.Year == year &&  p.MonthId<=monthCount);
        if (!sal.Any())
            return 0;
        return sal.Average(p => p.SalaryReceived);
    }
}
public class EmployeeReport
{
    public Employee.Employee Employee { get; set; }
    public int Year { get; set; }
    public int CountMonth { get; set; }

    public EmployeeReport(Employee.Employee employee, int year, int countMonth, ICalcSalary calcSalary)
    {
        this.Employee = employee;
        this.Year = year;
        this.CountMonth = countMonth;
        if (CountMonth > 12 || CountMonth==0)
            CountMonth = 12;
        MidSalary = calcSalary.MidSalary(employee.Id, year, CountMonth);
        AllSalary = calcSalary.AllSalary(employee.Id, year, CountMonth);
    }
    public  double MidSalary { get; set; }
    public  double AllSalary { get; set; }
}