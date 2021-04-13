using System;

using WarCroft.Constants;

namespace WarCroft.Entities.Characters.Contracts
{
    public abstract class Character
    {
        private double health;

        public double BaseHealth { get; set; }

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

        public bool IsAlive { get; set; } = true;

        protected void EnsureAlive()
        {
            if (!this.IsAlive)
            {
                throw new InvalidOperationException(ExceptionMessages.AffectedCharacterDead);
            }
        }
    }
}