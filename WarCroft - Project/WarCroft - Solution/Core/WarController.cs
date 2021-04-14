namespace WarCroft.Core
{
    using System;
    using System.Collections.Generic;
    using WarCroft.Entities.Characters;
    using WarCroft.Entities.Characters.Contracts;
    public class WarController
    {
        private List<Character> party;
        private List<Character> pool;
        public WarController()
        {
            this.party = new List<Character>();
            this.pool = new List<Character>();

        }

        public string JoinParty(string[] args)
        {
            var characterType = args[0];
            var name = args[1];

            Character character;

            if (characterType == nameof(Priest))
            {
                character = new Priest(name);
            }
            return null;
        }

        public string AddItemToPool(string[] args)
        {
            throw new NotImplementedException();
        }

        public string PickUpItem(string[] args)
        {
            throw new NotImplementedException();
        }

        public string UseItem(string[] args)
        {
            throw new NotImplementedException();
        }

        public string GetStats()
        {
            throw new NotImplementedException();
        }

        public string Attack(string[] args)
        {
            throw new NotImplementedException();
        }

        public string Heal(string[] args)
        {
            throw new NotImplementedException();
        }
    }
}
