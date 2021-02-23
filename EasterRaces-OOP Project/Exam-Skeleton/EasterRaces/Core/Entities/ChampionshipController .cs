using EasterRaces.Core.Contracts;
using EasterRaces.Models.Cars.Contracts;
using EasterRaces.Models.Cars.Entities;
using EasterRaces.Models.Drivers;
using EasterRaces.Models.Drivers.Contracts;
using EasterRaces.Models.Races.Contracts;
using EasterRaces.Models.Races.Entities;
using EasterRaces.Repositories.Contracts;
using EasterRaces.Repositories.Entities;
using EasterRaces.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasterRaces.Core.Entities
{
    public class ChampionshipController : IChampionshipController
    {
        private IRepository<ICar> carsRepository;
        private IRepository<IDriver> driversRepository;
        private IRepository<IRace> racesRepocitory;

        public ChampionshipController()
        {
            this.carsRepository = new CarRepository();
            this.driversRepository = new DriverRepository();
            this.racesRepocitory = new RaceRepository();
        }
        public string AddCarToDriver(string driverName, string carModel)
        {
            var currentDriver = driversRepository.GetByName(driverName);
            var currentCar = carsRepository.GetByName(carModel);

            if (currentDriver == null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.DriverNotFound, driverName));
            }
            if (currentCar == null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.CarNotFound, carModel));
            }
            currentDriver.AddCar(currentCar);

            return string.Format(OutputMessages.CarAdded, driverName, carModel);
        }

        public string AddDriverToRace(string raceName, string driverName)
        {
            var currentDriver = driversRepository.GetByName(driverName);
            var currentRace = racesRepocitory.GetByName(raceName);

            if (currentDriver == null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.DriverNotFound, driverName));
            }
            if (currentRace == null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.RaceNotFound, raceName));
            }
            currentRace.AddDriver(currentDriver);

            return string.Format(OutputMessages.DriverAdded, driverName, raceName);
        }

        public string CreateCar(string type, string model, int horsePower)
        {
            var currentCar = carsRepository.GetByName(model);
            if (currentCar != null)
            {
                throw new ArgumentException(string.Format(ExceptionMessages.CarExists, model));
            }
            if (type + "Car" == typeof(MuscleCar).Name)
            {
                currentCar = new MuscleCar(model, horsePower);
            }
            else if (type+"Car"==typeof(SportsCar).Name)
            {
                currentCar = new SportsCar(model, horsePower);
            }
            carsRepository.Add(currentCar);

            return string.Format(OutputMessages.CarCreated, currentCar.GetType().Name, model);
        }

        public string CreateDriver(string driverName)
        {
            var currentDriver = driversRepository.GetByName(driverName);
            if (currentDriver != null)
            {
                throw new ArgumentException(string.Format(ExceptionMessages.DriversExists, driverName));
            }
            currentDriver = new Driver(driverName);
            driversRepository.Add(currentDriver);
            return string.Format(OutputMessages.DriverCreated, driverName);
        }

        public string CreateRace(string name, int laps)
        {
            var currentRace = racesRepocitory.GetByName(name);
            if (currentRace != null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.RaceExists, name));
            }
            currentRace = new Race(name, laps);
            racesRepocitory.Add(currentRace);
            return string.Format(OutputMessages.RaceCreated, name);
        }

        public string StartRace(string raceName)
        {
            var currentRace = racesRepocitory.GetByName(raceName);

            if (currentRace == null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.RaceNotFound, raceName));
            }

            if (currentRace.Drivers.Count < 3)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.RaceInvalid, raceName, 3));
            }

            var allDrivers = currentRace.Drivers.OrderByDescending(x => x.Car.CalculateRacePoints(currentRace.Laps)).Where(x => x.CanParticipate).Take(3);


            var sb = new StringBuilder();
            var count = 1;

            foreach (var driver in allDrivers)
            {
                if (count == 1)
                {
                    driver.WinRace();
                    sb.AppendLine(string.Format(OutputMessages.DriverFirstPosition, driver.Name, raceName));
                }
                else if (count == 2)
                {
                    sb.AppendLine(string.Format(OutputMessages.DriverSecondPosition, driver.Name, raceName));
                }
                else
                {
                    sb.AppendLine(string.Format(OutputMessages.DriverThirdPosition, driver.Name, raceName));
                }
                count++;
            }
            racesRepocitory.Remove(currentRace);
            return sb.ToString().Trim();
        }
    }
}
