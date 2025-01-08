using Farm_Buddy_V3.Abstract;
using Farm_Buddy_V3.Actions;
using Farm_Buddy_V3.Menus;
using Farm_Buddy_V3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Farm_Buddy_V3
{
    internal class Controller
    {
        IMenuSelection currentmenu;

        List<Models.Company_Model> companies { get;  set; } = new List<Models.Company_Model>();

        Company_Model activeCompany;

        IAction currentAction;

        public Controller()
        {
            IMenuSelection menu = new A_MainMenu();
            currentmenu = menu;
        }

        public void Run()
        {
            while (true)
            {
                currentmenu.DisplayMenu();
                var selectedTask = currentmenu.OptionSelection();
                var menuSelection = selectedTask as IMenuSelection;
                var actionSelection = selectedTask as IAction;

                if (menuSelection != null)
                {
                    currentmenu = menuSelection;
                }
                else if (actionSelection != null)
                {
                    currentAction = actionSelection;

                    switch(currentAction.Name)
                    {
                        case "C_CreateCompany":
                            {
                                C_CreateCompany c_CreateCompany = new C_CreateCompany();

                                activeCompany = c_CreateCompany.CreateCompany(companies);
                                activeCompany ??= companies[0];
                                break;
                            }
                        case "C_CreateFarm":
                            {
                                C_CreateFarm c_CreateFarm = new C_CreateFarm();

                                Farm_Model farm = c_CreateFarm.CreateFarm(activeCompany);
                                if (farm != null)
                                    activeCompany.Farms.Add(farm);

                                break;
                            }
                        case "C_CreateFertilizer":
                            {
                                C_CreateFertilizer c_CreateFertilizer = new C_CreateFertilizer();
                                Fertilizer_Model fertilizer = c_CreateFertilizer.CreateFertilizer(activeCompany);
                                if (fertilizer != null)
                                    activeCompany.Fertilizers.Add(fertilizer);
                                break;
                            }
                              
                            break;
                        case "C_CreateField":
                            {
                                C_CreateField c_CreateField = new C_CreateField();
                                Field_Model field = c_CreateField.CreateField(activeCompany);
                                if (field != null)
                                    activeCompany.Fields.Add(field);
                                break;
                            }

                        case "C_CreateSeasons":

                            break;
                        case "C_CreateSpray":
                            {
                                C_CreateSpray c_CreateSpray = new C_CreateSpray();
                                Spray_Model spray = c_CreateSpray.StartSpray(activeCompany);
                                break;
                            }

                            break;
                        case "C_LoadDataFromDB":
                            //LoadDataFromDB();
                            //activeCompany = ?

                            break;
                        case "C_LoadDataFromFiles":
                            {
                                if (activeCompany == null)
                                {
                                    activeCompany = new Company_Model("Name placeholder");

                                }
                                C_LoadDataFromFiles c_LoadDataFromFiles = new C_LoadDataFromFiles();
                                c_LoadDataFromFiles.PerformAction(activeCompany);
                            }


                            break;
                        case "C_SaveDataToDB":

                            break;
                        case "C_SaveDataToFiles":
                            {
                                C_SaveDataToFiles c_SaveDataToFiles = new C_SaveDataToFiles();
                                c_SaveDataToFiles.PerformAction(activeCompany);
                            }

                            break;
                        case "C_ViewData":
                            {
                                C_ViewData c_ViewData = new C_ViewData();
                                c_ViewData.PerformAction(activeCompany);
                            }

                            break;
                        default:
                            break;
                    } 
                }
            }
        }
    }
}
