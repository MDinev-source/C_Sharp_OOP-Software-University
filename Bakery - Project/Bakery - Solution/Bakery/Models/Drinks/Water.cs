namespace Bakery.Models.Drinks
{
    public class Water : Drink
    {
        private const decimal WaterPrice = 1.50m;
        public Water(string name, int portion, decimal price, string brand)
            : base(name, portion, price, brand)
        {
        }
    }
}
