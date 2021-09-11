using System;
using System.Collections.Generic;

namespace DotNetDesignPatterns.Solid
{
    public enum Color
    {
        Red, Green, Blue
    }

    public enum Size
    {
        Small, Medium, Large, Yuge
    }

    public class Product
    {
        public string Name;
        public Color Color;
        public Size Size;

        public Product(string name, Color color, Size size)
        {
            if (name == null)
            {
                throw new ArgumentNullException(paramName: nameof(name));
            }
            Name = name;
            Size = size;
            Color = color;
        }
    }

    /*
    * Open closed principle states -
    * Classes should open for extension but closed for modification
    * Here we have breaked the open closed principle
    */
    public class ProductFilter
    {
        public IEnumerable<Product> FilterBySize(IEnumerable<Product> products, Size size)
        {
            foreach(var p in products)
                if (p.Size == size)
                    yield return p;
        }
        public IEnumerable<Product> FilterByColor(IEnumerable<Product> products, Color color)
        {
            foreach(var p in products)
                if (p.Color == color)
                    yield return p;
        }
        public IEnumerable<Product> FilterBySizeAndColor(IEnumerable<Product> products, Size size, Color color)
        {
            foreach(var p in products)
                if (p.Size == size && p.Color == color)
                    yield return p;
        }
    }

    /*
    * Solved the bad OPEN CLOSED Principle
    */
    public interface ISpecification<T>
    {
        bool IsSatisfied(T t);
    }

    public interface IFilter<T>
    {
        IEnumerable<T> Filter(IEnumerable<T> items, ISpecification<T> spec);
    }

    public class ColorSpecification : ISpecification<Product>
    {
        private Color color;

        public ColorSpecification(Color color)
        {
            this.color = color;
        }
        public bool IsSatisfied(Product t)
        {
            return t.Color == color;
        }
    }

    public class AndSpecification<T> : ISpecification<T>
    {
        ISpecification<T> first, second;

        public AndSpecification(ISpecification<T> first, ISpecification<T> second)
        {
            this.first = first ?? throw new ArgumentNullException(paramName: nameof(first));
            this.second = second ?? throw new ArgumentNullException(paramName: nameof(second));
        }

        public bool IsSatisfied(T t)
        {
            return first.IsSatisfied(t) && second.IsSatisfied(t);
        }
    }

    public class SizeSpecification : ISpecification<Product>
    {
        private Size size;

        public SizeSpecification(Size size)
        {
            this.size = size;
        }
        public bool IsSatisfied(Product t)
        {
            return t.Size == size;
        }
    }

    public class BetterFilter : IFilter<Product>
    {
        public IEnumerable<Product> Filter(IEnumerable<Product> items, ISpecification<Product> spec)
        {
            foreach (var item in items)
                if (spec.IsSatisfied(item))
                    yield return item;
        }
    }

    public class OpenClosedPrinciple
    {
        public OpenClosedPrinciple()
        {
            var apple = new Product("Apple", Color.Green, Size.Small);
            var tree = new Product("Tree", Color.Green, Size.Large);
            var house = new Product("House", Color.Blue, Size.Large);

            Product[] products = {apple, tree, house};

            var pf = new ProductFilter();

            Console.WriteLine("Green products (old):");
            foreach (var p in pf.FilterByColor(products, Color.Green))
                Console.WriteLine($" - {p.Name} is green");

            // *** NEW WAY ***
            var bf = new BetterFilter();
            var blueColorSpec = new ColorSpecification(Color.Blue);

            Console.WriteLine("Blue products (new):");
            foreach (var p in bf.Filter(products, blueColorSpec))
                Console.WriteLine($" - {p.Name} is blue");

            var greenColorSpec = new ColorSpecification(Color.Green);
            var smallSizeSpec = new SizeSpecification(Size.Small);
            var greenAndSmall = new AndSpecification<Product>(greenColorSpec, smallSizeSpec);

            Console.WriteLine("Small Green products (new):");
            foreach (var p in bf.Filter(products, greenAndSmall))
                Console.WriteLine($" - {p.Name} is green and small");
        }
    }
}
