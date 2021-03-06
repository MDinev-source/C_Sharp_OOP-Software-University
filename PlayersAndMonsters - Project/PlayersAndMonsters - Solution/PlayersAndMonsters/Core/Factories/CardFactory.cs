namespace PlayersAndMonsters.Core.Factories
{
    using PlayersAndMonsters.Models.Cards;
    using PlayersAndMonsters.Models.Cards.Contracts;
    using PlayersAndMonsters.Core.Factories.Contracts;
    public class CardFactory : ICardFactory
    {
        public ICard CreateCard(string type, string name)
        {
            ICard card = null;

            switch (type)
            {
                case "Trap":
                    card = new TrapCard(name);
                    break;
                case "Magic":
                    card = new MagicCard(name);
                    break;
            }

            return card;
        }
    }
}
