namespace Proxy;

public interface ISharedFolder
{
    void PerformRWOperations();
}

public class SharedFolder : ISharedFolder
{
    public void PerformRWOperations()
    {
        Console.WriteLine("Performing Read Write operation on the Shared Folder");
    }
}

public class Employee
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }

    public Employee(string username, string password, string role)
    {
        Username = username;
        Password = password;
        Role = role;
    }
}

class SharedFolderProxy : ISharedFolder
{
    private ISharedFolder _folder;
    private Employee _employee;
    public SharedFolderProxy(Employee emp)
    {
        _employee = emp;
    }

    public void PerformRWOperations()
    {
        if (_employee.Role.ToUpper() == "CEO" || _employee.Role.ToUpper() == "MANAGER")
        {
            _folder = new SharedFolder();
            Console.WriteLine("PerformRWOperations method");
            _folder.PerformRWOperations();
        }

        else Console.WriteLine("You don't have permission to access this folder");
    }
}


class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Client passing employee with Role Developer to folderproxy...");

        Employee emp1 = new("Aris", "Aris123", "Developer");
        SharedFolderProxy folderProxy1 = new(emp1);

        folderProxy1.PerformRWOperations();

        Console.WriteLine("\nClient passing employee with Role Manager to folderproxy...");

        Employee emp2 = new("Asel", "Asel123", "Manager");
        SharedFolderProxy folderProxy2 = new(emp2);

        folderProxy2.PerformRWOperations();
    }
}