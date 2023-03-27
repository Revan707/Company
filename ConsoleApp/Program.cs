using ProjectMMC.Core.Entities;
using ProjectMMC.Infrastructure.DBcontext;
using ProjectMMC.Infrastructure.Services;
using ProjectMMC.Infrastructure.Utilities.Exceptions;
using ProjectMMC.Infrastructure.Utilities.Helpers;
using System.Threading.Channels;
using System.Xml.Linq;

Console.WriteLine("Welcome");

EmployeeServices employeeServices = new();
DepartmentServices departmentServices = new();
CompanyServices companyServices = new();
while (true)
{
menu:
    Console.WriteLine("****************************");
    Console.WriteLine("0->Exit" +
        "\n1->Create Company" +
        "\n2->List Companies" +
        "\n3->Create Departments" +
        "\n4->List Departments" +
        "\n5->Create Employee" +
        "\n6->List Employee" +
        "\n7->GetAllDepartmentn" +
        "\n8->Update" +
        "\n9->Search Employee");
    Console.WriteLine("****************************");

    string? response = Console.ReadLine();
    int menu;
    bool tryToInt = int.TryParse(response, out menu);
    if (tryToInt)
    {
        if (menu >= 0 && menu <= 9)
        {
            switch (menu)
            {
                case (int)option.Exit:
                    Environment.Exit(0);
                    break;
                case (int)option.CompanyCreate:
                    Console.WriteLine("Enter Company name:");
                    try
                    {
                        string? res_companyname = Console.ReadLine();
                        companyServices.Creat(res_companyname);
                        Console.WriteLine($"New company is created:{res_companyname}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;
                case (int)option.ListCompany:
                    for (int i = 0; i < AppDBcontext.Companies.Length; i++)
                    {
                        if (AppDBcontext.Companies[i] is null)
                        {
                            Console.WriteLine("Not founds Companies");
                            goto case (int)option.DepartmentCreate;
                        }
                        else
                        {
                            break;
                        }
                    }
                    Console.WriteLine("Companies' List");
                    companyServices.GetAll();
                    break;
                case (int)option.DepartmentCreate:
                    for (int i = 0; i < AppDBcontext.Companies.Length; i++)
                    {
                        if (AppDBcontext.Companies[i] is null)
                        {
                            Console.WriteLine("Not found Company");
                            goto menu;
                        }
                        else
                        {
                            break;
                        }
                    }
                    Console.WriteLine("Enter Department Name:");
                    string? department_name = Console.ReadLine();
                employee_limit:
                    Console.WriteLine("Enter EmplyeeLimit Number:");
                    string? department_max = Console.ReadLine();
                    int employee_limit;
                    bool tryToIntLimit = int.TryParse(department_max, out employee_limit);
                    if (employee_limit < 0)
                    {
                        Console.WriteLine("Enter correct format");
                        goto employee_limit;
                    }
                    if (!tryToIntLimit)
                    {
                        Console.WriteLine("Enter correct Format");
                        goto employee_limit;
                    }
                select_company:
                    Console.WriteLine("Enter Company (Id):");
                    companyServices.GetAll();
                    string? select_company = Console.ReadLine();
                    int company_Id;
                    bool tryToIdCompany = int.TryParse(select_company, out company_Id);
                    if (!tryToIdCompany)
                    {
                        Console.WriteLine("Enter correct Company Id");
                        goto select_company;
                    }
                    try
                    {
                        departmentServices.Create(department_name, employee_limit, company_Id);
                        Console.WriteLine("Succesfully created");
                    }
                    catch (AddCompanyFailedException ex)
                    {
                        Console.WriteLine(ex.Message);
                        goto select_company;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        goto case (int)option.DepartmentCreate;
                    }
                    break;
                case (int)option.ListDepartment:
                    for (int i = 0; i < AppDBcontext.Departments.Length; i++)
                    {
                        if (AppDBcontext.Departments[i] is null)
                        {
                            Console.WriteLine("Not founds Department");
                            goto case (int)option.DepartmentCreate;
                        }
                        else
                        {
                            break;
                        }
                    }
                    Console.WriteLine("Departments' List");
                    departmentServices.GetAll();
                    break;


                case (int)option.EmployeeCreate:
                    for (int i = 0; i < AppDBcontext.Departments.Length; i++)
                    {
                        if (AppDBcontext.Departments[i] is null)
                        {
                            Console.WriteLine("Not found Department");
                            goto menu;
                        }
                        else
                        {
                            break;
                        }
                    }
                select_name:
                    Console.WriteLine("Enter Employee Name:");
                    string? employee_name = Console.ReadLine();
                    if (String.IsNullOrWhiteSpace(employee_name))
                    {
                        Console.WriteLine("Enter correct format");
                        goto select_name;
                    }
                select_surname:
                    Console.WriteLine("Enter Employee Surname:");
                    string? employee_surname = Console.ReadLine();
                    if (String.IsNullOrWhiteSpace(employee_surname))
                    {
                        Console.WriteLine("Enter correct format");
                        goto select_surname;
                    }
                select_salary:
                    Console.WriteLine("Enter salary");
                    string? value = Console.ReadLine();
                    decimal salary;
                    bool tryToSalary = decimal.TryParse(value, out salary);
                    if (!tryToSalary)
                    {
                        Console.WriteLine("Enter correct salary");
                        goto select_salary;
                    }
                    if (salary < 0)
                    {
                        Console.WriteLine("Enter correct salary");
                        goto select_salary;
                    }
                select_department:
                    departmentServices.GetAll();
                    Console.WriteLine("Enter Department (Id):");
                    string? select_department = Console.ReadLine();
                    int department_Id;
                    bool tryToIdDepartment = int.TryParse(select_department, out department_Id);
                    if (!tryToIdDepartment)
                    {
                        Console.WriteLine("Enter correct Department Id");
                        goto select_department;
                    }
                    try
                    {
                        employeeServices.Creat(employee_name, employee_surname, salary, department_Id);
                        Console.WriteLine("Succesfully created");
                    }
                    catch (AddDepartmentFailedException ex)
                    {
                        Console.WriteLine(ex.Message);
                        goto select_department;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        goto case (int)option.EmployeeCreate;
                    }
                    break;
                case (int)option.ListEmployee:
                    for (int i = 0; i < AppDBcontext.Employees.Length; i++)
                    {
                        if (AppDBcontext.Employees[i] is null)
                        {
                            Console.WriteLine("Not founds Employees");
                            goto case (int)option.DepartmentCreate;
                        }
                        else
                        {
                            break;
                        }
                    }
                    Console.WriteLine("Employees' List");
                    employeeServices.GetAll();
                    break;
                case (int)option.GetAllDepartment:
                    Console.WriteLine("Enter name:");
                    string? searchname = Console.ReadLine();
                    try
                    {
                        companyServices.GetAllDepartments(searchname);
                    }
                    catch (ArgumentNullException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    catch (NotFoundException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;
                case (int)option.Update:
                select_update:
                    departmentServices.GetAll();
                    Console.WriteLine("Enter Update");
                    string? update = Console.ReadLine();
                    int update_id;
                    bool tryToUpdateId = int.TryParse(update, out update_id);
                    if (!tryToUpdateId)
                    {
                        Console.WriteLine("Enter correct format");
                        goto select_update;
                    }
                    if (update_id < 0)
                    {
                        Console.WriteLine("Enter correct format");
                        goto select_update;
                    }
                    Console.WriteLine("Enter Department name");
                    string? new_name = Console.ReadLine();
                select_employeerLimit:
                    Console.WriteLine("Enter EmployeerLimit");
                    string? employeer_limit = Console.ReadLine();
                    int Employeer_limit;
                    bool tryToEmployeerLimit = int.TryParse(employeer_limit, out Employeer_limit);
                    if (!tryToEmployeerLimit)
                    {
                        Console.WriteLine("Enter corrrect choice");
                        goto select_employeerLimit;
                    }
                    try
                    {
                        departmentServices.Update(update_id, new_name, Employeer_limit);
                        Console.WriteLine("Succesfully change");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        goto case (int)option.Update;
                    }
                    break;
                case (int)option.SearchEmployee:
                    Console.WriteLine("Enter name:");
                    string? nameOrSurname = Console.ReadLine();
                    try
                    {
                        employeeServices.GetByName(nameOrSurname);
                    }
                    catch (ArgumentNullException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    catch (NotFoundException ex)
                    {
                        Console.WriteLine(ex.Message);
                        goto menu;
                    }
                    break;
                default:
                    Console.WriteLine("Select correct ones from menu!!");
                    break;
            }
        }
        else
        {
            Console.WriteLine("Enter correct menu");
        }
    }
}

