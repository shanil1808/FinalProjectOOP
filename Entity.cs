using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    public abstract class Entity
    {
        protected int id;
        protected string name;
        protected DateTime createdDate;

        //Constructor to initialize the entity's properties
        public Entity(int id, string name, DateTime createdDate) 
        {
            this.id = id;
            this.name = name;
            this.createdDate = createdDate;
        }

        //Getters and setters for the entity's properties
        public int GetId() { return id; } 

        public string GetName() { return name; } 

        public DateTime GetCreatedDate() { return createdDate; } 

        public void SetId(int id) { this.id = id; }

        public void SetName(string name) 
        {
            if (name == null) throw new Exception("name"); // Ensure that the name is not null
            this.name = name;
        }

        public void SetCreatedDate(DateTime dateTime) 
        { 
            this.createdDate = dateTime; //sets the date and time when the entity was created
        }

        // Validates the entity's properties
        public virtual bool Validate() 
        {
            if (name == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        // Abstract method to display the entity's information, to be implemented by derived classes
        public abstract void Display(); 
    }
}
