using System;
using System.Collections.Generic;
using System.Linq;

namespace DotNetDesignPatterns.Mediator
{
    /*
    * Here chat room is central component which acts as a mediator
    * which allows people to communicate with one and another
    * wihout knowing their presence.
    */
    public class ChatRoom
    {
        private List<Person> People = new List<Person>();

        public void Join(Person p)
        {
            string joinMsg = $"{p.Name} joins the chat";
            BroadCast("room", joinMsg);

            p.Room = this;
            People.Add(p);
        }

        public void BroadCast(string source, string message)
        {
            foreach (var p in People)
                if (p.Name != source)
                    p.Recieve(source, message);
        }

        public void Message(string source, string destination, string message)
        {
            People.FirstOrDefault(p => p.Name == destination)
                ?.Recieve(source, message);
        }
    }

    public class Person
    {
        public string Name { get; set; }
        public ChatRoom Room { get; set; }
        private List<string> _chatLog = new List<string>();

        public Person(string name)
        {
            Name = name;
        }

        public void Say(string message)
        {
            Room.BroadCast(Name, message);
        }

        public void PrivateMessage(string who, string message)
        {
            Room.Message(Name, who, message);
        }

        public void Recieve(string sender, string message)
        {
            string s = $"{sender}: '{message}'";
            _chatLog.Add(s);
            Console.WriteLine($"[{Name}'s chat sesson] {s}");
        }
    }

    public class MediatorPrinciple
    {
        public MediatorPrinciple()
        {
            var room = new ChatRoom();

            var nasim = new Person("Nasim");
            var mehedi = new Person("Mehedi");

            room.Join(nasim);
            room.Join(mehedi);

            nasim.Say("Hi");
            mehedi.Say("Hei, Nasim");

            var shanto = new Person("Shanto");

            room.Join(shanto);

            shanto.Say("Hi, everyone");
            nasim.PrivateMessage("Shanto", "Hei shanto");
        }
    }
}
