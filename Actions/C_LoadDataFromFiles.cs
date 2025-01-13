using Farm_Buddy_V3.Abstract;
using Farm_Buddy_V3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farm_Buddy_V3.Actions
{
    internal class C_LoadDataFromFiles : ITask, IAction
    {
        public string Name { get; set; }

        public C_LoadDataFromFiles()
        {
            Name = "C_LoadDataFromFiles";
        }
        public void PerformAction(Company_Model company)
        {
            

            while (true)
            {
                Console.Clear();
                Console.WriteLine("---------------------");
                Console.WriteLine("Load Data From File");
                Console.WriteLine("---------------------");
                Console.WriteLine("Please enter your folder location...");
                Console.WriteLine("OR type 'exit' to go back.");

                string? folderLocation = Console.ReadLine();
                if (folderLocation.ToLower() == "exit")
                {
                    break;
                }

                if (Directory.Exists(folderLocation))
                {
                    using (var br = new BinaryReader(File.OpenRead(Path.Combine(folderLocation, "Company.dat"))))
                    {
                        company.CompanyID = new Guid();
                        company.CompanyID = Guid.Parse(br.ReadString());
                        company.CompanyName = br.ReadString();
                        Console.WriteLine("Loading data......");
                        Console.WriteLine("Company data successfully loaded.");
                        Thread.Sleep(3000);
                    }

                    using (var br = new BinaryReader(File.OpenRead(Path.Combine(folderLocation, "Farms.dat"))))
                    {
                        int farms = br.ReadInt32();
                        for (int i = 0; i < farms; i++)
                        {
                            Guid farmID = Guid.Parse(br.ReadString());
                            string farmName = br.ReadString();
                            Farm_Model farm = new(farmID,farmName);
                            company.Farms.Add(farm);
                        }
                        Console.WriteLine("Farms Data Succesfully loaded.");
                        Thread.Sleep(3000);
                    }

                    using (var br = new BinaryReader(File.OpenRead(Path.Combine(folderLocation, "Fields.dat"))))
                    {
                        int fields = br.ReadInt32();
                        for (int i = 0; i < fields; i++)
                        {
                            Guid farmID = Guid.Parse(br.ReadString());
                            Guid fieldID = Guid.Parse(br.ReadString());
                            string fieldName = br.ReadString();
                            int fieldHectares = br.ReadInt32();
                            int fieldRunTimeInMinutes = br.ReadInt32();
                            int fieldDegrees = br.ReadInt32();
                            Field_Model field = new(farmID, fieldID, fieldName);
                            field.FieldHectares = fieldHectares;
                            field.FieldRunTimeInMinutes = fieldRunTimeInMinutes;
                            field.FieldDegrees = fieldDegrees;
                            company.Fields.Add(field);
                        }
                        Console.WriteLine("Fields Data Succesfully loaded.");
                        Thread.Sleep(3000);
                    }

                    using (var br = new BinaryReader(File.OpenRead(Path.Combine(folderLocation, "Fertilizers.dat"))))
                    {
                        int fertilizers = br.ReadInt32();
                        for (int i = 0; i < fertilizers; i++)
                        {
                            Guid fertilizerID = Guid.Parse(br.ReadString());
                            string fertilizerName = br.ReadString();
                            string fertilizerChemicalName = br.ReadString();
                            int fertilizerSolubility = br.ReadInt32();

                            Fertilizer_Model fertilizer = new(fertilizerID, fertilizerName, fertilizerSolubility);
                            company.Fertilizers.Add(fertilizer);
                        }
                        Console.WriteLine("Fertilizers Data Succesfully loaded.");
                        Thread.Sleep(3000);
                    }
                    break;
                }
                else
                {
                    Console.WriteLine("Directory does not exist.");
                    Thread.Sleep(2000);
                }
            }



        }
    }
}
