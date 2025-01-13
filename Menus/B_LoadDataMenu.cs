﻿using Farm_Buddy_V3.Abstract;
using Farm_Buddy_V3.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farm_Buddy_V3.Menus
{
    internal class B_LoadDataMenu:IMenuSelection, ITask
    {

        public string Name { get; }
        public string[] MenuListing { get; }
        public int MenuCount { get; }

        public B_LoadDataMenu()
        {
            Name = "Load Data Menu";
            MenuListing = new string[] { "From Files", "From Database", "From Cloud", "Back" };
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
                        return new C_LoadDataFromFiles();
                    case 2:
                        return new A_MainMenu();
                    case 3:
                        return new A_MainMenu();
                    case 4:
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
