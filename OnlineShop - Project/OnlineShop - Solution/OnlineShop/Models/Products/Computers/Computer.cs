namespace OnlineShop.Models.Products.Computers
{
using System;

using System.Linq;

using System.Text;

using System.Collections.Generic;

using OnlineShop.Common.Constants;

using OnlineShop.Models.Products.Components;
using OnlineShop.Models.Products.Peripherals;
    public abstract class Computer : Product, IComputer
    {
        private readonly List<IComponent> components;
        private readonly List<IPeripheral> peripherals;
       
        protected Computer(int id, string manufacturer, string model, decimal price, double overallPerformance)
            : base(id, manufacturer, model, price, overallPerformance)
        {
            this.components = new List<IComponent>();
            this.peripherals = new List<IPeripheral>();
            
        }

        public override double OverallPerformance
            =>!this.components.Any()?
            base.OverallPerformance
            :base.OverallPerformance+ this.Components.Average(x => x.OverallPerformance);

        public override decimal Price 
            => base.Price + this.Components.Sum(x => x.Price) + this.Peripherals.Sum(x => x.Price);
        public IReadOnlyCollection<IComponent> Components
            => this.components.AsReadOnly();
        public IReadOnlyCollection<IPeripheral> Peripherals
            => this.peripherals.AsReadOnly();

        public void AddComponent(IComponent component)
        {
            if (this.Components.Any(x=>x==component))
            {
                throw new ArgumentException(string.Format(ExceptionMessages.ExistingComponent, component.GetType().Name, this.GetType().Name, this.Id));
            }
            this.components.Add(component);
        }
        public void AddPeripheral(IPeripheral peripheral)
        {
            if (this.Peripherals.Any(x=>x==peripheral))
            {
                throw new ArgumentException(string.Format(ExceptionMessages.ExistingPeripheral, peripheral.GetType().Name, this.GetType().Name, this.Id));
            }
            this.peripherals.Add(peripheral);
        }
        public IComponent RemoveComponent(string componentType)
        {
            var currentComponent = this.components.FirstOrDefault(x => x.GetType().Name == componentType);
            if ((!this.Components.Any()) || (currentComponent == null))
            {
                throw new ArgumentException(string.Format(ExceptionMessages.NotExistingComponent, componentType, this.GetType().Name, this.Id));
            }
            this.components.Remove(currentComponent);
            return currentComponent;
        }
        public IPeripheral RemovePeripheral(string peripheralType)
        {
            var currentPeripheral = this.peripherals.FirstOrDefault(x => x.GetType().Name == peripheralType);
            if ((!this.Peripherals.Any()) || (currentPeripheral == null))
            {
                throw new ArgumentException(string.Format(ExceptionMessages.NotExistingPeripheral, peripheralType, this.GetType().Name, this.Id));
            }
            this.peripherals.Remove(currentPeripheral);
            return currentPeripheral;
        }
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Overall Performance: {this.OverallPerformance:f2}. Price: {this.Price:f2} - {this.GetType().Name}: {this.Manufacturer} {this.Model} (Id: {this.Id})");

            sb.AppendLine($"Components ({this.components.Count}):");

            if (this.components.Any())
            {

                foreach (var item in this.components)
                {
                    sb.AppendLine(item.ToString());
                }
            }
            if (this.peripherals.Any())
            {
            sb.AppendLine($"Peripherals ({this.peripherals.Count}); Average Overall Performace ({this.peripherals.Average(x => x.OverallPerformance):f2}):");

                foreach (var item in this.peripherals)
                {
                    sb.AppendLine(item.ToString());
                }
            }
            else
            {
                sb.AppendLine("Peripherals (0); Average Overall Performace (0.00)");
            }
            return sb.ToString().TrimEnd();
        }
    }
}
