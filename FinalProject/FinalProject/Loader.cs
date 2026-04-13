using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    public abstract class Loader : Worker
    {
        private double maxLiftWeight;

        public Loader(int exp, int id, string name, DateTime date, double maxLiftWeight)
            : base(exp, id, name, date)
        {
            this.maxLiftWeight = maxLiftWeight;
        }

        public override void PerformTask()
        {
            System.Console.WriteLine("Loading/unloading packages...");
        }
    }
}
