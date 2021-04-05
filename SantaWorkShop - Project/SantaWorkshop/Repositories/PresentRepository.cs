using SantaWorkshop.Models.Presents.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SantaWorkshop.Repositories.Contracts
{
    public class PresentRepository : IRepository<IPresent>
    {
        private List<IPresent> presents;

        public PresentRepository()
        {
            this.presents = new List<IPresent>();
        }
        public IReadOnlyCollection<IPresent> Models
            => this.presents.AsReadOnly();

        public void Add(IPresent model)
        {
            presents.Add(model);
        }

        public IPresent FindByName(string name)
        {
            var currentPresent = presents.FirstOrDefault(x => x.Name == name);
            return currentPresent;
        }

        public bool Remove(IPresent model)
        {
            return presents.Remove(model);
        }
    }
}
