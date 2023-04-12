using System;
using System.Collections.Generic;

namespace FirstBankOfSuncoast
{
    class Program
    {

        static string PromptForString(string prompt)
        {
            Console.Write(prompt);
            var userInput = Console.ReadLine();

            return userInput;
        }

        static int PromptForInteger(string prompt)
        {
            Console.Write(prompt);
            int userInput;
            var isThisGoodInput = Int32.TryParse(Console.ReadLine(), out userInput);

            if (isThisGoodInput)
            {
                return userInput;
            }
            else
            {
                Console.WriteLine("Sorry, that isn't a valid input, I'm using 0 as your answer.");
                return 0;
            }
        }

        static void Main(string[] args)
        {
            var database = new BankDatabase();
            database.LoadTransactions();

            Console.WriteLine("Welcome to the First Bank of Suncoast.");
            Console.WriteLine();


            var keepGoing = true;
            while (keepGoing)
            {
                Console.WriteLine("What would you like to do?");
                Console.WriteLine("If you would like to make changes to your Savings or Checking Account, please type C. ");
                Console.WriteLine("If you would like to check the balance of your Savings or Checking Account, please type B. ");
                Console.WriteLine("If you would like to see all of the Savings or Checking transactions, please type V. ");
                Console.WriteLine("If you would like to Quit, please type Q. ");
                Console.WriteLine();
                var choice = Console.ReadLine();

                var transactionToAdd = new Transaction();
                string accountType;
                string actionChoice;
                string accountForOutput = "error";
                int balance = 0;

                switch (choice)
                {
                    case "Q":
                        keepGoing = false;
                        break;
                    case "C":
                        accountType = PromptForString("Which account would you like to make changes to? (S)avings or (C)hecking? ");

                        //Check that the balance isn't below 0. Only update the database if it's equal to or greater than 0.

                        if (accountType == "S" || accountType == "s")
                        {
                            transactionToAdd.TypeOfAccount = "savings";
                            accountForOutput = "savings";

                            balance = 0;
                            var onlySavingsTransactions = new List<Transaction>();
                            accountForOutput = "savings";
                            onlySavingsTransactions = database.FindAccountType(accountForOutput);

                            foreach (var transaction in onlySavingsTransactions)
                            {

                                if (transaction.TypeOfAction == "deposit")
                                {
                                    balance = balance + transaction.Amount;
                                }
                                if (transaction.TypeOfAction == "withdraw")
                                {
                                    balance = balance - transaction.Amount;
                                }
                            }


                        }
                        if (accountType == "C" || accountType == "c")
                        {
                            transactionToAdd.TypeOfAccount = "checking";
                            accountForOutput = "checking";

                            balance = 0;
                            var onlyCheckingTransactions = new List<Transaction>();
                            accountForOutput = "checking";
                            onlyCheckingTransactions = database.FindAccountType(accountForOutput);
                            foreach (var transaction in onlyCheckingTransactions)
                            {
                                if (transaction.TypeOfAction == "deposit")
                                {
                                    balance = balance + transaction.Amount;
                                }
                                if (transaction.TypeOfAction == "withdraw")
                                {
                                    balance = balance - transaction.Amount;
                                }

                            }

                        }
                        Console.WriteLine($"Your balance is {balance}.");

                        Console.WriteLine();
                        actionChoice = PromptForString("Would you like to (W)ithdraw or (D)eposit? ");

                        if (actionChoice == "W" || actionChoice == "w")
                        {
                            transactionToAdd.TypeOfAction = "withdraw";
                            transactionToAdd.Amount = PromptForInteger($"Please type the amount you would like to withdraw from your {accountForOutput} account: ");
                            balance = balance - transactionToAdd.Amount;

                        }
                        if (actionChoice == "D" || actionChoice == "d")
                        {
                            transactionToAdd.TypeOfAction = "deposit";
                            transactionToAdd.Amount = PromptForInteger($"Please type the amount you would like to deposit to your {accountForOutput} account: ");
                            balance = balance + transactionToAdd.Amount;
                        }
                        Console.WriteLine();




                        if (balance >= 0)
                        {
                            database.AddTransaction(transactionToAdd);
                            database.SaveTransactions();
                        }
                        else
                        {
                            Console.WriteLine($"You do not have enough funds to make these changes, sorry.");
                        }
                        break;
                    case "B":
                        accountType = PromptForString("Which account would you like to look at? (S)avings or (C)hecking? ");
                        if (accountType == "S" || accountType == "s")
                        {
                            balance = 0;
                            var onlySavingsTransactions = new List<Transaction>();
                            accountForOutput = "savings";
                            onlySavingsTransactions = database.FindAccountType(accountForOutput);

                            //Console.WriteLine("The savings transactions are: ");
                            foreach (var transaction in onlySavingsTransactions)
                            {
                                //Console.WriteLine(transaction.TypeOfAccount);
                                //Console.WriteLine(transaction.TypeOfAction);
                                //Console.WriteLine(transaction.Amount);

                                if (transaction.TypeOfAction == "deposit")
                                {
                                    balance = balance + transaction.Amount;
                                }
                                if (transaction.TypeOfAction == "withdraw")
                                {
                                    balance = balance - transaction.Amount;
                                }

                            }
                            Console.WriteLine($"Your balance is {balance}.");

                        }
                        if (accountType == "C" || accountType == "c")
                        {
                            balance = 0;
                            var onlyCheckingTransactions = new List<Transaction>();
                            accountForOutput = "checking";
                            onlyCheckingTransactions = database.FindAccountType(accountForOutput);

                            //Console.WriteLine("The checking transactions are: ");
                            foreach (var transaction in onlyCheckingTransactions)
                            {
                                //Console.WriteLine(transaction.TypeOfAccount);
                                //Console.WriteLine(transaction.TypeOfAction);
                                //Console.WriteLine(transaction.Amount);

                                if (transaction.TypeOfAction == "deposit")
                                {
                                    balance = balance + transaction.Amount;
                                }
                                if (transaction.TypeOfAction == "withdraw")
                                {
                                    balance = balance - transaction.Amount;
                                }
                            }
                            Console.WriteLine($"Your balance is {balance}.");

                        }
                        break;
                    case "V":
                        accountType = PromptForString("Which account would you like to look at? (S)avings or (C)hecking? ");
                        if (accountType == "S" || accountType == "s")
                        {
                            var onlySavingsTransactions = new List<Transaction>();
                            accountForOutput = "savings";
                            onlySavingsTransactions = database.FindAccountType(accountForOutput);

                            Console.WriteLine("The savings transactions are: ");
                            foreach (var transaction in onlySavingsTransactions)
                            {
                                Console.WriteLine($"There was a {transaction.TypeOfAction} of {transaction.Amount}");
                            }

                        }
                        if (accountType == "C" || accountType == "c")
                        {
                            var onlyCheckingTransactions = new List<Transaction>();
                            accountForOutput = "Checking";
                            onlyCheckingTransactions = database.FindAccountType(accountForOutput);

                            Console.WriteLine("The checking transactions are: ");
                            foreach (var transaction in onlyCheckingTransactions)
                            {
                                Console.WriteLine($"There was a {transaction.TypeOfAction} of {transaction.Amount}");
                            }

                        }
                        Console.WriteLine();
                        break;

                    default:
                        Console.WriteLine("NOPE! ☠️");
                        break;
                }

            }
        }
    }
}
