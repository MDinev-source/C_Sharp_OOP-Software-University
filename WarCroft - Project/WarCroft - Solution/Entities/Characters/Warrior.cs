namespace WarCroft.Entities.Characters
{
    using WarCroft.Entities.Characters.Contracts;
    using WarCroft.Entities.Inventory;
    public class Warrior:Character,IAttacker
    {
        public Warrior(string name) 
            : base(name, 100, 50, 40, new Satchel())
        {
        }

        public void Attack(Character character)
        {
            throw new System.NotImplementedException();
        }
    }
}
