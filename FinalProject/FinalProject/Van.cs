using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    public abstract class Van : Vehicle
    {
        private bool isElectric;

        public Van(double speed, double maxCapacity, int id, string name, DateTime createdDate, bool isElectric)
            : base(speed, maxCapacity, id, name, createdDate)
        {
            this.isElectric = isElectric;
        }

        public override void Deliver(List<Package> packages)
        {
            foreach (var p in packages)
            {
                if (p.weight > GetRemainingCapacity())
                    throw new OverCapacityException("Van overloaded");

                SetCurrentLoad(GetCurrentLoad() + p.weight);
                p.UpdateStatus("Delivered");
            }
        }

        public override double CalculateEfficiency()
        {
            return isElectric ? GetSpeed() * 1.2 : GetSpeed();
        }
    }
}
