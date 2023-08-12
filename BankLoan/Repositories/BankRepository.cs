using BankLoan.Models.Contracts;
using BankLoan.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLoan.Repositories
{
    public class BankRepository : IRepository<IBank>
    {
        private readonly List<IBank> banks;
       
        public BankRepository()
        {
            this.banks = new List<IBank>();
        }
        public IReadOnlyCollection<IBank> Models => banks.AsReadOnly();

        public void AddModel(IBank model)
        {
            banks.Add(model);
        }

        public IBank FirstModel(string name)
        {
            //IBank bank = banks.FirstOrDefault(x => x.GetType().Name == name);
            IBank bank = banks.FirstOrDefault(x => x.Name == name);
            return bank;
        }

        public bool RemoveModel(IBank model)
        {
            if(banks.Contains(model))
            {
                banks.Remove(model);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
