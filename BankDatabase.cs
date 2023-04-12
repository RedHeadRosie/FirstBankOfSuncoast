using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;

namespace FirstBankOfSuncoast
{
    class BankDatabase
    {
        private List<Transaction> Transactions { get; set; } = new List<Transaction>();

        private string FileName = "transactions.csv";

        public void LoadTransactions()
        {
            if (File.Exists(FileName))
            {
                var fileReader = new StreamReader(FileName);
                var csvReader = new CsvReader(fileReader, CultureInfo.InvariantCulture);
                Transactions = csvReader.GetRecords<Transaction>().ToList();
                fileReader.Close();
            }
        }

        public void SaveTransactions()
        {
            var fileWriter = new StreamWriter(FileName);
            var csvWriter = new CsvWriter(fileWriter, CultureInfo.InvariantCulture);
            csvWriter.WriteRecords(Transactions);
            fileWriter.Close();
        }

        public void AddTransaction(Transaction newTransaction)
        {
            Transactions.Add(newTransaction);
        }

        public List<Transaction> GetAllTransactions()
        {
            return Transactions;
        }

        //method needed to find all savings or all checking transactions
        public List<Transaction> FindAccountType(string nameToFind)
        {
            List<Transaction> foundCheckingOrSavings = Transactions.FindAll(transaction => transaction.TypeOfAccount.Contains(nameToFind));

            return foundCheckingOrSavings;
        }

        //method needed to add up transactions for balance and to check not negative
    }
}
