using FinalProject;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Logic system = new Logic();
            Warehouse warehouse = new Warehouse();
            system.warehouses.Add(warehouse);

            while (true)
            {
                Console.WriteLine("\n--- MENU ---");
                Console.WriteLine("1. Add Package");
                Console.WriteLine("2. Assign Deliveries");
                Console.WriteLine("3. Sort Packages");
                Console.WriteLine("4. Search Package");
                Console.WriteLine("5. Run Simulation");
                Console.WriteLine("6. Undo");
                Console.WriteLine("7. Save");
                Console.WriteLine("8. Load");
                Console.WriteLine("0. Exit");

                int choice = int.Parse(Console.ReadLine());

                if (choice == 1)
                {
                    Package p = new Package();

                    Console.Write("ID: ");
                    p.id = int.Parse(Console.ReadLine());

                    Console.Write("Weight: ");
                    p.weight = double.Parse(Console.ReadLine());

                    Console.Write("Priority: ");
                    p.priorityLevel = int.Parse(Console.ReadLine());

                    Console.Write("Destination: ");
                    p.destination = Console.ReadLine();

                    p.status = "Pending";

                    warehouse.AddPackage(p);
                    system.allPackages.Add(p);
                    system.undoStack.Push(p);
                }
                else if (choice == 2)
                {
                    system.RunSimulation();
                }
                else if (choice == 3)
                {
                    system.SortPackages();
                }
                else if (choice == 4)
                {
                    Console.Write("Enter ID: ");
                    int id = int.Parse(Console.ReadLine());

                    var result = system.Search(id);
                    Console.WriteLine(result != null ? "Found" : "Not found");
                }
                else if (choice == 5)
                {
                    system.RunSimulation();
                }
                else if (choice == 6)
                {
                    system.Undo();
                }
                else if (choice == 7)
                {
                    system.Save("data.txt");
                }
                else if (choice == 8)
                {
                    system.Load("data.txt");
                }
                else if (choice == 0)
                {
                    break;
                }
            }
        }
    }
}

// EXCEPTIONS
class InvalidDataException : Exception
{
    public InvalidDataException(string msg) : base(msg) { }
}

class OverCapacityException : Exception
{
    public OverCapacityException(string msg) : base(msg) { }
}

class EmptyStructureException : Exception
{
    public EmptyStructureException(string msg) : base(msg) { }
}

// ALGORITHMS CLASS

class Logic
{
    public List<Package> allPackages = new List<Package>();
    public List<Warehouse> warehouses = new List<Warehouse>();

    public CustomStack<Package> undoStack = new CustomStack<Package>();

    // PRIORITY CALCULATION
    public double CalculatePriority(Package p)
    {
        return p.priorityLevel * 10 + p.weight;
    }

    // VEHICLE SELECTION
    public Vehicle SelectVehicle(Warehouse w, Package p)
    {
        Vehicle best = null;
        double bestEff = -1;

        foreach (var v in w.vehicles)
        {
            if (v.GetRemainingCapacity() >= p.weight)
            {
                double eff = v.CalculateEfficiency();

                if (eff > bestEff)
                {
                    bestEff = eff;
                    best = v;
                }
            }
        }

        return best;
    }

    // WORKER SELECTION
    public Worker SelectWorker(Warehouse w)
    {
        Worker best = null;
        double bestPerf = -1;

        foreach (var worker in w.workers)
        {
            if (worker.GetIsAvailable())
            {
                double perf = worker.CalculatePerformance();

                if (perf > bestPerf)
                {
                    bestPerf = perf;
                    best = worker;
                }
            }
        }

        return best;
    }

    // SORT (BUBBLE SORT)
    public void SortPackages()
    {
        for (int i = 0; i < allPackages.Count - 1; i++)
        {
            for (int j = 0; j < allPackages.Count - i - 1; j++)
            {
                if (CalculatePriority(allPackages[j]) <
                    CalculatePriority(allPackages[j + 1]))
                {
                    Package temp = allPackages[j];
                    allPackages[j] = allPackages[j + 1];
                    allPackages[j + 1] = temp;
                }
            }
        }
    }

    // SEARCH (LINEAR)
    public Package Search(int id)
    {
        foreach (var p in allPackages)
        {
            if (p.id == id)
                return p;
        }
        return null;
    }

    // SIMULATION
    public void RunSimulation()
    {
        Console.WriteLine("Simulation started");

        foreach (var w in warehouses)
        {
            while (!w.waitingQueue.IsEmpty())
            {
                Package p = w.waitingQueue.Dequeue();

                Vehicle v = SelectVehicle(w, p);
                Worker worker = SelectWorker(w);

                if (v == null)
                    throw new OverCapacityException("No vehicle");

                if (worker == null)
                    throw new InvalidDataException("No worker");

                p.status = "Delivered";

                Console.WriteLine("Delivered package " + p.id);
            }
        }

        Console.WriteLine("Simulation finished");
    }

    // SAVE FILE
    public void Save(string path)
    {
        using (StreamWriter sw = new StreamWriter(path))
        {
            foreach (var p in allPackages)
            {
                sw.WriteLine("PACKAGE|" + p.id + "|" + p.weight + "|" +
                             p.priorityLevel + "|" + p.destination + "|" + p.status);
            }
        }
    }

    // LOAD FILE
    public void Load(string path)
    {
        if (!File.Exists(path)) return;

        string[] lines = File.ReadAllLines(path);

        foreach (var line in lines)
        {
            string[] parts = line.Split('|');

            if (parts[0] == "PACKAGE")
            {
                Package p = new Package();
                p.id = int.Parse(parts[1]);
                p.weight = double.Parse(parts[2]);
                p.priorityLevel = int.Parse(parts[3]);
                p.destination = parts[4];
                p.status = parts[5];

                allPackages.Add(p);
            }
        }
    }

    // UNDO
    public void Undo()
    {
        if (undoStack.IsEmpty())
            throw new EmptyStructureException("Nothing to undo");

        Package last = undoStack.Pop();
        allPackages.Remove(last);

        Console.WriteLine("Undo done");
    }
}

