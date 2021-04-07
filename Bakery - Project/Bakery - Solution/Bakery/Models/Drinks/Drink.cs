namespace Bakery.Models.Drinks
{
    using Bakery.Models.Drinks.Contracts;
    public abstract class Drink : IDrink
    {
        public abstract string Name { get; }
        public abstract int Portion { get; }
        public abstract decimal Price { get; }
        public abstract string Brand { get; }
    }
}
