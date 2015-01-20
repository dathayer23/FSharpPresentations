using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PresentationCSharp;
namespace PuppyUnitTest
{
    [TestClass]
    public class TestPuppy
    {
        [TestMethod]
        public void RunCode()
        {
            var puppy = new Puppy("Gypsy", 10);
            var output = puppy.ToString();
        }
    }
}
