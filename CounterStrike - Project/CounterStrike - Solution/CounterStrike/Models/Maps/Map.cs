namespace CounterStrike.Models.Maps
{

    using System.Linq;

    using System.Collections.Generic;

    using CounterStrike.Models.Maps.Contracts;

    using CounterStrike.Models.Players.Contracts;
    public class Map : IMap
    {
        public string Start(IReadOnlyCollection<IPlayer> players)
        {
            var terrorists = players.Where(x => x.GetType().Name == "Terrorist" && x.IsAlive == true).ToList();
            var counterTerrorists = players.Where(x => x.GetType().Name == "CounterTerrorist" && x.IsAlive == true).ToList();

            while (true)
            {

                // Terrorists atack
                foreach (var terrorist in terrorists.Where(t => t.IsAlive == true))
                {
                    foreach (var counterTerrorist in counterTerrorists.Where(ct => ct.IsAlive == true))
                    {

                        counterTerrorist.TakeDamage(terrorist.Gun.Fire());

                    }
                }

                if (!counterTerrorists.Any(ct => ct.IsAlive == true))
                {
                    return "Terrorist wins!";

                }

                //Counter terrorist attack
                foreach (var counterTerrorist in counterTerrorists.Where(ct => ct.IsAlive == true))
                {
                    foreach (var terrorist in terrorists.Where(t => t.IsAlive == true))
                    {

                        terrorist.TakeDamage(counterTerrorist.Gun.Fire());

                    }
                }

                if (!terrorists.Any(ct => ct.IsAlive == true))
                {
                    return "Counter Terrorist wins!";

                }
            }
        }
    }
}
