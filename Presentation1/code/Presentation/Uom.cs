using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation
{
    class Uom
    {
        public double ComputeMiles()
        {
            return 42.0;
        }
        public double ComputeTemp()
        {
            return 42.0;
        }
        public double Dem0()
        {
            var miles = ComputeMiles();
            var temp = ComputeTemp();
            return miles + temp;
        }
    }
}
