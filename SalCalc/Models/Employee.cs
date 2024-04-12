using System.Collections.Generic;
using System.Net.Mail;
using System.Numerics;
using System.Text.RegularExpressions;

namespace SalCalc.Models
{

    

    public  class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string LastName { get; set; }
        public string Position { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public  string GetFio() => $"{FirstName} {SecondName} {LastName}";
        public bool VarEmpPhone()//функция проверки правильности введеного номера телефона номера телефона
        {

            try
            {
                if (string.IsNullOrEmpty(PhoneNumber))
                    return false;
                if (PhoneNumber.Length<11)//если чисел в номере меньше 11, то проверку не проходит
                {
                    return false;
                 
                }
                if (PhoneNumber[0]=='+' && PhoneNumber.Length<12)// если номер начинается с + и цифр в нем меньше 12 то проверку непроходит
                {
                    return false;
                }
                var r = new Regex(@"\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})");//регулярное выражение правильности номера телефона

                return r.IsMatch(PhoneNumber);
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool VarEmpEmail()//фунция корректности введеного email
        {
            if (string.IsNullOrEmpty(Email))
            {
                return false;
            }
            if (!Email.Contains("."))
            {
                return false;
            }
            try
            {

                MailAddress m = new MailAddress(Email);//Создает экземпляр email
                return true;//если email корректный дает true

            }
            catch (FormatException)
            {
              return false;//если email некорректный вызывается false

            }
           
        }
        public bool VarEmpName()
        {
            if (!string.IsNullOrEmpty(FirstName) &&  !string.IsNullOrEmpty(LastName) && !string.IsNullOrEmpty(SecondName))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public string IsVarEmployee()
        {
            if (!VarEmpEmail())
                return "Введенный email неверен";
            if(!VarEmpPhone())
                return "Введенный номер неверен";
            if(!VarEmpName())
                return "Введите корректное ФИО";
            return "";
        }
    }
    //Реализация паттерна комманда для пользователей
    public class EmployeeCommand<TL> : ICommand
    {
        private TL employee;
        public EmployeeCommand(TL employee):this()
        {
            this.employee=employee;
        }
        private ConnectDB db;
        public EmployeeCommand()
        { 
            db = new ConnectDB();
        }

        public bool Add()
        {
            if (employee is int)
                return false;
            db.Add(employee);
            db.SaveChanges();
            return true;
        } 
        //В зависимтости от переданного команде типа, удаляем пользователя из базы (в данном случае если передаем id в int то удаляем по id)
        public bool Remove()
        {
            switch (employee)
            {
                case int id:
                    var emp = db.Employee.FirstOrDefault(p => p.Id == id);
                    if (emp != null) db.Employee.Remove(emp);
                    db.SaveChanges();
                    break;
                default:
                    return false;
            }

            return true;
        }
        //В зависимости от того передали ли список пользовтелей или пользователя обновляем данные в бд
        public bool Update()
        {
            switch (employee)
            {
                case List<Employee> list:
                    db.Employee.UpdateRange(list);
                    break;
                case Employee _employee:
                    db.Employee.Update(_employee);
                    break;
                default:
                    return false;
            }

            db.SaveChanges();
            return true;
        }

        //Получаем список пользователей
        public List<T> GetDataList<T>()
        {
            return db.Employee.ToList() as List<T>;
        }
        //Получаем пользователей по переданному id
        public T GetData<T>()
        {
            switch (employee)
            {
                case int id:
                    var emp = db.Employee.FirstOrDefault(p => p.Id == id);
                    if (emp != null) return (T)(object)emp ;
                    break;
            }
            return default(T);
            
        }
    }
}
