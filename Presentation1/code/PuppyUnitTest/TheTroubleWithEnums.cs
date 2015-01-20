using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PuppyUnitTest
{
    public enum Boolean {  True, False }
    [TestClass]
    public class TheTroubleWithEnums
    {
        [TestMethod]
        public void Ooops()
        {
            int number = -42;
            Boolean check = (Boolean)number;
        }
    }

    public class DiscriminatedUnion
    {
        public string Demo(Boolean boolean)
        {
            switch(boolean)
            {
                case Boolean.True:
                    return "true";
                case Boolean.False:
                    return "false";
                //Wait what is this all about
                //This mysterious default case can never happen
                //default:
                //    return "what?!";
            }
            return "what?!";
        }
        //What happens when you add Maybe to your list of enums
    }
}
