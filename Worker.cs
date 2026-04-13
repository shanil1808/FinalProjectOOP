using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    public abstract class Worker : Entity
    {
        private int experienceYears;
        private int tasksCompleted;
        private bool isAvailable;

        //Constructor to initialize the worker's properties
        public Worker(int experienceYears, int id, string name, DateTime createdDate) 
            : base(id, name, createdDate)
        {
            this.experienceYears = experienceYears;
            this.tasksCompleted = 0;
            this.isAvailable = true;
        }

        //Getters and setters for the worker's properties
        public int GetExperienceYears() {  return experienceYears; }

        public int GetTasksCompleted() { return tasksCompleted; }

        public bool GetIsAvailable() { return isAvailable; } 

        public void SetExperienceYears(int experienceYears) 
        { 
            this.experienceYears = experienceYears;
        }
        public void SetTasksCompleted(int tasksCompleted) 
        { 
            this.tasksCompleted = tasksCompleted;
        }
        public void SetIsAvailable(bool isAvailable)
        {
            this.isAvailable = isAvailable;
        }

        // Method to increment the number of tasks completed by the worker
        public void AddTask() 
        { 
            this.tasksCompleted++;
        }

        //calculates the worker's performance based on the number of tasks completed and years of experience
        public virtual double CalculatePerformance()
        {
            return this.tasksCompleted + (this.experienceYears * 2); 
        }
        public abstract void PerformTask();
    }
}
