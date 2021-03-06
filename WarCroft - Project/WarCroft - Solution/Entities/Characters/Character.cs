﻿namespace WarCroft.Entities.Characters.Contracts
{
    using System;
    using WarCroft.Constants;
    using WarCroft.Entities.Inventory;
    using WarCroft.Entities.Items;
    public abstract class Character
    {
        private double health;
        private string name;
        private double armor;

        protected Character(string name, double baseHealth, double baseArmor, double abilityPoins, Bag bag)

        {

            this.Name = name;
            this.BaseHealth = baseHealth;
            this.BaseArmor = baseArmor;
            this.AbilityPoints = abilityPoins;
            this.Health = BaseHealth;
            this.Armor = BaseArmor;
            this.Bag = bag;
        }

        public string Name
        {
            get
            {
                return this.name;
            }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.CharacterNameInvalid);
                }
                this.name = value;
            }
        }

        public double BaseHealth { get; }

        public double Health
        {
            get
            {
                return this.health;
            }
            set
            {
                if (value > this.BaseHealth)
                {
                    value = this.BaseHealth;
                }
                if (value < 0)
                {
                    value = 0;
                }
                this.health = value;
            }
        }

        public double BaseArmor { get; }
        public double Armor
        {
            get
            {
                return this.armor;
            }
            private set
            {
                if (value < 0)
                {
                    value = 0;
                }
                armor = value;
            }
        }

        public double AbilityPoints { get; set; }

        public Bag Bag { get; set; }
        public bool IsAlive { get; set; } = true;

        protected void EnsureAlive()
        {
            if (!this.IsAlive)
            {
                throw new InvalidOperationException(ExceptionMessages.AffectedCharacterDead);
            }
        }

        public void TakeDamage(double hitPoints)
        {
            this.EnsureAlive();

            if (this.Armor >= hitPoints)
            {
                this.Armor = this.Armor - hitPoints;
                hitPoints = 0;
            }
            else
            {
                hitPoints = hitPoints - this.Armor;
                this.Armor = 0;
            }

            if (hitPoints > 0 && hitPoints < this.Health)
            {
                this.Health = this.Health - hitPoints;
            }

            else
            {
                hitPoints = hitPoints - this.Health;
                this.Health = 0;
                IsAlive = false;
            }
        }

        public void UseItem(Item item)
        {
            this.EnsureAlive();

            item.AffectCharacter(this);
        }
    }
}