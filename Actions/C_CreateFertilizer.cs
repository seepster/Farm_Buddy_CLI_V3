using Farm_Buddy_V3.Abstract;
using Farm_Buddy_V3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farm_Buddy_V3.Actions
{
    internal class C_CreateFertilizer : ITask, IAction
    {
        public string Name { get; set; }

        public Fertilizer_Model _fertilizer;

        public C_CreateFertilizer()
        {
            Name = "C_CreateFertilizer";
        }
        public Fertilizer_Model CreateFertilizer(Company_Model company)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Create Fertilizer Window");
                Console.WriteLine("---------------------");
                Console.WriteLine($"You are adding a Fertilizer to Company: {company.CompanyName}");
                Console.WriteLine("---------------------");
                if (company.Fertilizers.Count > 0)
                {
                    Console.WriteLine($"Here are the existing Fertilizers.");

                    int count = 1;

                    foreach (Models.Fertilizer_Model fertilizer in company.Fertilizers)
                    {
                        Console.WriteLine($"{count}. {fertilizer.FertilizerName}");
                        count++;
                    }
                }

                Console.WriteLine("Please enter the name of the fertilizer you would like to add or press Enter to return.");
                string? fertilizerName = Console.ReadLine();

                if (fertilizerName != null && fertilizerName != "")
                {
                    if (!(company.Fertilizers.Any(x => x.FertilizerName == fertilizerName)))
                    {
                        Console.WriteLine("---------------------------");
                        Console.WriteLine($"Please enter the Solubility(0 - 100)% of {fertilizerName}");
                        string? fertilizerSolubility = Console.ReadLine();

                        if (fertilizerSolubility != null && fertilizerSolubility != "")
                        {
                            int fertilizerSolubilityInt = 0;
                            if (int.TryParse(fertilizerSolubility, out fertilizerSolubilityInt))
                            {
                                _fertilizer = new Fertilizer_Model(fertilizerName, fertilizerSolubilityInt);
                                Console.WriteLine($"You have added Fertilizer: {_fertilizer.FertilizerName} to Company: {company.CompanyName}");
                                Thread.Sleep(2000);
                                return _fertilizer;
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("A similar Fertilizer already exists.");
                        Thread.Sleep(2000);
                    }
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
