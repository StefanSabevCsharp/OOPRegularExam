using BankLoan.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLoan.Models
{
    public abstract class Bank : IBank
    {
        private string name;
        private int capacity;
        private List<ILoan> loans;
        private List<IClient> clients;

        public Bank(string name, int capacity)
        {
            Name = name;
            Capacity = capacity;
            loans = new List<ILoan>();
            clients = new List<IClient>();
        }
        public string Name
        {
            get => name;

            private set
            {
                if(string.IsNullOrWhiteSpace(value))       //ALL NAMES ARE UNIQUE NE SUM GO NAPRAVIL
                {
                    throw new ArgumentException("Bank name cannot be null or empty.");
                }
               
                name = value;
            }
        }

        public int Capacity { get; private set; }

        public IReadOnlyCollection<ILoan> Loans => loans.AsReadOnly();

        public IReadOnlyCollection<IClient> Clients => clients.AsReadOnly();

        public void AddClient(IClient Client)
        {
            if(Clients.Count < Capacity)
            {
                clients.Add(Client);
            }
            else
            {
                throw new ArgumentException("Not enough capacity for this client.");
            }
        }

        public void AddLoan(ILoan loan)
        {
            loans.Add(loan);
        }

        public string GetStatistics()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"Name: {this.Name}, Type: {this.GetType().Name}");
            
            if (Clients.Count > 0)
            {
                List<string> list = new List<string>();
                foreach (var item in Clients)
                {
                    list.Add(item.Name);
                }
                sb.AppendLine($"Clients: {string.Join(", ", list)}");
            }
            else
            {
                sb.AppendLine("Clients: none");
            }
            
            sb.AppendLine($"Loans: {Loans.Count}, Sum of Rates: {SumRates()}");

            return sb.ToString().TrimEnd();

        }

        public void RemoveClient(IClient Client)
        {
           clients.Remove(Client);
        }

        public double SumRates()
        {
            double totalSumInterest = 0;

            foreach(var item in Loans)      //moje bi trqbva da polzvam poleto Interest ot ILoan
            {
                totalSumInterest += item.InterestRate;
            }
            return totalSumInterest;
        }
    }
}
