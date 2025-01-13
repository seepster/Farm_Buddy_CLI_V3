using Farm_Buddy_V3.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farm_Buddy_V3.Menus
{
    internal class A_MainMenu : IMenuSelection,ITask
    {
        //Create Data
        //Edit Data
        //Load Data
        //Save Data
        //View Data
        //Exit Program
        public string Name { get; }
        public string[] MenuListing { get; }
        public int MenuCount { get; }

        public A_MainMenu()
        {
            Name = "Main Menu";
            MenuListing = new string[] { "Create Data", "Edit Data", "Load Data", "Save Data", "View Data", "Exit Program" };
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
                        return new B_CreateDataMenu();
                    case 2:
                        return new B_EditDataMenu();
                    case 3:
                        return new B_LoadDataMenu();
                    case 4:
                        return new B_SaveDataMenu();
                    case 5:
                        return new B_ViewDataMenu();
                    case 6:
                        return new B_ExitProgramMenu();
                    default:
                        return this;
                }
            }
            else
                return this;
        }
    }

}
