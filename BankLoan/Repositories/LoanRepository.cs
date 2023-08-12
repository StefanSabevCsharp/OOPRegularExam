using BankLoan.Models.Contracts;
using BankLoan.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLoan.Repositories
{
    public class LoanRepository : IRepository<ILoan>
    {
        private readonly List<ILoan> loansList;
        public LoanRepository()
        {
            this.loansList = new List<ILoan>();
        }

        public IReadOnlyCollection<ILoan> Models => loansList.AsReadOnly();

        public void AddModel(ILoan model)
        {
            loansList.Add(model);
        }

        public ILoan FirstModel(string name)
        {

            ILoan loan = loansList.FirstOrDefault(x => x.GetType().Name == name);
            //moje bi ne trqbva da e gettype a mi napravo name kakto pri bank
            return loan;
        }

        public bool RemoveModel(ILoan model)
        {

            if (loansList.Contains(model))
            {
                loansList.Remove(model);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
