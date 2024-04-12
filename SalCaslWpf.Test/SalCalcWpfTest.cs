using Microsoft.EntityFrameworkCore;
using SalCalcWpf.Data;
using SalCalcWpf.Model;
using SalCalcWpf.Model.Employee;
using SalCalcWpf.Model.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SalCaslWpf.Test
{
    public class SalCalcWpfTest
    {
        private static ConnectDB InitContext()
        {
            string guid = Guid.NewGuid().ToString();
            ConnectDB.Guid=guid;
            return new ConnectDB();
        }
        //Правильное высчитываение средней зарплаты сотрудника за 3 месяца
        [Fact]
        public void ValidMidSalaryEmp()
        {
            Employee employee=new Employee();
            Salary salary1 = new Salary { Employee=employee, MonthId=1, Year=2023, SystemSalaryId=1, TypeSalaryId=1, SalaryReceived=50000 };
            Salary salary2 = new Salary { Employee=employee, MonthId=2, Year=2023, SystemSalaryId=1, TypeSalaryId=1, SalaryReceived=100000 };
            Salary salary3 = new Salary { Employee=employee, MonthId=3, Year=2023, SystemSalaryId=1, TypeSalaryId=1, SalaryReceived=120000 };
            using var db = InitContext();
            db.Salaries.Add(salary1);
            db.Salaries.Add(salary2);
            db.Salaries.Add(salary3);
            db.SaveChanges();
            ICalcSalary calcSalary = new CalcSalaryWithMonth();
            EmployeeReport rep = new EmployeeReport(employee, 2023,3,calcSalary);
            Assert.Equal(90000, rep.MidSalary);
        }
        //Правильное высчитывание общей зарплаты сотрудника за 4 месяца
        [Fact]
        public void ValidAllSalaryEmp()
        {
            Employee employee = new Employee();
            Salary salary1 = new Salary { Employee=employee, MonthId=1, Year=2023, SystemSalaryId=1, TypeSalaryId=1, SalaryReceived=50000 };
            Salary salary2 = new Salary { Employee=employee, MonthId=2, Year=2023, SystemSalaryId=1, TypeSalaryId=1, SalaryReceived=100000 };
            Salary salary3 = new Salary { Employee=employee, MonthId=3, Year=2023, SystemSalaryId=1, TypeSalaryId=1, SalaryReceived=120000 };
            Salary salary4 = new Salary { Employee=employee, MonthId=4, Year=2023, SystemSalaryId=1, TypeSalaryId=1, SalaryReceived=10000 };
            using var db = InitContext();
            db.Salaries.Add(salary1);
            db.Salaries.Add(salary2);
            db.Salaries.Add(salary3);
            db.Salaries.Add(salary4);
            db.SaveChanges();
            var sal = db.Salaries.Where(p => p.EmployeeId == employee.Id && p.Year == 2023 &&  p.MonthId<=4);
            Assert.Equal(280000, sal.Sum(p => p.SalaryReceived));
        }
        [Fact]
        public void ValidManyEmpSalMax()
        {
            Employee employee = new Employee();
            Salary salary1 = new Salary { Employee=employee, MonthId=1, Year=2023, SystemSalaryId=1, TypeSalaryId=1, SalaryReceived=50000 };
            Salary salary2 = new Salary { Employee=employee, MonthId=2, Year=2023, SystemSalaryId=1, TypeSalaryId=1, SalaryReceived=100000 };
            Salary salary3 = new Salary { Employee=employee, MonthId=3, Year=2023, SystemSalaryId=1, TypeSalaryId=1, SalaryReceived=120000 };
            Salary salary4 = new Salary { Employee=employee, MonthId=4, Year=2023, SystemSalaryId=1, TypeSalaryId=1, SalaryReceived=10000 };
            using var db = InitContext();
            db.Salaries.Add(salary1);
            db.Salaries.Add(salary2);
            db.Salaries.Add(salary3);
            db.Salaries.Add(salary4);
            Employee employee2 = new Employee();
            Salary salary21 = new Salary { Employee=employee2, MonthId=1, Year=2023, SystemSalaryId=1, TypeSalaryId=1, SalaryReceived=14000 };
            Salary salary22 = new Salary { Employee=employee2, MonthId=2, Year=2023, SystemSalaryId=1, TypeSalaryId=1, SalaryReceived=12555 };
            Salary salary23 = new Salary { Employee=employee2, MonthId=3, Year=2023, SystemSalaryId=1, TypeSalaryId=1, SalaryReceived=1600 };
            Salary salary24 = new Salary { Employee=employee2, MonthId=4, Year=2023, SystemSalaryId=1, TypeSalaryId=1, SalaryReceived=356000 };
            db.Salaries.Add(salary21);
            db.Salaries.Add(salary22);
            db.Salaries.Add(salary23);
            db.Salaries.Add(salary24);
            db.SaveChanges();
            ICalcSalary calcSalary = new CalcSalaryWithMonth();
            ReportList reportList=new ReportList();
            reportList.AddNewReport(employee2, 2023, 4, calcSalary);
            reportList.AddNewReport(employee, 2023, 4, calcSalary);
            Assert.Equal(384155, reportList.MaxSalary);
        }
    }
}
