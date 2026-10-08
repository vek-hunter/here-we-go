using System;
using System.Collections.Generic;
using System.IO;

namespace JournalProgram
{
    public class Journal
    {
        private readonly List<Entry> _entries;

        public Journal()
        {
            _entries = new List<Entry>();
        }

        public List<Entry> Entries
        {
            get { return _entries; }
        }

        public void AddEntry(Entry entry)
        {
            _entries.Add(entry);
        }

        public void DisplayAll()
        {
            if (_entries.Count == 0)
            {
                Console.WriteLine("No journal entries yet.");
                return;
            }

            foreach (Entry entry in _entries)
            {
                entry.Display();
            }
        }

        public void SaveToFile(string fileName)
        {
            using StreamWriter writer = new StreamWriter(fileName);

            foreach (Entry entry in _entries)
            {
                writer.WriteLine($"{entry.Date}~|~{entry.Prompt}~|~{entry.Response}");
            }
        }

        public void LoadFromFile(string fileName)
        {
            if (!File.Exists(fileName))
            {
                throw new FileNotFoundException($"The file '{fileName}' does not exist.");
            }

            _entries.Clear();
            string[] lines = File.ReadAllLines(fileName);

            foreach (string line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                string[] parts = line.Split("~|~");

                if (parts.Length >= 3)
                {
                    string date = parts[0];
                    string prompt = parts[1];
                    string response = parts[2];

                    if (parts.Length > 3)
                    {
                        response = string.Join("~|~", parts, 2, parts.Length - 2);
                    }

                    _entries.Add(new Entry(date, prompt, response));
                }
            }
        }
    }
}
