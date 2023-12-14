using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_4_User_Defined_Classes
{     
    internal class Pet
    {
        private string _name;
        private string _type;
        private int _age;
        private double _weight;

        //Constructor method
        public Pet()
        {
            //Set default values for properties
            _name = "Spot";
            _type = "Dog";
            _age = 1;
            _weight = 5;
        }


    public string Name
        {
            get { return _name; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("The name of the pet cannot be blank");
                }
                else
                {
                    _name = value;
                }
            }
        }
        public int Age
        {
            get { return _age; }
            set
            {
                if (value < 1)
                {
                    throw new ArgumentException("The age of the pet cannot be less than one year");

                }
                else
                {
                    _age = value;
                }                
            }
        }
        public double Weight
        {
            get { return _weight; }
            set
            {
                if (value < 5)
                {
                    throw new ArgumentException("The weight of the pet must be 5 pounds or greater.");
                }
                else
                {
                    _weight = value;
                }
            }
        }

        public string Type
        {
            get { return _type ?? "Dog"; } //If no _type is provided return the default value "Dog"
            set
            {
                _type = value;
            }
        }

        public Pet (string name, int age, double weight, string type)
        {
            _name = name;
            _type = type;
            _age = age;
            _weight = weight;

        }

        public double Acepromazine()
        {
            double dosage = Weight * (0.03 / 10); 
            
            if (Type.ToLower() == "cat")
            {
                dosage = _weight * (0.002 / 10);
            }

            return dosage;
        }

        public double Carprofen()
        {
            double dosage = Weight * (0.05 / 12);

            if (Type.ToLower() == "cat")
            {
                dosage = _weight * (0.025 / 12);
            }

            return dosage;
        }
    }
}
