namespace CounterStrike.Core
{
    using System;

    using System.Linq;

    using System.Text;

    using System.Collections.Generic;

    using CounterStrike.Core.Contracts;

    using CounterStrike.Models.Guns;
    using CounterStrike.Models.Guns.Contracts;

    using CounterStrike.Models.Maps;
    using CounterStrike.Models.Maps.Contracts;

    using CounterStrike.Models.Players;
    using CounterStrike.Models.Players.Contracts;

    using CounterStrike.Repositories;
    using CounterStrike.Repositories.Contracts;

    using CounterStrike.Utilities.Messages;
    public class Controller : IController
    {
        private IRepository<IGun> guns;
        private IRepository<IPlayer> players;
        private IMap map;

        public Controller()
        {
            this.guns = new GunRepository();
            this.players = new PlayerRepository();
            this.map = new Map();
        }


        public string AddGun(string type, string name, int bulletsCount)
        {
            IGun gun;

            if (type == nameof(Rifle))
            {
                gun = new Rifle(name, bulletsCount);
            }
            else if (type == nameof(Pistol))
            {
                gun = new Pistol(name, bulletsCount);
            }
            else
            {
                throw new ArgumentException(ExceptionMessages.InvalidGunType);
            }

            this.guns.Add(gun);

            string result = String.Format(OutputMessages.SuccessfullyAddedGun, name);
            return result;
        }

        public string AddPlayer(string type, string username, int health, int armor, string gunName)
        {
            IGun gun;
            IPlayer player;

            gun = this.guns.FindByName(gunName);

            if (gun == null)
            {
                throw new ArgumentException(ExceptionMessages.GunCannotBeFound);
            }

            if (type == nameof(Terrorist))
            {
                player = new Terrorist(username, health, armor, gun);
            }
            else if (type == nameof(CounterTerrorist))
            {
                player = new CounterTerrorist(username, health, armor, gun);
            }
            else
            {
                throw new ArgumentException(ExceptionMessages.InvalidPlayerType);
            }

            this.players.Add(player);

            string result = String.Format(OutputMessages.SuccessfullyAddedPlayer, username);

            return result;
        }

        public string Report()
        {
            StringBuilder sb = new StringBuilder();

            foreach (var player in this.players.Models.OrderBy(p => p.GetType().Name).ThenByDescending(p => p.Health).ThenBy(p => p.Username))
            {
                sb.AppendLine($"{player}");
            }

            return sb.ToString().TrimEnd();
        }

        public string StartGame()
        {
            List<IPlayer> allAlivePlayers = this.players.Models.Where(p => p.IsAlive == true).ToList();
            string result = this.map.Start(allAlivePlayers);
            return result;
        }
    }
}