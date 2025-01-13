using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farm_Buddy_V3.Abstract
{
    internal interface IMenuSelection
    {
        string Name { get;}

        string[] MenuListing { get;}

        int MenuCount { get;}

        void DisplayMenu();

        ITask OptionSelection();
    }
}
