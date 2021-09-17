using System;

namespace DotNetDesignPatterns.Solid
{
    public class Rectangle
    {
        public virtual int Width { get; set; }
        public virtual int Height { get; set; }

        public Rectangle()
        {
        }
        public Rectangle(int width, int height)
        {
            this.Width = width;
            this.Height = height;
        }
        public override string ToString()
        {
            return $"{nameof(Width)}: {Width}, {nameof(Height)}: {Height}";
        }
    }

    public class SquareBad : Rectangle
    {
        public new int Width {
            set { base.Width = base.Height = value; }
        }
        public new int Height {
            set { base.Width = base.Height = value; }
        }
    }

    /*
    * The Liskov Substitution principle defines that objects of a superclass shall be replaceable with objects of its subclasses without breaking the application
    */
    // ? Good way
    public class Square : Rectangle
    {
        public override int Width {
            set { base.Width = base.Height = value; }
        }
        public override int Height {
            set { base.Width = base.Height = value; }
        }
    }

    class LiskovSubstitution
    {
        static public int Area(Rectangle r) => r.Width * r.Height;
        public LiskovSubstitution()
        {
            Rectangle rc = new Rectangle(5,20);
            Console.WriteLine($"{rc} has area: {Area(rc)}");

            // ! Bad Behaviour
            Rectangle sq = new SquareBad();
            sq.Width = 4;
            Console.WriteLine($"{sq} has area: {Area(sq)}");

            // ? Ok behaviour
            Rectangle sq2 = new Square();
            sq2.Width = 4;
            Console.WriteLine($"{sq2} has area: {Area(sq2)}");
        }
    }
}
