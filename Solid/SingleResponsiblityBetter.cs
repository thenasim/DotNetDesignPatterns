using System;
using System.Collections.Generic;
using System.IO;

namespace DotNetDesignPatterns.Solid
{
    /*
    * The single-responsibility principle (SRP) is a computer-programming principle that states that every module, class or function in a computer program should have responsibility over a single part of that program's functionality, and it should encapsulate that part.
    */
    public class NewJournal
    {
        private readonly List<string> entries = new List<string>();
        private static int count = 0;

        public int AddEntry(string text)
        {
            entries.Add($"{++count}: {text}");
            return count; // memento
        }

        public void RemoveEntry(int index)
        {
            entries.RemoveAt(index);
        }

        public override string ToString()
        {
            return string.Join(Environment.NewLine, entries);
        }
    }

    // ? Persistence is now seperated
    public class Persistence {
        public void SaveToFile(NewJournal j, string filename, bool overwrite = false)
        {
            if (overwrite || !File.Exists(filename)) {
                File.WriteAllText(filename, j.ToString());
            }
        }

        public string LoadJournal(string filename)
        {
            return File.ReadAllText(filename);
        }
    }

    public class SingleResponsiblityBetter
    {
        public SingleResponsiblityBetter()
        {
            NewJournal myJournal = new();
            myJournal.AddEntry("I can cook this");
            myJournal.AddEntry("I should be able to read this");
            Console.WriteLine(myJournal);

            // Persistence p = new();
            // p.SaveToFile(myJournal, "journel.txt");
        }
    }
}
