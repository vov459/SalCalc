using Microsoft.AspNetCore.Mvc;
using SalCalc.Models;
using SalCalc.ViewModel;

namespace SalCalc.Controllers;

public class SalaryController : Controller
{
    public IActionResult Index()
    { 
        HttpContext.Session.SetInt32("OpenPage", 2);
        ICommand command = new EmployeeCommand<Employee>();
        var userList = command.GetDataList<Employee>();
        return View(new ViewModel.SalaryViewModel{EmployeeList=userList, TypeSalary = TypeSalary.GetTypeSalary(), MonthList = Month.GetMonth(), SystemSalary = SystemSalary.GetSystemSalary()});
    }
    public string AddSalary(Salary salary)
    {
        Console.WriteLine(salary.MonthId);
        ICommand command = new SalaryCommand<Salary>(salary);
        var result=command.Add();
        return result ? "Заплата успешно добавлена" : "Произошла ошибка при добавлении зарплаты";
    }

    public IActionResult GetSalary()
    {
        ICommand command = new SalaryCommand<Salary>();
        var salaryList = command.GetDataList<Salary>();
        command = new EmployeeCommand<Employee>();
        var userList = command.GetDataList<Employee>();
        return View("SalaryList", new SalaryViewModel{ EmployeeList=userList, TypeSalary = TypeSalary.GetTypeSalary(), MonthList = Month.GetMonth(), SystemSalary = SystemSalary.GetSystemSalary(), Salaries = salaryList});
    }
    public IActionResult UpdateSalary(List<Salary> salaries)
    {
        ICommand command = new SalaryCommand<List<Salary>>(salaries);
        command.Update();
        return Redirect("~/Salary");
    }
    public string DeleteSalary(int id)
    {
        ICommand command = new SalaryCommand<int>(id);
        var result= command.Remove();
        return result ? "Зарплата успешно удалена" : "Произошла ошибка при удалении";
    }
}