namespace Bakery.Models.BakedFoods
{
    public class Bread : BakedFood
    {
        private const int initialbreadportion = 200;

        public Bread(string name, decimal price) 
            : base(name,initialbreadportion, price)
        {
        }
    }
}
