using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PresentationCSharp;

namespace PuppyUnitTest
{

    [TestClass]
    class Functions
    {
        public static IEnumerable<Puppy> GiveMePuppies(IEnumerable<Puppy> source, Func<Puppy,bool> filter)
        {
            return source.Where(filter).ToList();
        }

        [TestMethod]
        public void Demo()
        {
            var puppies = new List<Puppy>()
               {
                   new Puppy("Gypsy", 10),
                   new Puppy("Mattie", 11),
                   new Puppy("Sooner", 16)
               };
            Func<Puppy,bool> filter = puppy => puppy.Name.StartsWith("G");
            var check = GiveMePuppies(puppies, filter);

            Predicate<Puppy> filter2 = puppy => puppy.Name.StartsWith("G");
            //var ooops = GiveMePuppies(puppies, filter2

            //what I really want
            var filter = PuppyUnitTest => PuppyUnitTest.Name.StartsWith("G");
        }
    }
}
