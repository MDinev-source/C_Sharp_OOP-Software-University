using SantaWorkshop.Models.Dwarfs.Contracts;
using SantaWorkshop.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SantaWorkshop.Repositories
{
    public class DwarfRepository : IRepository<IDwarf>
    {
        private readonly List<IDwarf> dwarves;

        public DwarfRepository()
        {
            this.dwarves = new List<IDwarf>();
        }
        public IReadOnlyCollection<IDwarf> Models
            => this.dwarves.AsReadOnly();
           

        public void Add(IDwarf model)
        {
            dwarves.Add(model);
        }

        public IDwarf FindByName(string name)
        {
            var currentDwarf = dwarves.FirstOrDefault(x => x.Name == name);
            return currentDwarf;
        }

        public bool Remove(IDwarf model)
        {
            return dwarves.Remove(model);
        }
    }
}

