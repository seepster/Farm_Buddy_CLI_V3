using Farm_Buddy_V3.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farm_Buddy_V3.Models
{
    internal class Company_Model
    {
        public Guid CompanyID { get;  set; }

        public string CompanyName { get;  set; }

        public List<Farm_Model> Farms { get;   set; } = new List<Farm_Model>();

        public List<Field_Model> Fields { get;  set; } = new List<Field_Model>();

        public List<Fertilizer_Model> Fertilizers { get; set; } = new List<Fertilizer_Model>();

        public Company_Model(string companyName)
        {
            CompanyID = Guid.NewGuid();
            CompanyName = companyName;
        }

        public Company_Model(Guid companyID, string companyName)
        {
            CompanyID = companyID;
            CompanyName = companyName;
        }
    }
}
