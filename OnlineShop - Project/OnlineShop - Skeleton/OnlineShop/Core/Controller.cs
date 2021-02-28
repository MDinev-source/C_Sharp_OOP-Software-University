namespace OnlineShop.Core
{
    using System;

    using System.Linq;

    using System.Collections.Generic;

    using OnlineShop.Common.Constants;
    using OnlineShop.Common.Enums;

    using OnlineShop.Models.Products.Components;
    using OnlineShop.Models.Products.Peripherals;
    using OnlineShop.Models.Products.Computers;

    public class Controller : IController
    {
        private List<IComponent> components;
        private List<IComputer> computers;
        private List<IPeripheral> peripherals;

        public Controller()
        {
            this.components = new List<IComponent>();
            this.computers = new List<IComputer>();
            this.peripherals = new List<IPeripheral>();
        }
        public string AddComponent(int computerId, int id, string componentTypeName, string manufacturer, string model, decimal price, double overallPerformance, int generation)
        {

            var currentComputer = this.computers.FirstOrDefault(x => x.Id == computerId);
            NonExistingComputeId(currentComputer);

            if (!Enum.TryParse(componentTypeName, out ComponentType componentType))
            {
                throw new ArgumentException(ExceptionMessages.InvalidComponentType);
            }
            if (this.components.Any(x => x.Id == id))
            {
                throw new ArgumentException(ExceptionMessages.ExistingComponentId);
            }

            IComponent component = null;

            switch (componentType)
            {
                case ComponentType.CentralProcessingUnit:
                    component = new CentralProcessingUnit(id, manufacturer, model, price, overallPerformance, generation);
                    break;
                case ComponentType.Motherboard:
                    component = new Motherboard(id, manufacturer, model, price, overallPerformance, generation);
                    break;
                case ComponentType.PowerSupply:
                    component = new PowerSupply(id, manufacturer, model, price, overallPerformance, generation);
                    break;
                case ComponentType.RandomAccessMemory:
                    component = new RandomAccessMemory(id, manufacturer, model, price, overallPerformance, generation);
                    break;
                case ComponentType.SolidStateDrive:
                    component = new SolidStateDrive(id, manufacturer, model, price, overallPerformance, generation);
                    break;
                case ComponentType.VideoCard:
                    component = new VideoCard(id, manufacturer, model, price, overallPerformance, generation);
                    break;
            }


            currentComputer.AddComponent(component);
            components.Add(component);

            return string.Format(SuccessMessages.AddedComponent, componentTypeName, id, computerId);
        }


        public string AddComputer(string computerTypeName, int id, string manufacturer, string model, decimal price)
        {
            if (this.computers.Any(x => x.Id == id))
            {
                throw new ArgumentException(ExceptionMessages.ExistingComputerId);
            }


            if (!Enum.TryParse(computerTypeName, out ComputerType computerType))
            {
                throw new ArgumentException(ExceptionMessages.InvalidComputerType);
            }

            IComputer computer = null;
            switch (computerType)
            {
                case ComputerType.DesktopComputer:
                    computer = new DesktopComputer(id, manufacturer, model, price);
                    break;
                case ComputerType.Laptop:
                    computer = new Laptop(id, manufacturer, model, price);
                    break;
            }

            computers.Add(computer);
            return string.Format(SuccessMessages.AddedComputer, id);
        }

        public string AddPeripheral(int computerId, int id, string peripheralTypeName, string manufacturer, string model, decimal price, double overallPerformance, string connectionType)
        {
            var currentComputer = this.computers.FirstOrDefault(x => x.Id == computerId);
            NonExistingComputeId(currentComputer);

            if (!Enum.TryParse(peripheralTypeName, out PeripheralType peripheralType))
            {
                throw new ArgumentException(ExceptionMessages.InvalidPeripheralType);
            }
            if (peripherals.Any(x => x.Id == id))
            {
                throw new ArgumentException(ExceptionMessages.ExistingPeripheralId);
            }
            IPeripheral peripheral = null;
            switch (peripheralType)
            {
                case PeripheralType.Headset:
                    peripheral = new Headset(id, manufacturer, model, price, overallPerformance, connectionType);
                    break;
                case PeripheralType.Keyboard:
                    peripheral = new Keyboard(id, manufacturer, model, price, overallPerformance, connectionType);
                    break;
                case PeripheralType.Monitor:
                    peripheral = new Monitor(id, manufacturer, model, price, overallPerformance, connectionType);
                    break;
                case PeripheralType.Mouse:
                    peripheral = new Mouse(id, manufacturer, model, price, overallPerformance, connectionType);
                    break;

            }
            peripherals.Add(peripheral);
            currentComputer.AddPeripheral(peripheral);

            return string.Format(SuccessMessages.AddedPeripheral, peripheralTypeName, id, computerId);

        }

        public string BuyBest(decimal budget)
        {
            var computerArr = this.computers.Where(x => x.Price <= budget);
            var currentComputer = computerArr.OrderBy(x => x.OverallPerformance).FirstOrDefault();

            if (currentComputer == null)
            {
                throw new ArgumentException(string.Format(ExceptionMessages.CanNotBuyComputer, budget));
            }

            computers.Remove(currentComputer);
            var result = currentComputer.ToString();
            return result;
        }

        public string BuyComputer(int id)
        {
            var currentComputer = this.computers.FirstOrDefault(x => x.Id == id);
            NonExistingComputeId(currentComputer);

            computers.Remove(currentComputer);
            var result = currentComputer.ToString();
            return result;
        }

        public string GetComputerData(int id)
        {
            var currentComputer = computers.FirstOrDefault(x => x.Id == id);
            NonExistingComputeId(currentComputer);


            return currentComputer.ToString();
        }

        public string RemoveComponent(string componentType, int computerId)
        {
            var currentComputer = computers.FirstOrDefault(x => x.Id == computerId);
            NonExistingComputeId(currentComputer);

            var component = currentComputer.RemoveComponent(componentType);

            this.components.Remove(component);

            return string.Format(SuccessMessages.RemovedComponent, componentType, component.Id);
        }

        public string RemovePeripheral(string peripheralType, int computerId)
        {
            var currentComputer = computers.FirstOrDefault(x => x.Id == computerId);
            NonExistingComputeId(currentComputer);

            var peripheral = currentComputer.RemovePeripheral(peripheralType);

            this.peripherals.Remove(peripheral);

            return string.Format(SuccessMessages.RemovedPeripheral, peripheralType, peripheral.Id);
        }
        private static void NonExistingComputeId(IComputer currentComputer)
        {
            if (currentComputer == null)
            {
                throw new ArgumentException(ExceptionMessages.NotExistingComputerId);
            }
        }
    }
}
