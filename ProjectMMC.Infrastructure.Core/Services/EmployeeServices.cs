using ProjectMMC.Core.Entities;
using ProjectMMC.Infrastructure.DBcontext;
using ProjectMMC.Infrastructure.Utilities.Exceptions;
using System;

namespace ProjectMMC.Infrastructure.Services;

public class EmployeeServices
{
    private static int index_count = 0;
    private DepartmentServices departmentServices;
    public void Creat(string name, string surname, decimal salary, int department_id)
    {
        foreach (var departments in AppDBcontext.Departments)
        {
            if (departments == null)
            {
                throw new NotFoundException("This department is not exist");
            }
            if (departments.Id == department_id) break;
        }   
        Employee new_employee = new(name, surname, salary,department_id);
        AppDBcontext.Employees[index_count++] = new_employee;
    }
   

    public void GetAll()
    {
        for (int i = 0; i < index_count; i++)
        {
            Console.WriteLine("\n************************************************************************\n");
            Console.WriteLine($"EmployeeId:{AppDBcontext.Employees[i].Id}," +
                $"EmployeeName:{AppDBcontext.Employees[i].Name}," +
                $"EmployeeSurname:{AppDBcontext.Employees[i].SurName}," +
                $"EmployeeSalary:{AppDBcontext.Employees[i].Salary},");
            Console.WriteLine("\n************************************************************************\n");
        }
    }
    public void GetByName(string nameOrSurname)
    {
        if (String.IsNullOrEmpty(nameOrSurname))
        {
            throw new ArgumentNullException();
        }

        bool isExsist = false;
        string fullname = String.Empty;
        for (int i = 0; i < index_count; i++)
        {
            fullname = AppDBcontext.Employees[i].Name + " " + AppDBcontext.Employees[i].SurName;

            if (fullname.ToUpper().Contains(nameOrSurname.ToUpper()))
            {
                isExsist = true;
                Console.WriteLine("\n************************************************************************\n");
                Console.WriteLine($"Employee id: {AppDBcontext.Employees[i].Id}; " +
                    $"Employee Name: {AppDBcontext.Employees[i].Name}; " +
                    $"Employee Surname: {AppDBcontext.Employees[i].SurName};");
                departmentServices.GetById(AppDBcontext.Employees[i].DepartmentId);
                Console.WriteLine("\n************************************************************************\n");
            }
        }

        if (!isExsist)
        {
            throw new NotFoundException("Not found Employee");
        }
    }
    
}

