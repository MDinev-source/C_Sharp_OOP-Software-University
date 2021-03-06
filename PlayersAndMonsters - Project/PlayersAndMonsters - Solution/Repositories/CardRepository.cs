namespace PlayersAndMonsters.Repositories
{
    using System;
    using System.Linq;
    using PlayersAndMonsters.Common;
    using PlayersAndMonsters.Models.Cards.Contracts;
    using PlayersAndMonsters.Repositories.Contracts;
    using System.Collections.Generic;

    public class CardRepository : ICardRepository
    {
        private readonly List<ICard> cards;

        public CardRepository()
        {
            this.cards = new List<ICard>();
        }

        public int Count => this.cards.Count;

        public IReadOnlyCollection<ICard> Cards
            => this.cards.AsReadOnly();

        public void Add(ICard card)
        {
            if (card == null)
            {
                throw new ArgumentException(ExceptionMessages.InvalidCard);
            }
            else if (cards.Any(x => x.Name == card.Name))
            {
                throw new ArgumentException(string.Format(ExceptionMessages.RepoContainsCard, card.Name));
            }

            cards.Add(card);
        }

        public ICard Find(string name)
        {
            var currCard = cards.FirstOrDefault(x => x.Name == name);
            return currCard;
        }

        public bool Remove(ICard card)
        {
            if (card == null)
            {
                throw new ArgumentException(ExceptionMessages.InvalidCard);
            }

            return cards.Remove(card);
        }
    }
}
