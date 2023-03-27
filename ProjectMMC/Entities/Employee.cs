using ProjectMMC.Core.Interfaces;

namespace ProjectMMC.Core.Entities;

public class Employee : IEntity
{
    public int Id { get ; private set ; }
    public string Name { get ; set ; }
    public string SurName { get ; set ; }
    public decimal Salary { get ; set ; }
    private int count { get ; set ; }
    public int DepartmentId { get; set; }

    public Employee()
    {
        Id=count++;
    }

    public Employee(string name, string surName, decimal salary,int department_id):this() 
    {
        Name = name;
        SurName = surName;
        Salary = salary;
        DepartmentId = department_id;
    }
    public override string ToString()
    {
        return ($"Employee:{Id},{Name},{SurName}");
    }
}
