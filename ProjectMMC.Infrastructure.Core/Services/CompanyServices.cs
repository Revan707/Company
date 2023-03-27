using ProjectMMC.Core.Entities;
using ProjectMMC.Infrastructure.DBcontext;
using ProjectMMC.Infrastructure.Utilities.Exceptions;
using System;
using System.Data;

namespace ProjectMMC.Infrastructure.Services;

public class CompanyServices
{
    private static int index_counter = 0;
    public void Creat(string? name)
    {
        if (String.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentNullException();
        }
        bool isExist = false;
        for (int i = 0; i < index_counter; i++)
        {
            if (AppDBcontext.Companies[i].Name.ToUpper() == name.ToUpper())
            {
                isExist = true;
                break;
            }
        }
        if (isExist)
        {
            throw new DuplicateNameException("This company already exist");
        }
        Company new_company = new (name);
        AppDBcontext.Companies[index_counter++] = new_company;
    }
    public void GetAll()
    {
        for (int i = 0;i < index_counter;i++)
        {
            Console.WriteLine($"Id:{AppDBcontext.Companies[i].Id}->Name:{AppDBcontext.Companies[i].Name}");
        }
    }
    public void GetAllDepartments(string searchname)
    {
        for (int i = 0; i < AppDBcontext.Companies.Length; i++)
        {
            if (AppDBcontext.Companies[i].Name.Equals(searchname))
            {
                foreach (var department in AppDBcontext.Departments)
                {
                    if (department == null) break;
                    if (department.CompanyId == AppDBcontext.Companies[i].Id)
                    {
                        Console.WriteLine(department.Name);
                    }
                }
                break;
            }
            else
            {
                throw new NotFoundException("Not found company");
            }
        }     
    }

}




