using Xunit;
using SalCalc.Models;
using Microsoft.EntityFrameworkCore;

namespace SalCalc.Test
{
    public class SalCalcTest
    {
        
        private static ConnectDB InitContext()
        {
            var builder = new DbContextOptionsBuilder<ConnectDB>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            
            return new ConnectDB(builder.Options);
        }
        [Fact]
        //Тест распознает ли программа правильный email при добавлении нового пользователя
        public void ValidEmployeeEmail()
        {
            Employee employee= new Employee { Email = "test@yandex.ru" };
            Assert.True(employee.VarEmpEmail());
        }
        [Fact]//Проверка подключения к БД
        public void ConnectDbTest()
        {
            var db = new ConnectDB();
            Assert.True(db.Database.CanConnect());
        }
        [Fact] //Проверка, корректно ли создались месяца в базе данных
        public void ValidDbMonth()
        {
            var db = new ConnectDB();
            var august = db.Month.FirstOrDefault(p => p.Id==8);
            Assert.NotNull(august);
            Assert.Equal("Август", august.Name);
        }
        [Fact]
        public void NoValidEmployeeEmail()
        {
            Employee employee = new Employee { Email = "test.yandex.ru" };
            Assert.False(employee.VarEmpEmail());
        }
        [Fact]
        //Тест на правильность распознавания номера телефона
        public void ValidEmployeePhone()
        {
            Employee employee = new Employee { PhoneNumber = "+79852346222" };
            Assert.True(employee.VarEmpPhone());
        }
        [Fact]
        //Тест распознает ли программа некорректно введеный номер телефона
        public void NoValidEmployeePhone()
        {
            Employee employee = new Employee { PhoneNumber= "54267345" };
            Assert.False(employee.VarEmpPhone());
        }
        [Fact]
        public void ValidNewEmp()
        {
            Employee employee = new Employee { FirstName = "Иван", SecondName="Иванович", LastName="Иванов", Email="ivanov@mail.ru", PhoneNumber="+79112456222" };
            Assert.Equal("",employee.IsVarEmployee());//Если все правильно, функция должна вывести пустую строку
        }
        [Fact]
        //Тест на распознает ли система неккоретно введеные данные нового пользователя
        public void NotValidNewEmp()
        {
            //отсуствует имя 
            Employee employee = new Employee { SecondName = "Иванович", LastName = "Иванов",  Email = "ivanov@mail.ru",PhoneNumber = "+79112456222" };
            Assert.NotEqual("", employee.IsVarEmployee());
        }
        [Fact] //Тестировние добавление и получения пользователя из базы данных
        public void TestAddSelectEmp()
        {
            var db = InitContext();
            Employee employee = new Employee { FirstName = "Иван", SecondName="Иванович", LastName="Иванов", Email="ivanov@mail.ru", PhoneNumber="+79112456222" };
            db.Employee.Add(employee);
            db.SaveChanges();
            Employee employee2 = db.Employee.LastOrDefault();
            Assert.Equal<Employee>(employee2, employee);
        }
        [Fact] //Тестировние добавления и получения зарплаты пользователя из базы данных (тест правильность получения пользователя методом inlude
        public void TestAddSelectSalaryEmp()
        {
           var db= InitContext();
            Employee employee = new Employee { FirstName = "Иван", SecondName="Иванович", LastName="Иванов", Email="ivanov@mail.ru", PhoneNumber="+79112456222" };
            db.Employee.Add(employee);
            db.SaveChanges();
            Assert.Equal(1, employee.Id);//Проверка что emp корректно присвоился Id
            Salary salary = new Salary { MonthId=1, Year=2022, SalaryReceived=32000, SystemSalaryId=1, TypeSalaryId=1, EmployeeId=employee.Id };
            db.Salaries.Add(salary);
            db.SaveChanges();
            var employee2 = db.Salaries.Include(p => p.Employee).FirstOrDefault(p => p.Id==salary.Id).Employee;
            Assert.Equal<Employee>(employee2, employee);
        }
    }
}