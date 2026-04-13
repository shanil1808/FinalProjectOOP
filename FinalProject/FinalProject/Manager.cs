using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    public abstract class Manager : Worker
    {
        private int teamSize;

        public Manager(int exp, int id, string name, DateTime date, int teamSize)
            : base(exp, id, name, date)
        {
            this.teamSize = teamSize;
        }

        public override void PerformTask()
        {
            System.Console.WriteLine("Managing logistics tasks...");
        }

        public Worker FindBestWorker(List<Worker> workers)
        {
            Worker best = null;
            double bestScore = -1;

            foreach (var w in workers)
            {
                if (w.GetIsAvailable())
                {
                    double score = w.CalculatePerformance();

                    if (score > bestScore)
                    {
                        bestScore = score;
                        best = w;
                    }
                }
            }

            return best;
        }
    }
}
