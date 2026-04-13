using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    public abstract class Drone : Vehicle
    {
        private double maxDistance;

        public Drone(double speed, double maxCapacity, int id, string name, DateTime createdDate, double maxDistance)
            : base(speed, maxCapacity, id, name, createdDate)
        {
            this.maxDistance = maxDistance;
        }

        public override void Deliver(List<Package> packages)
        {
            foreach (var p in packages)
            {
                if (p.weight > 5)
                    throw new InvalidDataException("Drone cannot carry heavy package");

                SetCurrentLoad(GetCurrentLoad() + p.weight);
                p.UpdateStatus("Delivered");
            }
        }

        public override double CalculateEfficiency()
        {
            return GetSpeed() / maxDistance;
        }
    }
}
