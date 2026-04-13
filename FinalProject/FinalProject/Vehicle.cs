using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    public abstract class Vehicle : Entity
    {
        protected double speed;
        protected double maxCapacity;
        protected double currentLoad;
        protected bool isAvaiable;

        //Constructor to initialize the vehicle's properties
        public Vehicle(double speed, double maxCapacity, int id, string name, DateTime createdDate) 
            :base(id, name, createdDate)
        {
            this.speed = speed;
            this.maxCapacity = maxCapacity;
            this.currentLoad = 0;
            this.isAvaiable = true;
        }

        //Getters and setters for the vehicle's properties
        public double GetSpeed() { return this.speed; }

        public double GetMaxCapacity() { return this.maxCapacity; }

        public double GetCurrentLoad() { return this.currentLoad; }

        public bool IsAvaiable() { return this.isAvaiable; } 

        public void SetSpeed(double speed) 
        { 
            this.speed = speed;
        }
        public void SetMaxCapacity(double maxCapacity) 
        { 
            this.maxCapacity = maxCapacity;
        }
        public void SetCurrentLoad(double currentLoad) 
        {
            if (currentLoad < 0)
                throw new ArgumentOutOfRangeException("Load cannot be negative");

            this.currentLoad = currentLoad;
        }
        public void SetIsAvaiable(bool isAvaiable) 
        {  
            this.isAvaiable = isAvaiable;
        }

        // Method to set the maximum capacity of the vehicle
        public void SetCapacity(double capacity) 
        {
            if (capacity <= 0) // Ensure that the capacity is a positive value
            {
                throw new ArgumentOutOfRangeException("capacity must be greater than 0"); // Throw an exception if the capacity is invalid
            }
            this.maxCapacity = capacity; // Set the maximum capacity of the vehicle
        }

        // Method to calculate the remaining capacity of the vehicle
        public double GetRemainingCapacity() 
        {
            return this.maxCapacity - this.currentLoad;
        }

        // Method to calculate the efficiency of the vehicle based on its speed and current load
        public virtual double CalculateEfficiency() 
        {
            if (currentLoad == 0)
                return speed; // avoids divide by 0

            return speed / currentLoad;
        }
      public abstract void Deliver(List<Package> packages);

    }
}
