using Farm_Buddy_V3.Abstract;
using Farm_Buddy_V3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Farm_Buddy_V3.Actions
{
    internal class C_CreateSpray : ITask, IAction
    {
        public string Name { get; private set; }

        public C_CreateSpray()
        {
            Name = "C_CreateSpray";
        }

        public Spray_Model StartSpray(Company_Model company)
        {
            Spray_Model spray = CreateSpraySession(company);
            spray = AddFertilizersToSpray(spray);
            spray = GetFertilizerInfoToSpray(spray);
            spray = GetMixTankInfo(spray);
            spray = GetNumberOfTanksInfo(spray);
            spray = GetFlowMeter(spray);
            spray = StartSpraySession(spray);
            return spray;

        }

        //first configure spraysession
        Spray_Model CreateSpraySession(Company_Model company)
        {
            Spray_Model spray;
            int farmIndex = 0;
            Guid farmID;
            string farmName = "";
            int fieldIndex = 0;
            string fieldName = "";
            List<Field_Model> thisFarmFields = new List<Field_Model>();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("You are about to Start a New Spray Session.");
                Console.WriteLine("-------------------------------------------");
                if (company != null)
                {
                    Console.WriteLine($"Company: {company.CompanyName}");

                    int count = 1;
                    if (company.Farms.Count > 0)
                    {
                        foreach (Farm_Model farm in company.Farms)
                        {
                            Console.WriteLine($"{count}. {farm.FarmName}");
                            count++;
                        }

                        Console.WriteLine("-------------------------------------------");
                        Console.WriteLine("Please select the Farm on which you are Spraying Today.");

                        int farmSelection = int.TryParse(Console.ReadLine(), out farmSelection) ? farmSelection : -1;
                        if (farmSelection > 0 && farmSelection <= company.Farms.Count)
                        {
                            farmIndex = farmSelection - 1;
                            Console.WriteLine();
                            Console.WriteLine("-----------------------------------------");
                            farmID = company.Farms[farmIndex].FarmID;
                            farmName = company.Farms[farmIndex].FarmName;

                            Console.WriteLine($"Below are the Fields available for Farm: {company.Farms[farmIndex].FarmName}");
                            Console.WriteLine("-----------------------------------------");

                            
                            int fieldcount = 1;
                            
                            foreach (Field_Model field in company.Fields)
                            {
                                if (field.FarmID == farmID)
                                {
                                    thisFarmFields.Add(field);
                                    Console.WriteLine($"{fieldcount}. {field.FieldName}");
                                    fieldcount++;
                                }
                                
                            }

                            Console.WriteLine();
                            Console.WriteLine("-----------------------------------------");
                            Console.WriteLine("Please select your field for this Spray.");

                            int fieldSelection = int.TryParse(Console.ReadLine(), out fieldSelection) ? fieldSelection : -1;
                            if (fieldSelection > 0 && fieldSelection <= thisFarmFields.Count)
                            {
                                fieldIndex = fieldSelection - 1;
                                fieldName = thisFarmFields[fieldIndex].FieldName;
                                Console.WriteLine();
                                Console.WriteLine("-----------------------------------------");
                                Console.WriteLine($"You have selected Field: {thisFarmFields[fieldIndex].FieldName}");
                                Console.WriteLine("-----------------------------------------");
                                Console.WriteLine("Please enter the name of the Spray.");
                                string sprayName = $"{company.CompanyName}-{farmName}-{fieldName}";
                                Console.WriteLine($"Or Press Enter if you want to use the name: {sprayName}");
                                string? sprayNameinput = Console.ReadLine();

                                if (sprayNameinput != null && sprayNameinput != "")
                                {
                                    sprayName = sprayNameinput;
                                }

                                spray = new Spray_Model(company, farmIndex, fieldIndex);
                                spray.SprayName = sprayName;
                                Console.WriteLine($"You have created a new Spray: {spray.SprayName}");
                                return spray;
                            }
                            else
                            {
                                Console.WriteLine("Invalid Field Selection.");
                                Thread.Sleep(2000);
                            }
                        }
                        else
                        {
                            Console.WriteLine("------------------");
                            Console.WriteLine("Invalid Farm Selection.");
                            Thread.Sleep(2000);
                        }

                    }
                    else
                    {
                        Console.WriteLine("You have no farm set up.");
                        Console.WriteLine("Please navigate to either Create Data Menu OR Load Data Menu.");
                        Thread.Sleep(3000);
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("You have no active company set up.");
                    Console.WriteLine("Please navigate to either Create Data Menu OR Load Data Menu.");
                    Thread.Sleep(3000);
                }

            }
        }

        Spray_Model AddFertilizersToSpray(Spray_Model spray)
        {
            //add fertilizers
            List<Fertilizer_Model> _fertilizersToSpray= new List<Fertilizer_Model>();

            Console.Clear();
            Console.WriteLine("Please select the Fertilizers to be sprayd");
            Console.WriteLine();
            Console.WriteLine("Type 'exit' when you are done selecting fertilizers");
            Console.WriteLine();
            Console.WriteLine("Please select the fertilizer ID from the list below.");
            Console.WriteLine("----------------------------------------------------");

            int count = 1;
            foreach(Fertilizer_Model fertilizer in spray.AvailableFertilizers)
            {
                Console.WriteLine($"{count} : {fertilizer.FertilizerName}");
                count++;
            }
            Console.WriteLine();
            Console.WriteLine("----------------------------------------------------");

            string? dataReader = null;
            int result = 0;

            while(true)
            {
                dataReader = Console.ReadLine();
                if (dataReader.ToString().ToLower() == "exit")
                {
                    break;
                }
                else
                if (int.TryParse(dataReader, out result))
                {
                    if (result <= spray.AvailableFertilizers.Count && result > 0)
                    {
                        Guid fertilizerID = spray.AvailableFertilizers[result - 1].FertilizerID;

                        if (spray.SprayFertilizers.Any(f => f.FertilizerID == fertilizerID))
                        {
                            Console.WriteLine();
                            Console.WriteLine("Fertilizer already added.");
                            Console.WriteLine();
                            Thread.Sleep(2000);
                        }
                        else
                        {

                            string fertilizerName = spray.AvailableFertilizers[result - 1].FertilizerName;
                            string fertilizerChemicalName = spray.AvailableFertilizers[result - 1].ChemicalName;
                            int fertilizerSolubility = spray.AvailableFertilizers[result - 1].Solubility;
                            Fertilizer_Model fertilizer = new Fertilizer_Model(fertilizerID, fertilizerName, fertilizerSolubility);
                            fertilizer.ChemicalName = fertilizerChemicalName;
                            _fertilizersToSpray.Add(fertilizer);

                            dataReader = null;
                            Console.WriteLine($"{fertilizer.FertilizerName} added");
                        }
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine("Invalid Fertilizer Selection.");
                        dataReader = null;
                    }
                }
                
            }
            spray.SprayFertilizers = _fertilizersToSpray;
            return spray;
        }

        //get fertilizer dosages

        Spray_Model GetFertilizerInfoToSpray(Spray_Model spray)
        {
            Console.Clear();
            Console.WriteLine("------------------------");
            Console.WriteLine($"Company: {spray.company.CompanyName}");
            Console.WriteLine($"Farm: {spray.farm.FarmName}");
            Console.WriteLine($"Field: {spray.field.FieldName}");
            Console.WriteLine($"Field Hectares: {spray.field.FieldHectares}");
            Console.WriteLine("-------------------------");
            Console.WriteLine("Please add your dosage information for the given fertilizers to be sprayd.");
            Console.WriteLine("--------------------------------------------------------------------------");

            for (int i = 0; i < spray.SprayFertilizers.Count; i++)
            {
                while (true)
                {
                    int dosageInt;
                    Console.WriteLine();
                    Console.WriteLine($"{i + 1}. {spray.SprayFertilizers[i].FertilizerName}");
                    Console.Write("Dosage(kg/ha):");
                    string? dosage = Console.ReadLine();
                    if (int.TryParse(dosage, out dosageInt))
                    {
                        spray.SprayFertilizers[i].FertilizerDosage = dosageInt;
                        spray.SprayFertilizers[i].FertilizerDosageTotal = dosageInt * spray.field.FieldHectares;
                        Console.WriteLine("Dosage applied.");
                        Console.WriteLine();
                        Thread.Sleep(1000);
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid Dosage.");
                        Thread.Sleep(2000);
                    }
                }
            }
            return spray;
        }

        //enter mixtank details

        Spray_Model GetMixTankInfo(Spray_Model spray)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Please enter your Mixing Tank Information.");
                Console.WriteLine("------------------------------------------");
                Console.Write("Mixing Tank Capacity(in litres): ");
                int mixTankCapacity = 0;
                string? dataReader = Console.ReadLine();
                if (int.TryParse(dataReader, out mixTankCapacity))
                {
                    spray.MixTankCapacityLitres = mixTankCapacity;
                    return spray;
                }
                else
                {
                    Console.WriteLine("All you need to do here is enter a quantity...");
                    Thread.Sleep(3000);
                }
            }
        }
        //calculate and determine number of tanks
        Spray_Model GetNumberOfTanksInfo(Spray_Model spray)
        {
            decimal attemptedNumberOfTanks = 1/10;
            decimal userAttemptedNumberOfTanks = 0;
            //We check fertilizer solubility
            for (int i = 0; i < spray.SprayFertilizers.Count; i++)
            {
                decimal denominator = spray.MixTankCapacityLitres * spray.SprayFertilizers[i].Solubility / 100;
                if (denominator != 0)
                {
                    if (spray.SprayFertilizers[i].FertilizerDosageTotal / denominator > attemptedNumberOfTanks)
                    {
                        attemptedNumberOfTanks = spray.SprayFertilizers[i].FertilizerDosageTotal / denominator;
                        spray.NumberOfTanks = attemptedNumberOfTanks;
                    }
                }
            }

            //we calculate estimated tanks qty and prompt user for whole numbers while outputting mixing qty's

            while (true)
            {
                    Console.Clear();
                    Console.WriteLine("----------------------------");
                    Console.WriteLine($"You are attempting to use : {attemptedNumberOfTanks:N2} Tanks");
                    Console.WriteLine();
                    Console.WriteLine("Please adjust this tank quantity to your liking in order to view mixing recipe");
                    Console.WriteLine();

                    string? dataReader = Console.ReadLine();

                    if (decimal.TryParse(dataReader, out userAttemptedNumberOfTanks))
                    {
                        Console.WriteLine();
                        Console.WriteLine("----------------------------------------------------");
                        Console.WriteLine("Your recipe per tank will be as follows:");
                        foreach (Fertilizer_Model fertilizer in spray.SprayFertilizers)
                        {
                            Console.WriteLine($"{fertilizer.FertilizerName} : {fertilizer.FertilizerDosageTotal / userAttemptedNumberOfTanks}");
                        }
                        Console.WriteLine();
                        Console.WriteLine("Is this a mixture you can manage ?");
                        Console.WriteLine("----------------------------------");
                        Console.WriteLine("Type 'y' to use this mixture and continue.....");
                        Console.WriteLine(" or type 'n' to try again.");

                        dataReader = Console.ReadLine();
                        if (dataReader.ToLower() == "y")
                        {
                            spray.NumberOfTanks = userAttemptedNumberOfTanks;
                            return spray;
                        }
                    }
                }
            }

        //calculate flowmeter
        Spray_Model GetFlowMeter(Spray_Model spray)
        {
            int _flowRecalibration = 0;
            // Next we calculate required flow and adjust for calibration to provide flowmeterreading
            foreach (Fertilizer_Model fertilizer in spray.SprayFertilizers)
            {
                if (fertilizer.FertilizerName == "Ammonium Sulphate")
                {
                    _flowRecalibration = 5;
                }
                else if (fertilizer.FertilizerName == "Urea")
                {
                    _flowRecalibration = 3;
                }
                else
                {
                    _flowRecalibration = 2;
                }
            }
            
            spray.FlowRequired = (spray.NumberOfTanks * spray.MixTankCapacityLitres) / spray.field.FieldRunTimeInMinutes;
            Console.WriteLine();
            Console.WriteLine($"The flow required is {spray.FlowRequired:N2} l.p.m");
            Console.WriteLine();
            Console.WriteLine($"Based on the contents of your mixture, I advise we make a {_flowRecalibration} percent adjustment to account for the 'heavier' fluid.");
            Console.WriteLine();
            Console.WriteLine("Press Enter to accept this adjustment or enter your preferred adjustment in percentage(0 - 10).");

            while (true)
            {
                string? dataReader = Console.ReadLine();

                if (int.TryParse(dataReader, out _flowRecalibration))
                {
                    if (_flowRecalibration <= 10 && _flowRecalibration >= 0)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Please enter a value from 0 to 10");
                    }
                }
            }
            spray.FlowRecalibration = _flowRecalibration;
            spray.FlowMeterReading = spray.FlowRequired * (1 + (spray.FlowRecalibration / 100));
            spray.FlowMeterReading = Math.Round(spray.FlowMeterReading, 1);

            Console.WriteLine($"Your flowmeter should read {decimal.Round(spray.FlowMeterReading, 1)} l.p.m");
            Thread.Sleep(3000);
            return spray;
        }

        //start spray session
        Spray_Model StartSpraySession(Spray_Model spray)
        {

            //Next we calculate the time it will take to empty the mixtank
            Console.Clear();
            Console.WriteLine("-----------------------------");
            Console.WriteLine("Spray Session Starting...");
            Console.WriteLine($"Company: {spray.company.CompanyName}");
            Console.WriteLine($"Farm: {spray.farm.FarmName}");
            Console.WriteLine($"Field: {spray.field.FieldName}");
            Console.WriteLine();
            Console.WriteLine("-------------------------------");
            Console.WriteLine($"Hectares: {spray.field.FieldHectares}");
            Console.WriteLine($"Field Run Time: {spray.field.FieldRunTimeInMinutes} minutes");
            Console.WriteLine($"Field Degrees: {spray.field.FieldDegrees}");
            Console.WriteLine("-------------------------------");
            Console.WriteLine("Fertilizers Dosage:");

            int count = 1;
            foreach (Fertilizer_Model fertilizers in spray.SprayFertilizers)
            {
                Console.WriteLine($"{count}. {fertilizers.FertilizerName}:" + "\t\t\t\t" + $"{fertilizers.FertilizerDosage} kg/ha for a total of {fertilizers.FertilizerDosageTotal} kg");
            }

            Console.WriteLine();
            Console.WriteLine("-------------------------------");
            Console.WriteLine($"You will be spraying a total of {spray.NumberOfTanks} tanks.");
            Console.WriteLine("-------------------------------");
            Console.WriteLine("Your mixture per tank will be:");
            Console.WriteLine("------------------------------");
            Console.WriteLine();

            foreach (Fertilizer_Model fertilizer in spray.SprayFertilizers)
            {
                Console.WriteLine($"{fertilizer.FertilizerName}:" + "\t\t\t\t" + $"{Math.Round((fertilizer.FertilizerDosageTotal/spray.NumberOfTanks),1)} kg.");
            }


            spray.MixTankTimeToEmpty = Math.Round((spray.MixTankCapacityLitres / spray.FlowRequired),1);
            Console.WriteLine("--------------------------------");
            Console.WriteLine($"Your flowmeter should read {spray.FlowMeterReading} l.p.m.");
            Console.WriteLine();
            Console.WriteLine("--------------------------------");
            Console.WriteLine($"Each tank will take {spray.MixTankTimeToEmpty} minutes to empty out.");
            Console.WriteLine();
            spray.PivotDegreePerTank = Math.Round((spray.field.FieldDegrees / spray.NumberOfTanks),1);

            Console.WriteLine($"Each tank will move the pivot {spray.PivotDegreePerTank} degrees");
            Console.WriteLine("---------------------------------");

            Console.WriteLine("You are now ready to start spraying.");
            Console.WriteLine();
            Console.WriteLine("Enter your start time and pivot degrees in order to keep track of your progress.");
            Console.WriteLine();

            Console.Write("Starting time: (In military format ex. 13:00)");
            string? dataReader = null;
            double _startHours = 0;
            int _startMinutes = 0;
            do
            {
                dataReader = Console.ReadLine();
                if (dataReader != null)
                {
                    if (TimeSpan.TryParse(dataReader, out TimeSpan startTime))
                    {
                        _startHours = startTime.Hours;
                        _startMinutes = startTime.Minutes;
                        Console.WriteLine($"You have started spraying at {_startHours}:{_startMinutes}");
                        break;
                    }
                    else
                    {
                        dataReader = null;
                        Console.WriteLine("Invalid Entry...");
                    }
                }
            } while (dataReader == null);

            Console.WriteLine("Enter the pivot degrees when spray started.");
            Console.WriteLine();
            int _startDegrees = 0;
            do
            {
                dataReader = Console.ReadLine();
                if (dataReader != null)
                {
                    if (int.TryParse(dataReader, out _startDegrees))
                    {
                        if (_startDegrees <= spray.field.FieldDegrees && _startDegrees >= 0)
                        {
                            Console.WriteLine($"You started spraying at {_startDegrees} degrees");
                            Console.WriteLine();
                            break;
                        }
                        else
                        {
                            dataReader = null;
                            Console.WriteLine("Outside pivot movement area.");
                        }
                    }
                    else
                    {
                        dataReader = null;
                        Console.WriteLine("Invalid Entry...");
                    }
                }
            } while (dataReader == null);

            //now we start the timer for our spraysession

            for (int i = 0; i < spray.NumberOfTanks; i++)
            {
                Console.WriteLine($"Tank {i + 1} is now spraying.");
                Console.WriteLine();
                int addHours = (int)(spray.MixTankTimeToEmpty / 60);
                int addMinutes = (int)(spray.MixTankTimeToEmpty % 60);
                _startHours += addHours;

                if (_startMinutes >= 60)
                {
                    _startHours += (int)_startMinutes / 60;
                    _startMinutes += _startMinutes % 60;
                }
                else
                {
                    _startHours += addHours;
                    _startMinutes += addMinutes % 60;
                }
                if (_startMinutes >= 60)
                {
                    _startHours += (int)_startMinutes / 60;
                    _startMinutes = _startMinutes % 60;
                }
                if (_startHours >= 24)
                {
                    _startHours = 0;
                }
                _startDegrees += (int)Math.Round(spray.PivotDegreePerTank);
                if(i+2 <= spray.NumberOfTanks)
                Console.WriteLine($"Tank {i + 2} should start at {_startHours}:{_startMinutes} , and pivot positioned at {_startDegrees} Degrees.");
            }
            Console.WriteLine();
            Console.WriteLine("You are done!.");
            Thread.Sleep(2000);
            return spray;
        }
        }
    }

        
        
        
