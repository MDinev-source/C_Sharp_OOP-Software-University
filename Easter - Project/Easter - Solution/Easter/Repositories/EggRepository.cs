namespace Easter.Repositories
{
    using Easter.Models.Eggs.Contracts;
    using Easter.Repositories.Contracts;
    using System.Collections.Generic;
    using System.Linq;
    public class EggRepository:IRepository<IEgg>
    {
        private readonly List<IEgg> eggs;
        public EggRepository()
        {
            this.eggs = new List<IEgg>();
        }
        public IReadOnlyCollection<IEgg> Models
            => this.eggs.AsReadOnly();

        public void Add(IEgg model)
        {
            this.eggs.Add(model);
        }

        public IEgg FindByName(string name)
        {
            return this.eggs.FirstOrDefault(x => x.Name == name);
        }

        public bool Remove(IEgg model)
        {
            return this.eggs.Remove(model);
        }
    }
}
