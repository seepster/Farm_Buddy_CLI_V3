using Farm_Buddy_V3.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Farm_Buddy_V3
{
    static class Console_Output
    {
         public static void DisplayMenu(IMenuSelection menu)
         {
            Console.Clear();
            Console.WriteLine(menu.Name);
            Console.WriteLine();
            int count = 1;
            foreach (string menuitem in menu.MenuListing)
            {
                Console.WriteLine($"{count}. {menuitem}");
                count++;
            }
            Console.WriteLine("-----------------------------------------------------");

         }


    }
}
