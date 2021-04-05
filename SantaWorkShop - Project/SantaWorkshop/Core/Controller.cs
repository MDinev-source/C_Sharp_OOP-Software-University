using SantaWorkshop.Core.Contracts;
using SantaWorkshop.Models.Dwarfs;
using SantaWorkshop.Models.Dwarfs.Contracts;
using SantaWorkshop.Models.Instruments;
using SantaWorkshop.Models.Instruments.Contracts;
using SantaWorkshop.Models.Presents;
using SantaWorkshop.Models.Presents.Contracts;
using SantaWorkshop.Models.Workshops;
using SantaWorkshop.Repositories;
using SantaWorkshop.Repositories.Contracts;
using SantaWorkshop.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SantaWorkshop.Core
{
    public class Controller : IController
    {
        private IRepository<IDwarf> dwarfRepository;
        private IRepository<IPresent> presentRepository;

        public Controller()
        {
            this.dwarfRepository = new DwarfRepository();
            this.presentRepository = new PresentRepository();
        }
        public string AddDwarf(string dwarfType, string dwarfName)
        {
            IDwarf dwarf;

            if (dwarfType==nameof(HappyDwarf))
            {
                dwarf = new HappyDwarf(dwarfName);
            }
            else if (dwarfType==nameof(SleepyDwarf))
            {
                dwarf = new SleepyDwarf(dwarfName);
            }
            else
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidDwarfType);
            }

            dwarfRepository.Add(dwarf);
            return string.Format(OutputMessages.DwarfAdded, dwarfType, dwarfName);
        }

        public string AddInstrumentToDwarf(string dwarfName, int power)
        {
            IInstrument instrument = new Instrument(power);

            var currentDwarf = dwarfRepository.FindByName(dwarfName);

            if (currentDwarf == null)
            {
                throw new InvalidOperationException(ExceptionMessages.InexistentDwarf);
            }

            currentDwarf.AddInstrument(instrument);
            return string.Format(OutputMessages.InstrumentAdded, power, dwarfName);
        }

        public string AddPresent(string presentName, int energyRequired)
        {
            IPresent present = new Present(presentName, energyRequired);
            presentRepository.Add(present);
            return string.Format(OutputMessages.PresentAdded, presentName);
            
        }

        public string CraftPresent(string presentName)
        {
            IPresent present = presentRepository.FindByName(presentName);
            var dwarfsReady = dwarfRepository.Models.OrderByDescending(x => x.Energy).Where(x => x.Energy >= 50).ToList();
            var workShop = new Workshop();

            if (!dwarfsReady.Any())
            {
                throw new InvalidOperationException(ExceptionMessages.DwarfsNotReady);
            }
            
            while (dwarfsReady.Any())
            {
                IDwarf currentDwarf = dwarfsReady.First();

                workShop.Craft(present, currentDwarf);

                dwarfsReady.Remove(currentDwarf);
            }

            if (present.IsDone()) 
            {
                return string.Format(OutputMessages.PresentIsDone, present.Name);
            }

            else
            {
                return string.Format(OutputMessages.PresentIsNotDone, present.Name);
            }
        }

        public string Report()
        {
           int presentsIsDone=presentRepository.Models.Where(x => x.IsDone()).Count();

            var sb = new StringBuilder();
            sb.AppendLine($"{presentsIsDone} presents are done!");
            sb.AppendLine("Dwarfs info:");

            foreach (var dwars in dwarfRepository.Models)
            {
                sb.AppendLine(dwars.ToString());
            }
            return sb.ToString().TrimEnd();
        }
    }
}
