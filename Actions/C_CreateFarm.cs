using Farm_Buddy_V3.Abstract;
using Farm_Buddy_V3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farm_Buddy_V3.Actions
{
    internal class C_CreateFarm : ITask, IAction
    {
        public string Name { get; set; }

        private Farm_Model _farm;

        public C_CreateFarm()
        {
            Name = "C_CreateFarm";
        }
        public Farm_Model CreateFarm(Company_Model company)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Create Farm Window");
                Console.WriteLine("---------------------");
                Console.WriteLine($"You are adding a Farm to Company: {company.CompanyName}");
                Console.WriteLine("---------------------");
                if (company.Farms.Count > 0)
                {
                    List<Models.Farm_Model> farms = company.Farms;

                    Console.WriteLine($"Here are the existing farms.");
                    int count = 1;

                    foreach (Models.Farm_Model farm in farms)
                    {
                        Console.WriteLine($"{count}. {farm.FarmName}");
                        count++;
                    }
                }
                Console.WriteLine();
                Console.WriteLine("-------------------------");
                Console.WriteLine("Please enter the name of the farm you would like to add or press Enter to return.");
                string? farmName = Console.ReadLine();

                if (farmName != null && farmName != "")
                    {
                        _farm = new Farm_Model(farmName);
                        Console.WriteLine($"You have added Farm: {_farm.FarmName} to Company: {company.CompanyName}");
                        Thread.Sleep(2000);
                        return _farm;
                    }

                else
                {
                    return null;
                }
            }
        }
    }
}
