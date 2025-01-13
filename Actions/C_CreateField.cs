using Farm_Buddy_V3.Abstract;
using Farm_Buddy_V3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farm_Buddy_V3.Actions
{
    internal class C_CreateField : ITask, IAction
    {
        private Field_Model _field;
        public string Name { get; private set; }

        public C_CreateField()
        {
            Name = "C_CreateField";
        }

        public Field_Model CreateField(Company_Model company)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Create Field Window");
                Console.WriteLine("---------------------");
                Console.WriteLine("Below are the existing Farms.");
                Console.WriteLine("---------------------------------");
                int count = 1;
                foreach (Farm_Model farm in company.Farms)
                {
                    Console.WriteLine($"{count}. {farm.FarmName}");
                    count++;
                }
                Console.WriteLine("---------------------------------");
                Console.WriteLine("Please select your Farm to add a Field to or type '0' to go back.");

                int farmSelection = int.TryParse(Console.ReadLine(), out farmSelection) ? farmSelection : -1;

                if (farmSelection > 0 && farmSelection <= company.Farms.Count)
                {
                    Guid farmIdForNewField = company.Farms[farmSelection - 1].FarmID;
                    farmSelection -= 1;
                    Console.WriteLine("---------------------------------------");
                    Console.WriteLine($"You have opted to add a Field to Farm: {company.Farms[farmSelection].FarmName}");
                    Console.WriteLine("---------------------------------------");
                    Console.WriteLine();
                    if (company.Fields.Count > 0)
                    {
                        int count2 = 1;  
                        Console.WriteLine("Below are the existing fields on this farm.");
                        Console.WriteLine("-------------------------------------------");
                        foreach (Field_Model field in company.Fields)
                        {
                            if (field.FarmID == farmIdForNewField)
                            {
                                Console.WriteLine($"{count2}. {field.FieldName}");
                                count2++;
                            }
                        }
                    }
                    Console.WriteLine("---------------------------------");
                    Console.WriteLine("You may now enter your field name");
                    string? fieldName = Console.ReadLine();
                    if (fieldName != null || fieldName != "")
                    {
                        _field = new Field_Model(farmIdForNewField, fieldName);
                        Console.WriteLine("Please enter field hectares.");
                        decimal hectares = 0;
                        if (decimal.TryParse(Console.ReadLine(), out hectares))
                        {
                            _field.FieldHectares = decimal.Round(hectares, 2);
                            int fieldRunTime = 0;
                            Console.WriteLine("Please enter field run time in minutes.");
                            if (int.TryParse(Console.ReadLine(), out fieldRunTime))
                            {
                                _field.FieldRunTimeInMinutes = fieldRunTime;
                                int fieldDegrees = 0;
                                Console.WriteLine("Please enter field degrees.");
                                if (int.TryParse(Console.ReadLine(), out fieldDegrees))
                                {
                                    if (fieldDegrees > 0 && fieldDegrees <= 360)
                                    {
                                        _field.FieldDegrees = fieldDegrees;
                                        return _field;
                                    }
                                }
                            }

                        }
                    }
                        break;
                }
                else if(farmSelection == 0)
                {
                    return null;
                }
            }
            return _field;

        }
    }
}
