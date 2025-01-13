using Farm_Buddy_V3.Abstract;
using Farm_Buddy_V3.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farm_Buddy_V3.Menus
{
    internal class B_CreateDataMenu : IMenuSelection, ITask
    {
        //Create Company
        //Create Farm
        //Create Field
        //Create Seasons
        //Create Spray
        //Create Fertilizer
        //back
        public string Name { get; }
        public string[] MenuListing { get; }
        public int MenuCount { get; }

        public B_CreateDataMenu()
        {
            Name = "Create Data Menu";
            MenuListing = new string[] { "Create Company", "Create Farm", "Create Field", "Create Seasons", "Create Spray", "Create Fertilizer", "Back" };
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
                        return new C_CreateCompany();
                    case 2:
                        return new C_CreateFarm();
                    case 3:
                        return new C_CreateField();
                    case 4:
                        return new C_CreateSeasons();
                    case 5:
                        return new C_CreateSpray();
                    case 6:
                        return new C_CreateFertilizer();
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
