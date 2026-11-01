using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fruits_oop
{
    public class Fruit
    {
        public string Name { get; init; }
        public double Weight { get; init; }

        public Fruit(string name, double weight)
        {
            this.Name = name;
            this.Weight = weight;
        }

        public string DisplayInfo()
        {
            return $"{Name} ({Weight} kg)";
        }
    }
}