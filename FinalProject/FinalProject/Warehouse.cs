using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    class Warehouse
    {
        public string name;

        public List<Package> packages = new List<Package>();
        public List<Vehicle> vehicles = new List<Vehicle>();
        public List<Worker> workers = new List<Worker>();

        // Queue for waiting packages
        public CustomQueue<Package> waitingQueue = new CustomQueue<Package>();

        public void AddPackage(Package p)
        {
            packages.Add(p);
            waitingQueue.Enqueue(p); // required usage
        }

        public void RemovePackage(int packageId)
        {
            packages.RemoveAll(p => p.id == packageId);
        }

        // Vehicle selection algorithm
        public Vehicle FindBestVehicle(Package p)
        {
            Vehicle best = null;
            double bestEfficiency = -1;

            foreach (var v in vehicles)
            {
                if (v.GetRemainingCapacity() >= p.weight)
                {
                    double efficiency = v.CalculateEfficiency();

                    if (efficiency > bestEfficiency)
                    {
                        bestEfficiency = efficiency;
                        best = v;
                    }
                }
            }

            return best;
        }

        // Worker selection
        public Worker AssignWorker()
        {
            Worker best = null;
            double bestPerf = -1;

            foreach (var w in workers)
            {
                if (w.GetIsAvailable())
                {
                    double perf = w.CalculatePerformance();

                    if (perf > bestPerf)
                    {
                        bestPerf = perf;
                        best = w;
                    }
                }
            }

            return best;
        }

        public List<Package> GetPendingPackages()
        {
            return packages.FindAll(p => p.status == "Pending");
        }
    }
}
