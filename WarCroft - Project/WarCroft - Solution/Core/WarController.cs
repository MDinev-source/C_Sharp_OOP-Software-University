namespace WarCroft.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using WarCroft.Constants;
    using WarCroft.Entities.Characters;
    using WarCroft.Entities.Characters.Contracts;
    using WarCroft.Entities.Items;

    public class WarController
    {
        private List<Character> party;
        private List<Item> pool;
        public WarController()
        {
            this.party = new List<Character>();
            this.pool = new List<Item>();
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
            else if (characterType == nameof(Warrior))
            {
                character = new Warrior(name);
            }
            else
            {
                throw new ArgumentException(string.Format(ExceptionMessages.InvalidCharacterType, characterType));
            }

            party.Add(character);

            return string.Format(SuccessMessages.JoinParty, name);
        }

        public string AddItemToPool(string[] args)
        {
            var itemName = args[0];

            Item item;

            if (itemName == nameof(FirePotion))
            {
                item = new FirePotion();
            }
            else if (itemName == nameof(HealthPotion))
            {
                item = new HealthPotion();
            }
            else
            {
                throw new ArgumentException(string.Format(ExceptionMessages.InvalidItem, itemName));
            }

            pool.Add(item);

            return string.Format(SuccessMessages.AddItemToPool, itemName);
        }

        public string PickUpItem(string[] args)
        {
            var characterName = args[0];

            var character = this.party.FirstOrDefault(x => x.Name == characterName);

            if (character == null)
            {
                throw new ArgumentException(string.Format(ExceptionMessages.CharacterNotInParty, characterName));
            }

            var item = this.pool.Last();

            if (item == null)
            {
                throw new InvalidOperationException(ExceptionMessages.ItemPoolEmpty);
            }

            pool.Remove(item);
            character.Bag.AddItem(item);

            return string.Format(SuccessMessages.PickUpItem, characterName, item.GetType().Name);
        }

        public string UseItem(string[] args)
        {
            var characterName = args[0];
            var itemName = args[1];

            var character = this.party.FirstOrDefault(x => x.Name == characterName);

            if (character == null)
            {
                throw new ArgumentException(string.Format(ExceptionMessages.CharacterNotInParty, characterName));
            }

            var item = character.Bag.GetItem(itemName);

            character.UseItem(item);

            return string.Format(string.Format(SuccessMessages.UsedItem, characterName, itemName));
        }

        public string GetStats()
        {
            var sb = new StringBuilder();
            foreach (var character in party.OrderByDescending(x => x.IsAlive).ThenByDescending(x => x.Health))
            {
                sb.AppendLine(string.Format(SuccessMessages.CharacterStats
                    , character.Name
                    , character.Health
                    , character.BaseHealth
                    , character.Armor
                    , character.BaseArmor
                    , character.IsAlive
                    ? "Alive"
                    : "Dead"));
            }

            return sb.ToString();
        }

        public string Attack(string[] args)
        {
            var attackerName = args[0];
            var receiverName = args[1];

            var currentAttacker = this.party.FirstOrDefault(x => x.Name == attackerName);
            var receiver = this.party.FirstOrDefault(x => x.Name == receiverName);

            Warrior attacker = (Warrior)currentAttacker;

            if (attacker == null || receiver == null)
            {
                if (attacker == null)
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.CharacterNotInParty, attackerName));
                }
                else
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.CharacterNotInParty, receiverName));
                }
            }

            if (attacker.AbilityPoints == 0)
            {
                throw new ArgumentException(string.Format(ExceptionMessages.AttackFail, attackerName));
            }

            attacker.Attack(receiver);

            var sb = new StringBuilder();

            sb.AppendLine(string.Format(SuccessMessages.AttackCharacter
                , attackerName
                , receiverName
                , attacker.AbilityPoints
                , receiverName
                , receiver.Health
                , receiver.BaseHealth
                , receiver.Armor
                , receiver.BaseArmor));

            if (!receiver.IsAlive)
            {
                sb.AppendLine(string.Format(SuccessMessages.AttackKillsCharacter, receiverName));
            }

            return sb.ToString().TrimEnd();
        }

        public string Heal(string[] args)
        {
            var healerName = args[0];
            var receiverName = args[1];

            var currenthealer = this.party.FirstOrDefault(x => x.Name == healerName);
            var receiver = this.party.FirstOrDefault(x => x.Name == receiverName);

            Priest healer = (Priest)currenthealer;

            if (healer == null || receiver == null)
            {
                if (healer == null)
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.CharacterNotInParty, healerName));
                }
                else
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.CharacterNotInParty, receiverName));
                }
            }

            if (healer.AbilityPoints == 0)
            {
                throw new ArgumentException(string.Format(ExceptionMessages.HealerCannotHeal, healerName));
            }
            var sb = new StringBuilder();

            healer.Heal(receiver);

            sb.AppendLine(string.Format(SuccessMessages.HealCharacter
                , healerName
                , receiverName
                , healer.AbilityPoints
                , receiverName
                , receiver.Health));

            return sb.ToString().TrimEnd();
        }
    }
}
