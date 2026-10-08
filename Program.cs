using System;
using System.IO;

namespace JournalProgram
{
    // Exceeds the basic requirements by adding a random prompt generator, simple entry tracking,
    // and a low-risk file format using the ~|~ separator so entries are easy to save and load.
    public class Program
    {
        static void Main(string[] args)
        {
            Journal journal = new Journal();
            PromptGenerator promptGenerator = new PromptGenerator();

            Console.WriteLine("========================================");
            Console.WriteLine("Welcome to the Journal Program");
            Console.WriteLine("========================================");

            bool running = true;

            while (running)
            {
                Console.WriteLine();
                Console.WriteLine("1. Write a new entry");
                Console.WriteLine("2. Display the journal");
                Console.WriteLine("3. Save the journal");
                Console.WriteLine("4. Load the journal");
                Console.WriteLine("5. Exit");
                Console.Write("Choose an option: ");

                string choice = Console.ReadLine() ?? "";

                switch (choice)
                {
                    case "1":
                        WriteNewEntry(journal, promptGenerator);
                        break;

                    case "2":
                        DisplayJournal(journal);
                        break;

                    case "3":
                        SaveJournal(journal);
                        break;

                    case "4":
                        LoadJournal(journal);
                        break;

                    case "5":
                        running = false;
                        break;
                }
            }
        }

        static void WriteNewEntry(Journal journal, PromptGenerator promptGenerator)
        {
            string prompt = promptGenerator.GetRandomPrompt();
            Console.WriteLine();
            Console.WriteLine("Prompt:");
            Console.WriteLine(prompt);
            Console.Write("Your response: ");
            string response = Console.ReadLine() ?? "";

            if (string.IsNullOrWhiteSpace(response))
            {
                return;
            }

            string date = DateTime.Now.ToShortDateString();
            Entry entry = new Entry(date, prompt, response);
            journal.AddEntry(entry);
        }

        static void DisplayJournal(Journal journal)
        {
            Console.WriteLine();
            Console.WriteLine("Journal Entries:");
            journal.DisplayAll();
        }

        static void SaveJournal(Journal journal)
        {
            Console.Write("Filename: ");
            string fileName = Console.ReadLine() ?? "journal.txt";

            try
            {
                journal.SaveToFile(fileName);
            }
            catch (Exception)
            {
            }
        }

        static void LoadJournal(Journal journal)
        {
            Console.Write("Filename: ");
            string fileName = Console.ReadLine() ?? "journal.txt";

            try
            {
                journal.LoadFromFile(fileName);
            }
            catch (Exception)
            {
            }
        }
    }
}
