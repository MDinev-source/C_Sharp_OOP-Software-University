namespace Bakery.Core
{
    using System;
    using System.Text;
    using System.Linq;
    using Bakery.Models.Drinks;
    using Bakery.Models.Tables;
    using Bakery.Core.Contracts;
    using Bakery.Utilities.Enums;
    using Bakery.Models.BakedFoods;
    using Bakery.Utilities.Messages;
    using System.Collections.Generic;
    using Bakery.Models.Drinks.Contracts;
    using Bakery.Models.Tables.Contracts;
    using Bakery.Models.BakedFoods.Contracts;

    public class Controller : IController
    {
        private List<IBakedFood> bakedFoods;
        private List<IDrink> drinks;
        private List<ITable> tables;

        public Controller()
        {
            this.bakedFoods = new List<IBakedFood>();
            this.drinks = new List<IDrink>();
            this.tables = new List<ITable>();
        }
        public string AddDrink(string type, string name, int portion, string brand)
        {
            var currentDrink = Enum.TryParse(type, out DrinkType validDrink);

            IDrink drink;

            if (validDrink.ToString() == nameof(Tea))
            {
                drink = new Tea(name, portion, brand);
            }
            else
            {
                drink = new Water(name, portion, brand);
            }

            var result = string.Empty;

            if (currentDrink)
            {
                this.drinks.Add(drink);

                result = string.Format(OutputMessages.DrinkAdded, name, brand);
            }

            return result;
        }

        public string AddFood(string type, string name, decimal price)
        {
            var food = Enum.TryParse(type, out BakedFoodType validFood);

            IBakedFood bakedFood;

            if (validFood.ToString() == nameof(Bread))
            {
                bakedFood = new Bread(name, price);
            }
            else
            {
                bakedFood = new Cake(name, price);
            }

            var result = string.Empty;

            if (food)
            {
                this.bakedFoods.Add(bakedFood);

                result = string.Format(OutputMessages.FoodAdded, name, type);
            }

            return result;
        }

        public string AddTable(string type, int tableNumber, int capacity)
        {
            var currentTable = Enum.TryParse(type, out TableType validTable);

            ITable table;

            if (validTable.ToString() == nameof(InsideTable))
            {
                table = new InsideTable(tableNumber, capacity);
            }
            else
            {
                table = new OutsideTable(tableNumber, capacity);
            }

            var result = string.Empty;

            if (currentTable)
            {
                this.tables.Add(table);

                result = string.Format(OutputMessages.TableAdded, tableNumber);
            }

            return result;
        }

        public string GetFreeTablesInfo()
        {
            var freeTables = this.tables.Where(x => !x.IsReserved);

            var sb = new StringBuilder();

            foreach (var table in freeTables)
            {
                sb.Append(table.GetFreeTableInfo());
            }

            return sb.ToString().TrimEnd();
        }

        public string GetTotalIncome()
        {
            var result = this.tables.Sum(x => x.GetBill());

            return string.Format(OutputMessages.TotalIncome, result);
        }

        public string LeaveTable(int tableNumber)
        {
            var table = this.tables.Find(x => x.TableNumber == tableNumber);

            var bill = table.GetBill();

            table.Clear();

            var sb = new StringBuilder();

            sb.AppendLine($"Table: {tableNumber}");

            sb.AppendLine($"Bill: {bill:f2}");

            return sb.ToString().TrimEnd();
        }

        public string OrderDrink(int tableNumber, string drinkName, string drinkBrand)
        {
            var table = this.tables.Find(x => x.TableNumber == tableNumber);

            var drink = this.drinks.Find(x => x.Name == drinkName);

            if (table == null)
            {
                return string.Format(OutputMessages.WrongTableNumber, tableNumber);
            }
            else if (drink == null)
            {
                return string.Format(OutputMessages.NonExistentDrink, drinkName,drinkBrand);
            }

            else
            {
                table.OrderDrink(drink);

                return string.Format(OutputMessages.DrinkOrderSuccessful, tableNumber, drinkName,drinkBrand);
            }
        }

        public string OrderFood(int tableNumber, string foodName)
        {
            var table = this.tables.Find(x => x.TableNumber == tableNumber);

            var food = this.bakedFoods.Find(x => x.Name == foodName);

            if (table == null)
            {
                return string.Format(OutputMessages.WrongTableNumber, tableNumber);
            }
            else if (food == null)
            {
                return string.Format(OutputMessages.NonExistentFood, foodName);
            }

            else
            {
                table.OrderFood(food);

                return string.Format(OutputMessages.FoodOrderSuccessful, tableNumber, foodName);
            }
        }

        public string ReserveTable(int numberOfPeople)
        {
            var isNotReseverTable = this.tables.FirstOrDefault(x => x.IsReserved == false && x.Capacity >= numberOfPeople);

            if (isNotReseverTable != null) 
            {
                isNotReseverTable.Reserve(numberOfPeople);

                return string.Format(OutputMessages.TableReserved, isNotReseverTable.TableNumber, numberOfPeople);
            }

            else
            {
                return string.Format(OutputMessages.ReservationNotPossible, numberOfPeople);
            }
        }
    }
}
