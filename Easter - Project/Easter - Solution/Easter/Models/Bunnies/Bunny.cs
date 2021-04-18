namespace Easter.Models.Bunnies
{
    using Easter.Models.Bunnies.Contracts;
    using Easter.Models.Dyes.Contracts;
    using Easter.Utilities.Messages;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public abstract class Bunny : IBunny
    {
        private string name;
        private int energy;

        protected Bunny(string name, int energy)
        {
            Name = name;
            Energy = energy;
            Dyes = new List<IDye>();
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
                    throw new ArgumentException(ExceptionMessages.InvalidBunnyName);
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
                if (value < 0)
                {
                    value = 0;
                }
                this.energy = value;
            }
        }
        public ICollection<IDye> Dyes { get; }

        public void AddDye(IDye dye)
        {
            Dyes.Add(dye);
        }
        public abstract void Work();

        public override string ToString()
        {
            var sb = new StringBuilder();

            var dyesNotFinishedCount = this.Dyes.Count(x => !x.IsFinished());

            sb.AppendLine($"Name: {this.Name}");
            sb.AppendLine($"Energy: {this.Energy}");
            sb.AppendLine($"Dyes: {dyesNotFinishedCount} not finished");

            return sb.ToString().TrimEnd();
        }
    }
}
