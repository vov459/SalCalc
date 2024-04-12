using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace SalCalc.Models;

public sealed class ConnectDB : DbContext
{
    public DbSet<Employee> Employee { get; set; }
    public DbSet<Month> Month { get; set; }
    public DbSet<SystemSalary> SystemSalaries { get; set; }
    public DbSet<TypeSalary> TypeSalaries { get; set; }
    public  DbSet<Salary> Salaries { get; set; }
    public ConnectDB()
    {
       //Database.EnsureDeleted();
        Database.EnsureCreated();
    }
    public ConnectDB(DbContextOptions<ConnectDB> options)
        : base(options) { }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
            // optionsBuilder.UseSqlServer(@"Server=localhost\SQLEXPRESS;Database=SelCalc;Trusted_Connection=True;");
            //optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=SelCalc;Trusted_Connection=True;");
           optionsBuilder.UseSqlServer(@"Server=localhost,1433;Database=SelCalc;User Id=sa;Password=***********;");
            



    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Month>().HasData(new List<Month> { new Month { Id = 1, Name = "Январь"},new Month { Id = 2, Name = "Февраль"},new Month { Id = 3, Name = "Март"},new Month { Id = 4, Name = "Апрель"},new Month { Id = 5, Name = "Май"},new Month { Id = 6, Name = "Июнь"},new Month { Id = 7, Name = "Июль"},new Month { Id = 8, Name = "Август"},new Month { Id = 9, Name = "Сентябрь"},new Month { Id = 10, Name = "Октябрь"},new Month { Id = 11, Name = "Ноябрь"},new Month { Id = 12, Name = "Декабрь"} });
        modelBuilder.Entity<SystemSalary>().HasData(new List<SystemSalary> { new SystemSalary{Id = 1, Type = "Повременная"},new SystemSalary{Id = 2, Type = "Сдельная"},new SystemSalary{Id = 3, Type = "Комиссионная"},new SystemSalary{Id = 4, Type = "Аккордная"},new SystemSalary{Id = 5, Type = "Система плавающих окладов"} });
        modelBuilder.Entity<TypeSalary>().HasData(new List<TypeSalary> { new TypeSalary{Id = 1, Type = "Денежная форма"},new TypeSalary{Id = 2, Type = "Неденежная форма"} });

    }
}