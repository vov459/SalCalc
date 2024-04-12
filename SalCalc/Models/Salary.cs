using Microsoft.EntityFrameworkCore;

namespace SalCalc.Models
{
    public class Salary
    {

        public int Id { get; set; }
        public double SalaryReceived { get; set; }
        public Month Month { get; set; }
        public int MonthId { get; set; }
        public Employee Employee { get; set; }
        public int EmployeeId { get; set; }
        public TypeSalary TypeSalary { get; set; }
        public int TypeSalaryId { get; set; }
        public SystemSalary SystemSalary { get; set; }
        public int SystemSalaryId { get; set; }
        public int Year { get; set; }
        

    }
    //Реализация паттерна комманда для зарплаты, работает абсолютно аналогично пользователям
    public class SalaryCommand<TL> : ICommand
    {
        private TL salary;
        public SalaryCommand(TL salary):this()
        {
            this.salary=salary;
        }
        private ConnectDB db;
        public SalaryCommand()
        { 
            db = new ConnectDB();
        }
        public bool Add()
        {
            db.Add(salary);
            db.SaveChanges();
            return true;
        }
        public List<T> GetDataList<T>()
        {
            return db.Salaries.Include(p=>p.TypeSalary).Include(p=>p.SystemSalary).Include(p=>p.Employee).Include(p=>p.Month).ToList() as List<T>;
        }
        public T GetData<T>()
        {
            throw new NotImplementedException();
        }
        public bool Remove()
        {
            switch (salary)
            {
                case int id:
                    var sal = db.Salaries.FirstOrDefault(p => p.Id == id);
                    if (sal != null) db.Salaries.Remove(sal);
                    db.SaveChanges();
                    break;
                default:
                    return false;
            }

            return true;
        }
        public bool Update()
        {
            switch (salary)
            {
                case List<Salary> list:
                    db.Salaries.UpdateRange(list);
                    break;
                case Salary _salary:
                    db.Salaries.UpdateRange(_salary);
                    break;
                default:
                    return false;
            }

            db.SaveChanges();
            return true;
        }
    }
    public class SystemSalary
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public static List<SystemSalary> GetSystemSalary()
        {
            using var db = new ConnectDB();
            return db.SystemSalaries.ToList();
        }
    }
    public class TypeSalary
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public static List<TypeSalary> GetTypeSalary()
        {
            using var db = new ConnectDB();
            return db.TypeSalaries.ToList();
        }
    }
    public class Month
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public static List<Month> GetMonth()
        {
            using var db = new ConnectDB();
            return db.Month.ToList();
        }
    }
}
