namespace CounterStrike.Models.Guns
{
    public class Pistol : Gun
    {
        private const int BulletsFired = 1;

        public Pistol(string name, int bulletsCount) : base(name, bulletsCount)
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
