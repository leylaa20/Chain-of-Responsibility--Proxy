using System;

namespace Chain_of_Responsibility;

class PurchaseOrder
{
    public int RequestNumber { get; set; }
    public double Amount { get; set; }
    public double Price { get; set; }
    public string Name { get; set; }

    public PurchaseOrder(int number, double amount, double price, string name)
    {
        RequestNumber = number;
        Amount = amount;
        Price = price;
        Name = name;

        Console.WriteLine("\nPurchase request for " + name + " (" + amount + " for $"
                          + price.ToString() + ") has been submitted");
    }
}

abstract class Approver
{
    protected Approver? Supervisor;

    public void SetSupervisor(Approver supervisor)
    {
        Supervisor = supervisor;
    }

    public abstract void ProcessRequest(PurchaseOrder purchase);
}

class HeadChef : Approver
{
    public override void ProcessRequest(PurchaseOrder purchase)
    {
        if (purchase.Price < 1000)
            Console.WriteLine($"{GetType().Name} approved purchase request #{purchase.RequestNumber}");

        else if (Supervisor != null)
            Supervisor.ProcessRequest(purchase);
    }
}

class PurchasingManager : Approver
{
    public override void ProcessRequest(PurchaseOrder purchase)
    {
        if (purchase.Price < 2000)
            Console.WriteLine($"{GetType().Name} approved purchase request #{purchase.RequestNumber}");

        else if (Supervisor != null)
            Supervisor.ProcessRequest(purchase);

    }
}

class GeneralManager : Approver
{
    public override void ProcessRequest(PurchaseOrder purchase)
    {
        if (purchase.Price < 10000)
            Console.WriteLine($"{GetType().Name} approved purchase request #{purchase.RequestNumber}");

        else
            Console.WriteLine($"Purchase request #{purchase.RequestNumber} requires an meeting");
    }
}

class Program
{
    static void Main(string[] args)
    {
        Approver leyla = new HeadChef();
        Approver aris = new PurchasingManager();
        Approver asel = new GeneralManager();

        // responsibility chain burda yaranir
        leyla.SetSupervisor(aris);
        aris.SetSupervisor(asel);

        PurchaseOrder p = new PurchaseOrder(1, 20, 69, "fruit");
        leyla.ProcessRequest(p);

        p = new PurchaseOrder(2, 300, 1389, "vegetable");
        leyla.ProcessRequest(p);

        p = new PurchaseOrder(3, 500, 5624, "beef");
        leyla.ProcessRequest(p);

        p = new PurchaseOrder(4, 4, 15198, "ovens");
        leyla.ProcessRequest(p);

        Console.ReadKey();
    }
}