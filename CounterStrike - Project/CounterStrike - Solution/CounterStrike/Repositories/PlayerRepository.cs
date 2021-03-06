namespace CounterStrike.Repositories
{
    using System;

    using System.Linq;

    using System.Collections.Generic;

    using CounterStrike.Utilities.Messages;

    using CounterStrike.Repositories.Contracts;

    using CounterStrike.Models.Players.Contracts;
    public class PlayerRepository : IRepository<IPlayer>
    {
        private readonly List<IPlayer> models;

        public PlayerRepository()
        {
            this.models = new List<IPlayer>();
        }

        public IReadOnlyCollection<IPlayer> Models => this.models;

        public void Add(IPlayer model)
        {
            if (model == null)
            {
                throw new ArgumentException(ExceptionMessages.InvalidPlayerRepository);
            }

            this.models.Add(model);
        }

        public IPlayer FindByName(string name)
        {
            var searchedPlayer = this.models.FirstOrDefault(p => p.Username == name);

            return searchedPlayer;
        }

        public bool Remove(IPlayer model) => this.models.Remove(model);
    }
}

