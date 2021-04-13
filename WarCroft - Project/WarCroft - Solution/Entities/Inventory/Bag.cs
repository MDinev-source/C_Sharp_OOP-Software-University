namespace WarCroft.Entities.Inventory
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using WarCroft.Constants;
    using WarCroft.Entities.Items;

    public abstract class Bag : IBag
    {
        private readonly List<Item> items;
        protected Bag(int capacity)
        {
            this.Capacity = capacity;
            this.items = new List<Item>();
        }

        public int Capacity { get; set; } = 100;
        public int Load
            => this.Items.Sum(x => x.Weight);
        public IReadOnlyCollection<Item> Items
            => this.items.AsReadOnly();

        public void AddItem(Item item)
        {
            var sum = item.Weight + this.Load;

            if (sum > this.Capacity)
            {
                throw new InvalidOperationException(ExceptionMessages.EmptyBag);
            }

            this.items.Add(item);
        }
        public Item GetItem(string name)
        {
            if (this.Items.Count == 0)
            {
                throw new InvalidOperationException(ExceptionMessages.EmptyBag);
            }

            var item = this.Items.FirstOrDefault(x => x.GetType().Name == name);

            if (item == null)
            {
                throw new ArgumentException(String.Format(ExceptionMessages.ItemNotFoundInBag, name));
            }

            this.items.Remove(item);

            return item;
        }
    }
}
