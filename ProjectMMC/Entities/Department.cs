using ProjectMMC.Core.Interfaces;

namespace ProjectMMC.Core.Entities;

public class Department : IEntity
{
    public int Id { get; private set; }
    public string Name { get ; set; }
    public int EmployeeLimit { get; set; }
    public int CompanyId { get; set; }
    private static int count { get; set; }
    public Department()
    {
        Id = count++;
    }

    public Department(string name, int employee_limit, int companyId):this() 
    {
        Name = name;
        EmployeeLimit = employee_limit;
        CompanyId = companyId;
    }
    public override string ToString()
    {
        return ($"Department:{Id},{Name}");
    }

}
