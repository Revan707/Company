using ProjectMMC.Core.Interfaces;

namespace ProjectMMC.Core.Entities;

public class Company:IEntity
{
    public int Id { get;private set; }
    public string Name { get; set; }
    private static int count { get; set; }
    public Company()
    { 
        Id = count++;
    }

    public Company(string name):this()
    {       
        Name = name;
    }
}
