using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farm_Buddy_V3.Models
{
    internal class Spray_Model
    {
        
        public Guid SprayID { get; set; }

        public string SprayName { get;  set; }

        public Company_Model company;

        public Farm_Model farm;

        public Field_Model field;

        public List<Fertilizer_Model> SprayFertilizers { get; set; } = new List<Fertilizer_Model>();

        public List<Fertilizer_Model> AvailableFertilizers { get; set; } = new List<Fertilizer_Model>();

        public int MixTankCapacityLitres { get; set; }
        public decimal FlowRecalibration { get; set; }

        public decimal NumberOfTanks { get; set; }
        public decimal FlowRequired { get; set; }
        public decimal FlowMeterReading { get; set; }
        public decimal MixTankTimeToEmpty { get; set; }
        public decimal PivotDegreePerTank { get; set; }

        public decimal FertilizersQty { get; set; }
        public decimal FertilizerTotalKG { get; set; }

        public Spray_Model(Company_Model company_Model, int farmIndex, int fieldIndex)
        {
            SprayID = Guid.NewGuid();
            company = company_Model;
            farm = company_Model.Farms[farmIndex];
            field = company_Model.Fields[fieldIndex];
            AvailableFertilizers = company_Model.Fertilizers;
        }

        public void AddSprayFertilizer(Fertilizer_Model fertilizer, decimal fertilizerDosage, decimal fertilizerDosageTotal)
        {
            fertilizer.FertilizerDosage = fertilizerDosage;
            fertilizer.FertilizerDosageTotal = fertilizerDosageTotal;
            SprayFertilizers.Add(fertilizer);
        }

        public void RemoveFertilizer(int index)
        {
            SprayFertilizers.RemoveAt(index);
        }

        public void InitializeSpraySession()
        {

        }

        public void RecordSpraySession()
        {

        }


    }
}
