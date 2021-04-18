namespace Easter.Models.Workshops
{
    using Easter.Models.Bunnies.Contracts;
    using Easter.Models.Eggs.Contracts;
    using Easter.Models.Workshops.Contracts;
    using System.Linq;

    public class Workshop : IWorkshop
    {
        public void Color(IEgg egg, IBunny bunny)
        {
            while (bunny.Energy > 0 && bunny.Dyes.Any())
            {
                var dye = bunny.Dyes.FirstOrDefault(x => !x.IsFinished());

                while (!dye.IsFinished() && !egg.IsDone())
                {

                    bunny.Work();
                    dye.Use();
                    egg.GetColored();

                    if (bunny.Energy == 0)
                    {
                        break;
                    }

                }

                if (dye.IsFinished())
                {
                    bunny.Dyes.Remove(dye);
                }

                if (egg.IsDone())
                {
                    break;
                }
            }

        }
    }
}

