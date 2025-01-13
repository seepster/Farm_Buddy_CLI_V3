using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farm_Buddy_V3.Models
{
    internal class Fertilizer_Model
    {
        public Guid FertilizerID { get;  set; }
        public string FertilizerName { get;  set; }
        public string ChemicalName { get;  set; }
        public int Solubility { get;  set; }

        public decimal FertilizerDosage { get;  set; }

        public decimal FertilizerDosageTotal { get; set; }

        public Fertilizer_Model(string fertilizerName, int solubility)
        {
            FertilizerID = Guid.NewGuid();
            FertilizerName = fertilizerName;
            ChemicalName = "";
            Solubility = solubility;
        }

        public Fertilizer_Model(Guid fertilizerID, string fertilizerName, int solubility)
        {
            FertilizerID = fertilizerID;
            FertilizerName = fertilizerName;
            ChemicalName = "";
            Solubility = solubility;
        }
    }
}
