using Farm_Buddy_V3.Abstract;
using Farm_Buddy_V3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farm_Buddy_V3.Actions
{
    internal class C_SaveDataToFiles : ITask, IAction
    {
        public string Name { get; set; }

        public C_SaveDataToFiles()
        {
            Name = "C_SaveDataToFiles";
        }
        public void PerformAction(Company_Model company)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("---------------------");
                Console.WriteLine("Save Data To File");
                Console.WriteLine("---------------------");
                Console.WriteLine("Please enter your folder location...");
                Console.WriteLine("OR type 'exit' to go back.");
                string? folderLocation = Console.ReadLine();

                if (Directory.Exists(folderLocation))
                {

                    string companyFile = Path.Combine(folderLocation, "Company.dat");
                    string farmsFile = Path.Combine(folderLocation, "Farms.dat");
                    string fieldsFile = Path.Combine(folderLocation, "Fields.dat");
                    string fertilizersFile = Path.Combine(folderLocation, "Fertilizers.dat");

                    //perform file verify and create

                    if (!File.Exists(companyFile))
                    {
                        File.Create(companyFile).Close();
                    }
                    if (!File.Exists(farmsFile))
                    {
                        File.Create(farmsFile).Close();
                    }
                    if (!File.Exists(fieldsFile))
                    {
                        File.Create(fieldsFile).Close();
                    }
                    if (!File.Exists(fertilizersFile))
                    {
                        File.Create(fertilizersFile).Close();
                    }

                    //save company model to files

                    string companyID = company.CompanyID.ToString();
                    var companyName = company.CompanyName;
                    int farmsCount = company.Farms.Count;
                    int fieldsCount = company.Fields.Count;
                    int fertilizersCount = company.Fertilizers.Count;

                    //company file
                    if (companyID != null)
                    {
                        using (var sw = new BinaryWriter(File.Open(companyFile, FileMode.OpenOrCreate)))
                        {
                            sw.Write(companyID.ToString());
                            sw.Write(companyName);
                            sw.Write(farmsCount);
                            sw.Write(fieldsCount);
                        }
                    }
                    //farms file
                    if (farmsCount > 0)
                    {
                        using (var sw = new BinaryWriter(File.Open(farmsFile, FileMode.OpenOrCreate)))
                        {
                            sw.Write(farmsCount);
                            foreach (Farm_Model farm in company.Farms)
                            {
                                sw.Write(farm.FarmID.ToString());
                                sw.Write(farm.FarmName);
                            }
                        }
                    }
                    //fields file
                    if (fieldsCount > 0)
                    {
                        using (var sw = new BinaryWriter(File.Open(fieldsFile, FileMode.OpenOrCreate)))
                        {
                            sw.Write(fieldsCount);
                            foreach (Field_Model field in company.Fields)
                            {
                                sw.Write(field.FarmID.ToString());
                                sw.Write(field.FieldID.ToString());
                                sw.Write(field.FieldName);
                                sw.Write(field.FieldHectares);
                                sw.Write(field.FieldRunTimeInMinutes);
                                sw.Write(field.FieldDegrees);
                            }
                        }
                    }
                    //fertilizers file
                    if (fertilizersCount > 0)
                    {
                        using (var sw = new BinaryWriter(File.Open(fertilizersFile, FileMode.OpenOrCreate)))
                        {
                            sw.Write(fertilizersCount);
                            foreach (Fertilizer_Model fertilizer in company.Fertilizers)
                            {
                                sw.Write(fertilizer.FertilizerID.ToString());
                                sw.Write(fertilizer.FertilizerName);
                                sw.Write(fertilizer.ChemicalName);
                                sw.Write(fertilizer.Solubility);
                            }
                        }
                    }

                }
                else if (folderLocation == "exit")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Folder location does not exist.");
                    Thread.Sleep(2000);
                }
                break;
            }

        }
    }
}
