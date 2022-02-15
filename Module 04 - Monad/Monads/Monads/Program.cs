using System;
using System.Collections.Generic;
using System.Linq;

namespace Monads
{
    internal class Program
    {
        #region Contravariance and Delegates
        class X
        {
            public int Y { get; set; }

            public int IncreaseY(int amount)
            {
                Y += amount;
                return Y;
            }
        }

        public class Shape
        {

        }

        public class Polygon : Shape
        {

        }

        public class Rectangle : Polygon
        {

        }


        public static void PlayWithCovariance()
        {
            IEnumerable<Polygon> polygons = null;
            IEnumerable<Shape> shapes = polygons;

            IMyInterface<Polygon, Polygon> myPolygons = null;
            IMyInterface<Shape, Rectangle> myRectangle = myPolygons;


        }

        public static void DoSomethingWithObjects(IEnumerable<double> objects) { }

        static int AddOne(int x)
        {
            return x + 1;
        }

        static Func<int, int> DoSomethingTwiceWithNumbers(Func<int, int> func)
        {
            func(10);
            func(20);
            return AddOne;
        }

        #endregion

        static void Main(string[] args)
        {
            //var people = GetPeople();

            //Enumerable.Range(1, 5);

            //var names = GetPeople()
            //        .Select(person => person.Name);

            //names = from person in GetPeople()
            //        select person.Name;

            //var aliases = GetPeople()
            //        .SelectMany(person => person.Aliases.Select(alias => (person, alias)))
            //        .Select(pair => $"{pair.alias} ({pair.person.Name})");

            //aliases = from person in GetPeople()
            //          from alias in person.Aliases
            //          select $"{alias} ({person.Name})";


            //var x = new Lazy<int>(() => 2 + 2);

            //var val = x.Value;
            //var val2 = x.Value;

            var five = Later.Of(5);

            var ten = from i in five
                      select i * 2;

            var fifty = from i in five
                        from j in ten
                        let n = i * j
                        select $"{i} * {j} = {n}";

            Console.WriteLine(five);
            Console.WriteLine(ten);
            Console.WriteLine(fifty);

            fifty.Calucate();

            Console.WriteLine(five);
            Console.WriteLine(ten);
            Console.WriteLine(fifty);


        }

        public static IEnumerable<Person> GetPeople()
        {
            yield return new Person
            {
                Name = "John Lennon", 
                Aliases = new List<string> { "Beatle number 1", "Johny"}
            };

            yield return new Person
            {
                Name = "Richard Starkey",
                Aliases = new List<string> { "Ringo Starr", "The drumer" }
            };
         }

        public class Person
        {
            public string Name { get; set; }

            public IEnumerable<string> Aliases { get; set; }
        }

    }

}
