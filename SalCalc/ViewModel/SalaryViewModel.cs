using SalCalc.Models;

namespace SalCalc.ViewModel;

//Использованием ViewModel для передачи view несколько значений
public class SalaryViewModel
{
    public  List<Employee> EmployeeList { get; set; }
    public  List<Month> MonthList { get; set; }
    public  List<SystemSalary> SystemSalary { get; set; }
    public  List<TypeSalary> TypeSalary { get; set; }
    public List<Salary> Salaries { get; set; }
}