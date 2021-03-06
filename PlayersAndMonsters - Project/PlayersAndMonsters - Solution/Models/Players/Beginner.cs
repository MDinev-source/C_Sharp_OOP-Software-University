namespace PlayersAndMonsters.Models.Players
{
    using PlayersAndMonsters.Repositories.Contracts;

    public class Beginner : Player
    {
        private const int InitialHealth = 50;
        public Beginner(ICardRepository cardRepository, string username)
            : base(cardRepository, username, InitialHealth)
        {

        }

    }
}
