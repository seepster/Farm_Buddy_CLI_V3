using Farm_Buddy_V3.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farm_Buddy_V3.Menus
{
    internal class B_EditDataMenu:IMenuSelection, ITask
    {
        //Edit Company
        //Edit Farm
        //Edit Field
        //Edit Seasons
        //Edit Spray
        //Edit Fertilizer
        //back
        public string Name { get; }
        public string[] MenuListing { get; }
        public int MenuCount { get; }

        public B_EditDataMenu()
        {
            Name = "Edit Data Menu";
            MenuListing = new string[] { "Edit Company", "Edit Farm", "Edit Field", "Edit Seasons", "Edit Spray", "Edit Fertilizer", "Back" };
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
                        return new A_MainMenu();
                    case 2:
                        return new A_MainMenu();
                    case 3:
                        return new A_MainMenu();
                    case 4:
                        return new A_MainMenu();
                    case 5:
                        return new A_MainMenu();
                    case 6:
                        return new A_MainMenu();
                    case 7:
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
