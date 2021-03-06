namespace CounterStrike.Models.Maps.Contracts
{
    using System.Collections.Generic;

    using CounterStrike.Models.Players.Contracts;

    public interface IMap
    {

        string Start(IReadOnlyCollection<IPlayer> models);
    }
}
