using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation
{
    class Enums
    {
        enum State { New, Draft, Published, Inactive, Discontinued }
        void HandleState(State state)
        {
            switch (state)
            {
                case State.Inactive: // code for Inactive
                    break;
                case State.Draft: // code for Draft
                    break;
                case State.New: // code for New
                    break;
                case State.Discontinued: // code for Discontinued
                    break;
            }
        }

    }
}
