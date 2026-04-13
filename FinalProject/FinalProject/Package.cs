using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    internal class Package
    {
        public int id;
        public double weight;
        public int priorityLevel; // 1–5
        public string destination;
        public string status; // Pending / Assigned / Delivered

        // Calculate priority (simple formula)
        public double CalculatePriorityScore()
        {
            // higher priority + heavier = more important
            return priorityLevel * 10 + weight;
        }

        public void UpdateStatus(string newStatus)
        {
            status = newStatus;
        }

        public bool IsHeavy()
        {
            return weight > 10; // threshold
        }
    }
}

