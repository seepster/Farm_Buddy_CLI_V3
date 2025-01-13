using Farm_Buddy_V3.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farm_Buddy_V3.Models
{
    internal class Farm_Model
    {
        public Guid FarmID { get; set; }
        public string FarmName { get; private set; }



        public Farm_Model( string farmName)
        {

            FarmID = Guid.NewGuid();
            FarmName = farmName;
        }

        public Farm_Model(Guid farmId, string farmName)
        {
            FarmID = farmId;
            FarmName = farmName;
        }

    }
}
