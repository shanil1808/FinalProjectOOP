using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    class DeliverySystem : IFileHandler, ISortable
    {
        public List<Warehouse> warehouses = new List<Warehouse>();
        public List<Package> allPackages = new List<Package>();

        // Undo system
        public CustomStack<Package> undoStack = new CustomStack<Package>();

        public void AddWarehouse(Warehouse w)
        {
            warehouses.Add(w);
        }

        public void AddPackage(Package p)
        {
            allPackages.Add(p);
            undoStack.Push(p); // save for undo
        }

        // Linear Search
        public Package SearchPackageById(int id)
        {
            foreach (var p in allPackages)
            {
                if (p.id == id)
                    return p;
            }
            return null;
        }

        // Bubble Sort 
        public void Sort()
        {
            for (int i = 0; i < allPackages.Count - 1; i++)
            {
                for (int j = 0; j < allPackages.Count - i - 1; j++)
                {
                    if (allPackages[j].CalculatePriorityScore() <
                        allPackages[j + 1].CalculatePriorityScore())
                    {
                        var temp = allPackages[j];
                        allPackages[j] = allPackages[j + 1];
                        allPackages[j + 1] = temp;
                    }
                }
            }
        }

        // Delivery process
        public void ProcessDeliveries()
        {
            foreach (var warehouse in warehouses)
            {
                while (!warehouse.waitingQueue.IsEmpty())
                {
                    Package p = warehouse.waitingQueue.Dequeue();

                    Vehicle v = warehouse.FindBestVehicle(p);
                    Worker w = warehouse.AssignWorker();

                    if (v == null)
                        throw new OverCapacityException("No vehicle available");

                    if (w == null)
                        throw new InvalidDataException("No worker available");

                    p.UpdateStatus("Delivered");

                    Console.WriteLine("Delivered package " + p.id);
                }
            }
        }

        public void SimulateDay()
        {
            Console.WriteLine("---- Simulation Start ----");

            Sort();
            ProcessDeliveries();

            Console.WriteLine("---- Simulation End ----");
        }

        // SAVE
        public void Save(string path)
        {
            using (StreamWriter sw = new StreamWriter(path))
            {
                foreach (var p in allPackages)
                {
                    sw.WriteLine($"PACKAGE|{p.id}|{p.weight}|{p.priorityLevel}|{p.destination}|{p.status}");
                }
            }
        }

        // LOAD
        public void Load(string path)
        {
            if (!File.Exists(path))
                return;

            string[] lines = File.ReadAllLines(path);

            foreach (var line in lines)
            {
                string[] parts = line.Split('|');

                if (parts[0] == "PACKAGE")
                {
                    Package p = new Package
                    {
                        id = int.Parse(parts[1]),
                        weight = double.Parse(parts[2]),
                        priorityLevel = int.Parse(parts[3]),
                        destination = parts[4],
                        status = parts[5]
                    };

                    allPackages.Add(p);
                }
            }
        }

        // Undo
        public void Undo()
        {
            if (undoStack.IsEmpty())
                throw new EmptyStructureException("Nothing to undo");

            Package p = undoStack.Pop();
            allPackages.Remove(p);

            Console.WriteLine("Undo last package");
        }
    }
}
