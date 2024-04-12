using Microsoft.EntityFrameworkCore;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using SalCalcWpf.Data;

namespace SalCalcWpf.Model
{
    public class Salary:ReactiveObject
    {
        [Reactive]
        public int Id { get; set; }
        [Reactive]
        public double SalaryReceived { get; set; }
        [Reactive]
        public Month Month { get; set; }
        [Reactive]
        public int MonthId { get; set; }
        [Reactive]
        public Employee.Employee Employee { get; set; }
        [Reactive]
        public int EmployeeId { get; set; }
        [Reactive]
        public TypeSalary TypeSalary { get; set; }
        [Reactive]
        public int TypeSalaryId { get; set; }
        [Reactive]
        public SystemSalary SystemSalary { get; set; }
        [Reactive]
        public int SystemSalaryId { get; set; }
        [Reactive]
        public int Year { get; set; }
        public void DelAll()
        {
            SystemSalary=null;
            TypeSalary=null;
            Employee=null;
            Month=null;
        }

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
        public ObservableCollection<T> GetDataList<T>()
        {
            return new ObservableCollection<T>( db.Salaries.Include(p=>p.TypeSalary).Include(p=>p.SystemSalary).Include(p=>p.Employee).Include(p=>p.Month).ToList() as List<T>);
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
                case ObservableCollection<Salary> list:
                    foreach (var item in list)
                        item.DelAll();
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
