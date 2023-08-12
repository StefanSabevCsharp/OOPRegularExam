using BankLoan.Core.Contracts;
using BankLoan.Models;
using BankLoan.Models.Contracts;
using BankLoan.Repositories;
using BankLoan.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BankLoan.Core
{
    public class Controller : IController
    {
        private IRepository<IBank> banks;
        private IRepository<ILoan> loans;

        public Controller()
        {
            this.banks = new BankRepository();
            this.loans = new LoanRepository();
        }

       

        public string AddBank(string bankTypeName, string name)
        {
            if(bankTypeName == "CentralBank")
            {
                IBank bank = new CentralBank(name);
                //if there is a bank with the same name in the list should throw an exception
                //if(banks.Models.Any(x => x.Name == name))
                //{
                    
                //}
                banks.AddModel(bank);
                return $"{bankTypeName} is successfully added.";
            }
            else if(bankTypeName == "BranchBank")
            {
                IBank bank = new BranchBank(name);
                banks.AddModel(bank);
                return $"{bankTypeName} is successfully added.";
            }
            else
            {
                throw new ArgumentException("Invalid bank type.");
            }
        }

        public string AddClient(string bankName, string clientTypeName, string clientName, string id, double income)
        {
            
            if(clientTypeName != "Adult" && clientTypeName != "Student")
            {
                throw new ArgumentException("Invalid client type.");
            }


            IBank bank = banks.FirstModel(bankName);

           
           //moje bi tova tuk nqma da stane
            if (bank.GetType().Name == "CentralBank" && clientTypeName == "Student")
            {
                throw new ArgumentException("Unsuitable bank.");
            }
            else if (bank.GetType().Name == "BranchBank" && clientTypeName == "Adult")
            {
                throw new ArgumentException("Unsuitable bank.");
            }
            //do tuk

            if (clientTypeName == "Adult")
            {
                IClient client = new Adult(clientName, id, income);
                bank.AddClient(client);
                return $"{clientTypeName} successfully added to {bankName}.";
            }
            else
            {
                IClient client = new Student(clientName, id, income);
                bank.AddClient(client);
                return $"{clientTypeName} successfully added to {bankName}.";
            }


        }

        public string AddLoan(string loanTypeName)
        {
            if(loanTypeName == "MortgageLoan")
            {
                ILoan loan = new MortgageLoan();
                loans.AddModel(loan);
                return $"{loanTypeName} is successfully added.";
            }
            else if(loanTypeName == "StudentLoan")
            {
                ILoan loan = new StudentLoan();
                loans.AddModel(loan);
                return $"{loanTypeName} is successfully added.";
            }
            else
            {
                throw new ArgumentException("Invalid loan type.");
            }
        }

        public string FinalCalculation(string bankName)
        {
            IBank bank = banks.FirstModel(bankName);
            double funds = 0;
            foreach (IClient client in bank.Clients)
            {
                funds += client.Income;
            }
            foreach (ILoan loan in bank.Loans)
            {
                funds += loan.Amount;
            }
            return $"The funds of bank {bankName} are {funds:f2}.";


        }

        public string ReturnLoan(string bankName, string loanTypeName)
        {
            
            ILoan loan = loans.FirstModel(loanTypeName);
            IBank bank = banks.FirstModel(bankName);
            if(loan != null) //Probably to check if bank is null too

            {
                bank.AddLoan(loan);
                loans.RemoveModel(loan);
                return $"{loanTypeName} successfully added to {bankName}.";
            }
            else
            {
                throw new ArgumentException($"Loan of type {loanTypeName} is missing.");
            }
        }

        public string Statistics()
        {
            StringBuilder sb = new StringBuilder();
            foreach (IBank bank in banks.Models)
            {
               sb.AppendLine(bank.GetStatistics());
            }
            return sb.ToString().TrimEnd();
        }
    }
}
