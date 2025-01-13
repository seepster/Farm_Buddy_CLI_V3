using Farm_Buddy_V3.Abstract;
using Farm_Buddy_V3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farm_Buddy_V3.Actions
{
    internal class C_ViewData : ITask, IAction
    {
        public string Name { get; set; }

        public C_ViewData()
        {
            Name = "C_ViewData";
        }

        public void PerformAction(Company_Model company)
        {
            while (true)
            {
                if (company != null)
                {
                    Console.Clear();
                    Console.WriteLine("----------------------------");
                    Console.WriteLine("Data View");
                    Console.WriteLine("----------------------------");
                    Console.WriteLine($"Company ID: {company.CompanyID}");
                    Console.WriteLine($"Company Name: {company.CompanyName}");
                    Thread.Sleep(4000);
                    Console.WriteLine("Press Enter to continue...");
                    Console.ReadLine();

                    if (company.Farms.Count > 0)
                    {
                        foreach (Farm_Model farm in company.Farms)
                        {
                            //display farms
                            Console.WriteLine();
                            Console.WriteLine("----------------------------");
                            Console.WriteLine($"Farm ID: {farm.FarmID}");
                            Console.WriteLine($"Farm Name: {farm.FarmName}");
                            Console.WriteLine("----------------------------");
                            Thread.Sleep(2000);
                            Console.WriteLine("Press Enter to continue...");
                            Console.ReadLine();

                            //display fields 
                            if (company.Fields.Count > 0)
                            {
                                Console.WriteLine($"Fields for: {farm.FarmName}");

                                foreach (Field_Model field in company.Fields)
                                {
                                    if (farm.FarmID == field.FarmID)
                                    {
                                        Console.WriteLine();
                                        Console.WriteLine("--------------------------------------------");
                                        Console.WriteLine($"Field ID: " + "\t\t\t\t"+ $"{field.FieldID}");
                                        Console.WriteLine($"Field Name: " + "\t\t\t\t"+ $"{field.FieldName}");
                                        Console.WriteLine($"Field Hectares " + "\t\t\t\t"+ $"{field.FieldHectares}");
                                        Console.WriteLine($"Field Run Time in Minutes: " + "\t"+ $"{field.FieldRunTimeInMinutes}");
                                        Console.WriteLine($"Field Degrees: " + "\t\t\t\t"+ $"{field.FieldDegrees}");
                                        Console.WriteLine("--------------------------------------------");
                                        Thread.Sleep(2000);
                                    }
                                }
                                Console.WriteLine("Press Enter to continue...");
                                Console.ReadLine();
                            }
                        }

                        
                        
                    }
                    if (company.Fertilizers.Count > 0)
                    {
                        Console.Clear();
                        Console.WriteLine("--------------------");
                        Console.WriteLine("Fertilizers");
                        //display fertilizers
                        foreach (Fertilizer_Model fertilizer in company.Fertilizers)
                        {
                            Console.WriteLine("---------------------");
                            Console.WriteLine($"Fertilizer ID: {fertilizer.FertilizerID}");
                            Console.WriteLine($"Fertilizer Name: {fertilizer.FertilizerName}");
                            Console.WriteLine($"Chemical Name: {fertilizer.ChemicalName}");
                            Console.WriteLine($"Solubility: {fertilizer.Solubility}");
                            Console.WriteLine("---------------------");
                        }
                        Console.WriteLine("Press Enter to continue...");
                        Console.ReadLine();
                        break;
                    }
                }
                else
                {
                    Console.WriteLine("No data to display.");
                    Thread.Sleep(2000);
                }
                break;
            }
        }
    }
}
