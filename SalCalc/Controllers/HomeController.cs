using Microsoft.AspNetCore.Mvc;
using SalCalc.Models;
using System.Diagnostics;

namespace SalCalc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            HttpContext.Session.SetInt32("OpenPage", 1);
            return View();
        }

        public string AddUser(Employee employee)
        {
            if (employee.IsVarEmployee()!="")
                return employee.IsVarEmployee();
            ICommand command = new EmployeeCommand<Employee>(employee);
           var result= command.Add();
           return result ? "Пользователь успешно добавлен" : "Произошла ошибка при добавлении";
        }
        public IActionResult GetUser()
        {
            ICommand command = new EmployeeCommand<Employee>();
            var userList = command.GetDataList<Employee>();
            return View("UserList", userList);
        }
        public IActionResult UpdateUser(List<Employee> employees)
        {
            ICommand command = new EmployeeCommand<List<Employee>>(employees);
            command.Update();
            return Redirect("~/Home");
        }

        public string DeleteUser(int id)
        {
            ICommand command = new EmployeeCommand<int>(id);
            var result= command.Remove();
            return result ? "Пользователь успешно удален" : "Произошла ошибка при удалении";
        }
        public JsonResult GetEmployee(int id)
        {
            ICommand command = new EmployeeCommand<int>(id);
            return Json(command.GetData<Employee>());
        }
    }
}