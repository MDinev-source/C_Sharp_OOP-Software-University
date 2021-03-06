namespace PlayersAndMonsters.Repositories
{
    using System;
    using System.Linq;
    using PlayersAndMonsters.Common;
    using System.Collections.Generic;
    using PlayersAndMonsters.Repositories.Contracts;
    using PlayersAndMonsters.Models.Players.Contracts;

    public class PlayerRepository : IPlayerRepository
    {
        private readonly List<IPlayer> players;

        public PlayerRepository()
        {
            this.players = new List<IPlayer>();
        }

        public int Count => this.players.Count;

        public IReadOnlyCollection<IPlayer> Players
            => this.players.AsReadOnly();

        public void Add(IPlayer player)
        {
            if (player == null)
            {
                throw new ArgumentException(ExceptionMessages.InvalidPlayer);
            }
            else if (players.Any(x => x.Username == player.Username))
            {
                throw new ArgumentException(string.Format(ExceptionMessages.RepoContainsPlayer, player.Username));
            }

            players.Add(player);
        }

        public IPlayer Find(string username)
        {
            var currCard = players.FirstOrDefault(x => x.Username == username);
            return currCard;
        }

        public bool Remove(IPlayer player)
        {
            if (player == null)
            {
                throw new ArgumentException(ExceptionMessages.InvalidPlayer);
            }

            return players.Remove(player);
        }
    }
}
