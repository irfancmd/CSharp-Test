using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP.Basics
{
    internal class EncapsulationExample
    {
        // Uncess necessary, keep fields private
        private string name;

        // Private fields can be accessed by getters and setter methods
        public string getName () 
        {  
            return name;
        }

        public void setName (string name) 
        {
            this.name = name;
        }

        // The usage of getter and setter adds a lot of boilerplate code. So, C# provides a shorthand called "Properties"
        private int age;

        public int Age
        {
            get
            {
                return age;
            }

            set 
            { 
                age = value;
            }
        }

        // If we won't do much validation, we can simply declare properties like this. That way we don't have to
        // explicitly write the private field name.
        public string Description { get; set; }

        // If prevent an outside entity from setting the value of a proeprty, we can totaly omit the set method. But, that
        // may lead to issues because we won't be able to set that value from within our own class as well. To solve this,
        // we can declare the set method as private. We can also make it protected if we want.
        public string Country { get; private set; }
    }
}
