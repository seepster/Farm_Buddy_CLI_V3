using Farm_Buddy_V3.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farm_Buddy_V3.Menus
{
    internal class B_ExitProgramMenu:IMenuSelection, ITask
    {

        public string Name { get; }
        public string[] MenuListing { get; }
        public int MenuCount { get; }

        public B_ExitProgramMenu()
        {
            Name = "Exit Program Menu";
            MenuListing = new string[] { "yes", "no" };
            MenuCount = MenuListing.Length;
        }

        public void DisplayMenu()
        {
            Console_Output.DisplayMenu(this);
        }

        public ITask OptionSelection()
        {
            if (int.TryParse(Console.ReadLine(), out int choice))
            {
                switch (choice)
                {
                    case 1:
                        Environment.Exit(0);
                        return this;
                    case 2:
                        return new A_MainMenu();
                    default:
                        return this;
                }
            }
            else
                return this;

        }
    }
}
