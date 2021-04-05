using System;
using System.Collections.Generic;
using System.Text;

namespace SantaWorkshop.Models.Dwarfs
{
    public class HappyDwarf : Dwarf
    {
        private const int InitialEnergy = 100;
        public HappyDwarf(string name) 
            : base(name,InitialEnergy)
        {
            this.Energy = InitialEnergy;
        }

        public override void Work()
        {
            this.Energy -= 10;
        }
    }
}
