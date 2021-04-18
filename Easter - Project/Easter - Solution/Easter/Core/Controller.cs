namespace Easter.Core
{
    using Easter.Core.Contracts;
    using Easter.Models.Bunnies;
    using Easter.Models.Bunnies.Contracts;
    using Easter.Models.Dyes;
    using Easter.Models.Eggs;
    using Easter.Models.Eggs.Contracts;
    using Easter.Models.Workshops;
    using Easter.Models.Workshops.Contracts;
    using Easter.Repositories;
    using Easter.Repositories.Contracts;
    using Easter.Utilities.Messages;
    using System;
    using System.Linq;
    using System.Text;

    public class Controller : IController
    {
        private readonly IRepository<IBunny> bunnies;
        private readonly IRepository<IEgg> eggs;
        private IWorkshop workshop;
        public Controller()
        {
            this.bunnies = new BunnyRepository();
            this.eggs = new EggRepository();
            this.workshop = new Workshop();
        }

        public string AddBunny(string bunnyType, string bunnyName)
        {
            IBunny bunny;

            if (bunnyType == nameof(HappyBunny))
            {
                bunny = new HappyBunny(bunnyName);
            }
            else if (bunnyType == nameof(SleepyBunny))
            {
                bunny = new SleepyBunny(bunnyName);
            }
            else
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidBunnyType);
            }

            bunnies.Add(bunny);

            return string.Format(OutputMessages.BunnyAdded, bunnyType, bunnyName);
        }

        public string AddDyeToBunny(string bunnyName, int power)
        {
            var bunny = bunnies.FindByName(bunnyName);

            if (bunny == null)
            {
                throw new InvalidOperationException(ExceptionMessages.InexistentBunny);
            }

            var dye = new Dye(power);

            bunny.AddDye(dye);

            return string.Format(OutputMessages.DyeAdded, power, bunnyName);
        }

        public string AddEgg(string eggName, int energyRequired)
        {
            var egg = new Egg(eggName, energyRequired);

            eggs.Add(egg);

            return string.Format(OutputMessages.EggAdded, eggName);
        }

        public string ColorEgg(string eggName)
        {
            var mostReadyBunniers = bunnies.Models
                 .OrderByDescending(x => x.Energy)
                 .Where(c => c.Energy >= 50)
                 .ToList();

            if (!mostReadyBunniers.Any())
            {
                throw new InvalidOperationException(ExceptionMessages.BunniesNotReady);
            }

            var egg = eggs.FindByName(eggName);

            foreach (var bunny in mostReadyBunniers)
            {
                while (bunny.Energy > 0 && !egg.IsDone())
                {
                    workshop.Color(egg, bunny);
                }

                if (egg.IsDone())
                {
                    if (bunny.Energy == 0)
                    {

                        this.bunnies.Remove(bunny);
                    }
                    break;
                }

                this.bunnies.Remove(bunny);

            }


            if (egg.IsDone())
            {
                return string.Format(OutputMessages.EggIsDone, eggName);
            }

            return string.Format(OutputMessages.EggIsNotDone, eggName);

        }

        public string Report()
        {
            var sb = new StringBuilder();

            var coloredEggsCount = eggs.Models.Count(x => x.IsDone());

            sb.AppendLine($"{coloredEggsCount} eggs are done!");
            sb.AppendLine("Bunnies info:");

            foreach (var bunny in bunnies.Models)
            {
                sb.AppendLine(bunny.ToString());
            }

            return sb.ToString().TrimEnd();
        }
    }
}
