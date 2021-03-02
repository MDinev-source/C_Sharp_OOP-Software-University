namespace EasterRaces.Repositories.Entities
{
    using System.Linq;

    using System.Collections.Generic;

    using EasterRaces.Repositories.Contracts;

    using EasterRaces.Models.Drivers.Contracts;

    public class DriverRepository : IRepository<IDriver>
    {
        private readonly List<IDriver> drivers;
        public DriverRepository()
        {
            this.drivers = new List<IDriver>();
        }
        public IReadOnlyCollection<IDriver> Drivers
            => this.drivers.AsReadOnly();

        public void Add(IDriver model)
        {
            this.drivers.Add(model);
        }

        public IReadOnlyCollection<IDriver> GetAll()
        {
            return Drivers;
        }

        public IDriver GetByName(string name)
        {
            return drivers.FirstOrDefault(x => x.Name == name);
        }

        public bool Remove(IDriver model)
        {
            return this.drivers.Remove(model);
        }
    }
}
