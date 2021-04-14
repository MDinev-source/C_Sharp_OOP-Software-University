namespace WarCroft.Entities.Characters
{
    using WarCroft.Entities.Characters.Contracts;
    using WarCroft.Entities.Inventory;

    public class Priest:Character, IHealer
    {
        public Priest(string name) 
            : base(name, 50, 25, 40, new Backpack())
        {
            this.Bag = new Backpack();
        }
        
        public void Heal(Character character)
        {
            throw new System.NotImplementedException();
        }
    }
}
