using RobotService.Core.Contracts;
using RobotService.Models.Garages;
using RobotService.Models.Procedures;
using RobotService.Models.Procedures.Contracts;
using RobotService.Models.Robots;
using RobotService.Models.Robots.Contracts;
using RobotService.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RobotService.Core
{
    public class Controller : IController
    {
        private Garage garage;
        private Charge charge;
        private Chip chip;
        private Polish polish;
        private Rest rest;
        private TechCheck techCheck;
        private Work work;

        private List<IProcedure> procedures;

        public Controller()
        {
            this.garage = new Garage();

            this.charge = new Charge();
            this.chip = new Chip();
            this.polish = new Polish();
            this.rest = new Rest();
            this.techCheck = new TechCheck();
            this.work = new Work();

            this.procedures = new List<IProcedure>()
            {
                this.charge, this.chip, this.polish, this.rest, this.techCheck, this.work
            };

        }

        public string Charge(string robotName, int procedureTime)
        {
            if (CheckIfRobotExists(robotName) == false)
            {
                throw new ArgumentException(string.Format(ExceptionMessages.InexistingRobot, robotName));
            }

            IRobot robot = this.garage.Robots.Values.FirstOrDefault(x => x.Name == robotName);

            this.charge.DoService(robot, procedureTime);

            return string.Format(OutputMessages.ChargeProcedure, robot.Name);
        }



        public string Chip(string robotName, int procedureTime)
        {
            if (CheckIfRobotExists(robotName) == false)
            {
                throw new ArgumentException(string.Format(ExceptionMessages.InexistingRobot, robotName));
            }

            IRobot robot = this.garage.Robots.Values.FirstOrDefault(x => x.Name == robotName);

            this.chip.DoService(robot, procedureTime);

            return string.Format(OutputMessages.ChipProcedure, robot.Name);
        }

        public string History(string procedureType)
        {
            IProcedure procedure = this.procedures.FirstOrDefault(x => x.GetType().Name == procedureType);

            return procedure.History();
        }

        public string Manufacture(string robotType, string name, int energy, int happiness, int procedureTime)
        {
            if (robotType != "HouseholdRobot" && robotType != "PetRobot" && robotType != "WalkerRobot")
            {
                throw new ArgumentException(string.Format(ExceptionMessages.InvalidRobotType, robotType));
            }

            IRobot robot;

            if (robotType == "HouseholdRobot")
            {
                robot = new HouseholdRobot(name, energy, happiness, procedureTime);
            }
            else if (robotType == "PetRobot")
            {
                robot = new PetRobot(name, energy, happiness, procedureTime);
            }
            else
            {
                robot = new WalkerRobot(name, energy, happiness, procedureTime);
            }

            this.garage.Manufacture(robot);

            return string.Format(OutputMessages.RobotManufactured, robot.Name);
        }

        public string Polish(string robotName, int procedureTime)
        {
            if (CheckIfRobotExists(robotName) == false)
            {
                throw new ArgumentException(string.Format(ExceptionMessages.InexistingRobot, robotName));
            }

            IRobot robot = this.garage.Robots.Values.FirstOrDefault(x => x.Name == robotName);

            this.polish.DoService(robot, procedureTime);

            return String.Format(OutputMessages.PolishProcedure, robot.Name);
        }

        public string Rest(string robotName, int procedureTime)
        {
            if (CheckIfRobotExists(robotName) == false)
            {
                throw new ArgumentException(string.Format(ExceptionMessages.InexistingRobot, robotName));
            }

            IRobot robot = this.garage.Robots.Values.FirstOrDefault(x => x.Name == robotName);

            this.rest.DoService(robot, procedureTime);

            return string.Format(OutputMessages.RestProcedure, robot.Name);
        }

        public string Sell(string robotName, string ownerName)
        {
            if (CheckIfRobotExists(robotName) == false)
            {
                throw new ArgumentException(string.Format(ExceptionMessages.InexistingRobot, robotName));
            }

            IRobot robot = this.garage.Robots.Values.FirstOrDefault(x => x.Name == robotName);

            this.garage.Sell(robot.Name, ownerName);

            if (robot.IsChipped)
            {
                return string.Format(OutputMessages.SellChippedRobot, ownerName);
            }
            else
            {
                return string.Format(OutputMessages.SellNotChippedRobot, ownerName);
            }
        }

        public string TechCheck(string robotName, int procedureTime)
        {
            if (CheckIfRobotExists(robotName) == false)
            {
                throw new ArgumentException(string.Format(ExceptionMessages.InexistingRobot, robotName));
            };

            IRobot robot = this.garage.Robots.Values.FirstOrDefault(x => x.Name == robotName);

            this.techCheck.DoService(robot, procedureTime);

            return string.Format(OutputMessages.TechCheckProcedure, robot.Name);
        }

        public string Work(string robotName, int procedureTime)
        {
            if (CheckIfRobotExists(robotName) == false)
            {
                throw new ArgumentException(string.Format(ExceptionMessages.InexistingRobot, robotName));
            }

            IRobot robot = this.garage.Robots.Values.FirstOrDefault(x => x.Name == robotName);

            this.work.DoService(robot, procedureTime);

            return string.Format(OutputMessages.WorkProcedure, robot.Name, procedureTime);
        }

        private bool CheckIfRobotExists(string robotName)
        {
            IRobot robot = this.garage.Robots.Values.FirstOrDefault(x => x.Name == robotName);

            if (robot == null)
            {
                return false;               
            }

            return true;
        }
    }
}
