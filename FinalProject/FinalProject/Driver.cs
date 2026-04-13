using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    public abstract class Driver : Worker
    {
        private string licenseType;

        public Driver(int exp, int id, string name, DateTime date, string licenseType)
            : base(exp, id, name, date)
        {
            this.licenseType = licenseType;
        }

        public override void PerformTask()
        {
            System.Console.WriteLine("Driver delivering packages...");
        }
    }
}
