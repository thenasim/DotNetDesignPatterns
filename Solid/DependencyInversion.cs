using System;
using System.Collections.Generic;
using System.Linq;

/*
* The Dependency Inversion Principle (DIP) states that high level modules should not depend on low level modules; both should depend on abstractions. Abstractions should not depend on details. Details should depend upon abstractions.
*/
namespace DotNetDesignPatterns.Solid
{
    public enum Relationship
    {
        Parent,
        Child,
        Sibling
    }

    public interface IRelationshipBrowser
    {
       IEnumerable<Person> FindAllChildrenOf(string name);
    }

    public class Person
    {
        public string Name;
    }

    // * low level module
    public class Relationships : IRelationshipBrowser
    {
        private List<(Person, Relationship, Person)> relations
            = new List<(Person, Relationship, Person)>();

        public void AddParentAndChild(Person parent, Person child)
        {
            relations.Add((parent, Relationship.Parent, child));
            relations.Add((child, Relationship.Child, parent));
        }

        // ! Bad
        // public List<(Person, Relationship, Person)> Relations => relations;

        // ? It's now better implementing interface
        public IEnumerable<Person> FindAllChildrenOf(string name)
        {
            return relations
                .Where(x => x.Item1.Name == name && x.Item2 == Relationship.Parent)
                .Select(r => r.Item3);
        }
    }

    // * high level module
    public class Research
    {
        // ! Breaking Dependency Inversion
        // public Research(Relationships relationships)
        // {
        //     var relations = relationships.Relations;

        //     foreach (var r in relations
        //         .Where(x => x.Item1.Name == "Nasim" && x.Item2 == Relationship.Parent))
        //     {
        //         Console.WriteLine($"Nasim has a child called {r.Item3.Name}");
        //     }
        // }

        public Research(IRelationshipBrowser browser)
        {
            foreach (var p in browser.FindAllChildrenOf("Nasim"))
            {
                Console.WriteLine($"Nasim has a child called {p.Name}");
            }
        }
    }

    public class DependencyInversion
    {
        public DependencyInversion()
        {
            var parent = new Person { Name = "Nasim" };
            var child1 = new Person { Name = "Huzaifa" };
            var child2 = new Person { Name = "Fatema" };

            var relationships = new Relationships();
            relationships.AddParentAndChild(parent, child1);
            relationships.AddParentAndChild(parent, child2);

            new Research(relationships);
        }
    }
}
