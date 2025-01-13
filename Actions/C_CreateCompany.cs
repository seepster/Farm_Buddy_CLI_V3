using Farm_Buddy_V3.Abstract;
using Farm_Buddy_V3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farm_Buddy_V3.Actions
{
    internal class C_CreateCompany : ITask, IAction
    {
        public string Name { get; set; }

        private Company_Model _company;

        public C_CreateCompany()
        {
            Name = "C_CreateCompany";
        }
        public Company_Model CreateCompany(List<Models.Company_Model> companies)
        {
            Console.Clear();
            Console.WriteLine("Create Company Window");
            Console.WriteLine("---------------------");

            if (companies.Count > 0)
            {
                Console.WriteLine("Below are the existing companies.");
                Console.WriteLine("---------------------------------");
                int count = 1;
                foreach (Company_Model company in companies)
                {
                    Console.WriteLine($"{count}. {company.CompanyName}");
                    count++;
                }
                Console.WriteLine("---------------------------------");
            }
            Console.WriteLine("If you would like to add a company, simply type the company name.");
            Console.WriteLine("Alternatively press Enter to return to the previous menu");

            string? companyName = Console.ReadLine();

            if (companyName != null && companyName != "")
            {
                bool companyExists = companies.Any(x => x.CompanyName == companyName);
                if (!companyExists)
                {
                    _company = new Company_Model(companyName);
                    Console.WriteLine();
                    Console.WriteLine($"Company {companyName} succesfully added.");
                    Thread.Sleep(2000);
                    return _company;
                }
                else
                {
                    Console.WriteLine("Company already exists.");
                    Thread.Sleep(2000);
                    return null;
                }
            }
            return null;
        }
    }
}
