namespace CounterStrike.Models.Guns
{
    public class Rifle : Gun
    {
        private const int BulletsFired = 10;

        public Rifle(string name, int bulletsCount) : base(name, bulletsCount)
        {

        }

        public override int Fire()
        {
            if (this.BulletsCount - BulletsFired >= 0)
            {
                this.BulletsCount -= BulletsFired;

                return BulletsFired;
            }
            else
            {
                return 0;
            }
        }
    }
}



