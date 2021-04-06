using RobotService.Models.Robots.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace RobotService.Models.Robots
{
    public class HouseholdRobot : Robot, IRobot
    {
        public HouseholdRobot(string name, int energy, int happiness, int procedureTime) 
            : base(name, energy, happiness, procedureTime)
        {

        }
    }
}
