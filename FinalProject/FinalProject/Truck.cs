using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    public abstract class Truck : Vehicle
    {
        private double fuelConsumption;

        public Truck(double speed, double maxCapacity, int id, string name, DateTime createdDate, double fuelConsumption)
            : base(speed, maxCapacity, id, name, createdDate)
        {
            this.fuelConsumption = fuelConsumption;
        }

        public override double CalculateEfficiency()
        {
            return GetSpeed() / (fuelConsumption + GetCurrentLoad());
        }

        public override void Deliver(List<Package> packages)
        {
            foreach (var p in packages)
            {
                if (p.weight > GetRemainingCapacity())
                    throw new OverCapacityException("Truck overloaded");

                SetCurrentLoad(GetCurrentLoad() + p.weight);
                p.UpdateStatus("Delivered");
            }
        }
    }
}
