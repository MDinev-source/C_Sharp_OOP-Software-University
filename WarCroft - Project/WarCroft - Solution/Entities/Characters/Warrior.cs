namespace WarCroft.Entities.Characters
{
    using WarCroft.Entities.Characters.Contracts;
    using WarCroft.Entities.Inventory;
    public class Warrior:Character,IAttacker
    {
        public Warrior(string name)
            : base(name, 100, 20, 40, new Satchel())
        {
            this.Bag = new Backpack();
        }
        public void Attack(Character character)
        {
            throw new System.NotImplementedException();
        }
    }
}
