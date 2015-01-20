using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationCSharp
{
    public class Puppy2
    {
        public Puppy2(string name, int age)
        {
            Name = name;
            Age = age;
        }
        public string Name { get; private set; }
        public int Age { get; private set; }
    }
}
