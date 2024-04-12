using Microsoft.AspNetCore.Mvc;
using SalCalc.Models;
using SalCalc.Models.Report;
using SalCalc.ViewModel;

namespace SalCalc.Controllers;

public class ReportController : Controller
{
    //Создания списка отчетов для каждого пользователя в отдельности
    private  static List<ReportList> _reportLists { get; set; }

    public ReportController()
    {
        _reportLists ??= new List<ReportList>();
    }
    private void SetGuid()
    {
        //Присваивание каждому пользователю уникального id и создания "отчета" для каждого пользователя и добавление в статистический список каждый раз когда пользователь открывает index, если отчет существует, пересоздает его
        var id = "";
        if (!HttpContext.Session.Keys.Contains("Guid"))
        {
            id = Guid.NewGuid().ToString();
            HttpContext.Session.SetString("Guid",id);
        }
        else
        {
            id = HttpContext.Session.GetString("Guid");
            var rep = _reportLists.FirstOrDefault(p => p.Id == id);
            if (rep != null)
                _reportLists.Remove(rep);
        }
        _reportLists.Add(new ReportList {Id = id});
    }

    private ReportList GetThisReport()
    {
        var id = HttpContext.Session.GetString("Guid");
        return  _reportLists.FirstOrDefault(p => p.Id == id);
    }
    public IActionResult Index()
    {
        HttpContext.Session.SetInt32("OpenPage", 3);
        SetGuid();
        ICommand command = new EmployeeCommand<Employee>();
        var userList = command.GetDataList<Employee>();
        return View(new ReportViewModel{ EmployeeList = userList});
    }

    public void AddUserInReport(int id, int year,int month, bool withMonth)
    {
        var rep = GetThisReport();
        ICalcSalary calcSalary;
        if (withMonth)
            calcSalary = new CalcSalaryWithMonth();
        else
            calcSalary = new CalcSalaryWithoutMonth();
        ICommand command = new EmployeeCommand<int>(id);
        var user = command.GetData<Employee>();
        rep.AddNewReport(user, year, month, calcSalary);
    }

    public IActionResult GetReport()
    {
        return View("ReportList", GetThisReport());
    }
}