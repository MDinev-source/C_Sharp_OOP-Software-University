namespace Bakery.Models.BakedFoods
{
    using Bakery.Utilities.Messages;
    using System;
    using System.Collections.Generic;
    using System.Text;
    public class BakedFood
    {
        private string name;
        private int portion;

        public string Name
        {
            get 
            { 
                return name; 
            }
            private set 
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.InvalidName);
                }
                name = value; 
            }
        }

        public int Portion
        {
            get 
            {
                return portion; 
            }
            set 
            {
                if (value <= 0)
                {
                    throw new ArgumentException(ExceptionMessages.InvalidPortion);
                }
                portion = value; 
            }
        }

    }
}
