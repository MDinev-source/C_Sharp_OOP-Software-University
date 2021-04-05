using SantaWorkshop.Models.Dwarfs.Contracts;
using SantaWorkshop.Models.Instruments.Contracts;
using SantaWorkshop.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SantaWorkshop.Models.Dwarfs
{
    public abstract class Dwarf : IDwarf
    {
        private string name;
        private int energy;
        private Dwarf()
        {
            this.Instruments = new List<IInstrument>();
        }
        protected Dwarf(string name, int energy)
            :this()
        {
            Name = name;
            Energy = energy;

        }

        public string Name 
        {
            get
            {
                return this.name;
            }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.InvalidDwarfName);
                }
                this.name = value;
            }
        }
        public int Energy 
        {
            get
            {
                return this.energy;
            }
            protected set
            {
                if (value<0)
                {
                    value = 0;
                }

                this.energy = value;
            }
        }
        public ICollection<IInstrument> Instruments { get; }

        public void AddInstrument(IInstrument instrument)
        {
            this.Instruments.Add(instrument);
        }
        public abstract void Work();

        public override string ToString()
        {
            int count = Instruments.Where(x =>!(x.IsBroken())).Count();

            var sb = new StringBuilder();
            sb.AppendLine($"Name: {Name}");
            sb.AppendLine($"Energy: {Energy}");
            sb.AppendLine($"Instruments: {count} not broken left");

            return sb.ToString().TrimEnd();
        }
    }
}
