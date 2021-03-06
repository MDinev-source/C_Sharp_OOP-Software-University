namespace CounterStrike.Models.Players
{
    using System;

    using System.Text;

    using CounterStrike.Utilities.Messages;

    using CounterStrike.Models.Guns.Contracts;

    using CounterStrike.Models.Players.Contracts;
    public class Player : IPlayer
    {
        private string username;
        private int health;
        private int armor;
        private IGun gun;


        public Player(string username, int health, int armor, IGun gun)
        {
            this.Username = username;
            this.Health = health;
            this.Armor = armor;
            this.Gun = gun;
        }

        public string Username
        {
            get
            {
                return this.username;
            }
            private set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.InvalidPlayerName);
                }

                this.username = value;
            }
        }

        public int Health
        {
            get
            {
                return this.health;
            }
            private set
            {
                if (value <= 0)
                {

                    throw new ArgumentException(ExceptionMessages.InvalidPlayerHealth);
                }

                this.health = value;

            }
        }
        public int Armor
        {
            get
            {
                return this.armor;
            }
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException(ExceptionMessages.InvalidPlayerArmor);
                }

                this.armor = value;
            }
        }
        public IGun Gun
        {
            get
            {
                return this.gun;
            }
            private set
            {
                if (value == null)
                {
                    throw new ArgumentException(ExceptionMessages.InvalidGun);
                }

                this.gun = value;
            }
        }

        public bool IsAlive => this.Health > 0;

        public void TakeDamage(int points)
        {

            int currentpoints = points;

            if (this.armor - currentpoints >= 0)
            {
                this.armor -= currentpoints;
            }
            else if (this.armor - currentpoints < 0)
            {
                currentpoints = currentpoints - this.armor;
                this.armor = 0;
                this.health -= currentpoints;
            }

            if (this.health <= 0)
            {

                this.health = 0;
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"{this.GetType().Name}: {this.Username}");
            sb.AppendLine($"--Health: {this.Health}");
            sb.AppendLine($"--Armor: {this.Armor}");
            sb.AppendLine($"--Gun: {this.Gun.Name}");

            return sb.ToString().TrimEnd();
        }
    }
}

