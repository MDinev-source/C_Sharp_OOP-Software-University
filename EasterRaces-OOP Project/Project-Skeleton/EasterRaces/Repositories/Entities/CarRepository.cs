namespace EasterRaces.Repositories.Entities
{

    using System.Linq;

    using System.Collections.Generic;

    using EasterRaces.Models.Cars.Contracts;

    using EasterRaces.Repositories.Contracts;


    public class CarRepository : IRepository<ICar>
    {
        private readonly List<ICar> cars;
        public CarRepository()
        {
            this.cars = new List<ICar>();
        }
        public IReadOnlyCollection<ICar> Cars
            => this.cars.AsReadOnly();

        public void Add(ICar model)
        {
            this.cars.Add(model);
        }

        public IReadOnlyCollection<ICar> GetAll()
        {
            return Cars;
        }

        public ICar GetByName(string name)
        {
            return cars.FirstOrDefault(x => x.Model == name);
        }

        public bool Remove(ICar model)
        {
            return this.cars.Remove(model);
        }
    }
}
