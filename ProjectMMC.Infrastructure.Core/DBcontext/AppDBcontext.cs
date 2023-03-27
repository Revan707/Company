using ProjectMMC.Core.Entities;

namespace ProjectMMC.Infrastructure.DBcontext;

public static class AppDBcontext
{
    public static Employee[] Employees { get; set; } = new Employee[1000];
    public static Department[] Departments { get; set; }= new Department[100];
    public static Company[] Companies { get; set; }=new Company[50];
}
