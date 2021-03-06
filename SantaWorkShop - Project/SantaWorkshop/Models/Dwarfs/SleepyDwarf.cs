﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SantaWorkshop.Models.Dwarfs
{
   public class SleepyDwarf:Dwarf
    {
        private const int InitialEnergy = 50;
        public SleepyDwarf(string name)
            : base(name, InitialEnergy)
        {
            this.Energy = InitialEnergy;
        }

        public override void Work()
        {
            this.Energy -= 15;
        }
    }
}
