﻿namespace WarCroft.Entities.Characters
{
    using WarCroft.Entities.Characters.Contracts;
    using WarCroft.Entities.Inventory;

    public class Priest:Character, IHealer
    {
        public Priest(string name) 
            : base(name,50, 25, 40, new Backpack())
        {
            
        }
        public void Heal(Character character)
        {
            this.AbilityPoints += character.AbilityPoints;
        }
    }
}
