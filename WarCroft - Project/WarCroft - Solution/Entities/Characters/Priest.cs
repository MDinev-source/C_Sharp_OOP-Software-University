namespace WarCroft.Entities.Characters
{
    using WarCroft.Entities.Characters.Contracts;
    using WarCroft.Entities.Inventory;

    public class Priest:Character, IHealer
    {
        private const double InitialAbilityPoints = 40; 
        public Priest(string name, double health, double armor, Bag bag) 
            : base(name, health, armor, InitialAbilityPoints, bag)
        {
            this.Bag = new Backpack();
        }
        public override double BaseHealth 
        { 
            get => base.BaseHealth; 
            protected set =>base.BaseHealth = value=50; 
        }

        public override double BaseArmor
        { 
            get => base.BaseArmor;
            protected set => base.BaseArmor = value=25; 
        }
        public void Heal(Character character)
        {
            throw new System.NotImplementedException();
        }
    }
}
