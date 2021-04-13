namespace Bakery.Models.Tables
{
    using System;
    using System.Linq;
    using System.Text;
    using Bakery.Utilities.Messages;
    using System.Collections.Generic;
    using Bakery.Models.Drinks.Contracts;
    using Bakery.Models.Tables.Contracts;
    using Bakery.Models.BakedFoods.Contracts;

    public abstract class Table : ITable
    {
        private List<IBakedFood> foodOrders;
        private List<IDrink> drinkOrders;
        private int capacity;
        private int numberOfPeople;

        public Table(int tableNumber, int capacity, decimal pricePerPerson)
        {
            this.TableNumber = tableNumber;
            this.Capacity = capacity;
            this.PricePerPerson = pricePerPerson;
            this.IsReserved = false;

            this.foodOrders = new List<IBakedFood>();
            this.drinkOrders = new List<IDrink>();
        }
        public int TableNumber { get; }
        public int Capacity
        {
            get
            {
                return this.capacity;
            }
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException(ExceptionMessages.InvalidTableCapacity);
                }

                this.capacity = value;
            }
        }
        public int NumberOfPeople
        {
            get
            {
                return this.numberOfPeople;
            }

            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentException(ExceptionMessages.InvalidNumberOfPeople);
                }

                this.numberOfPeople = value;
            }
        }
        public decimal PricePerPerson { get; }

        public bool IsReserved { get; private set; }

        public decimal Price
            => this.NumberOfPeople * this.PricePerPerson;

        public IReadOnlyCollection<IBakedFood> FoodOrders
            => this.foodOrders.AsReadOnly();

        public IReadOnlyCollection<IDrink> DrinkOrders
            => this.drinkOrders.AsReadOnly();

        public void Clear()
        {
            this.drinkOrders.Clear();
            this.foodOrders.Clear();
        }

        public decimal GetBill()
        {
            var result = this.DrinkOrders.Sum(x => x.Price)
                + this.FoodOrders.Sum(y => y.Price)
                + this.NumberOfPeople * this.PricePerPerson;

            return result;
        }

        public string GetFreeTableInfo()
        {
            var sb = new StringBuilder();

            sb.AppendLine($"Table: {this.TableNumber}");
            sb.AppendLine($"Type: {this.GetType().Name}");
            sb.AppendLine($"Capacity: {this.Capacity}");
            sb.AppendLine($"Price per Person: {this.PricePerPerson}");

            return sb.ToString().TrimEnd();
        }

        public void OrderDrink(IDrink drink)
        {
            this.drinkOrders.Add(drink);
        }

        public void OrderFood(IBakedFood food)
        {
            this.foodOrders.Add(food);
        }

        public void Reserve(int numberOfPeople)
        {
            this.NumberOfPeople = numberOfPeople;
            this.IsReserved = true;
        }
    }
}
