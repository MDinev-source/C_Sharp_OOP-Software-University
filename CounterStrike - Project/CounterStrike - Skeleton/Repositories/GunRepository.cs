namespace CounterStrike.Repositories
{
    using System;

    using System.Linq;

    using System.Collections.Generic;

    using CounterStrike.Utilities.Messages;

    using CounterStrike.Models.Guns.Contracts;

    using CounterStrike.Repositories.Contracts;

    public class GunRepository : IRepository<IGun>
    {
        private List<IGun> models;

        public GunRepository()
        {
            this.models = new List<IGun>();
        }

        public IReadOnlyCollection<IGun> Models => this.models;

        public void Add(IGun model)
        {
            if (model == null)
            {
                throw new ArgumentException(ExceptionMessages.InvalidGunRepository);
            }

            this.models.Add(model);
        }

        public IGun FindByName(string name)
        {
            var searchedGun = this.models.FirstOrDefault(g => g.Name == name);

            return searchedGun;
        }

        public bool Remove(IGun model) => this.models.Remove(model);
    }
}