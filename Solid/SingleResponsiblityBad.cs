using System;
using System.Collections.Generic;
using System.IO;

namespace DotNetDesignPatterns.Solid
{
    /*
    * The single-responsibility principle (SRP) is a computer-programming principle that states that every module, class or function in a computer program should have responsibility over a single part of that program's functionality, and it should encapsulate that part.
    */
    public class Journal
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

        // ! Violating Single Responsiblity
        public void SaveToFile(string filename)
        {
            File.WriteAllText(filename, ToString());
        }

        // ! Violating Single Responsiblity
        public string LoadJournal(string filename)
        {
            return File.ReadAllText(filename);
        }
    }

    public class SingleResponsiblityBad
    {
        public SingleResponsiblityBad()
        {
            Journal myJournal = new Journal();
            myJournal.AddEntry("It is raining today");
            myJournal.AddEntry("I ate an Apple");
            Console.WriteLine(myJournal);
        }
    }
}
