using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farm_Buddy_V3.Models
{
    internal class Field_Model
    {

        public Guid FarmID { get; set; }
        public Guid FieldID { get;  set; }
        public string FieldName { get;  set; }
        public decimal FieldHectares { get; set; }
        public int FieldRunTimeInMinutes { get; set; }
        public int FieldDegrees { get; set; }

        public Field_Model(Guid farmID,string fieldName)
        {
            FarmID = farmID;
            FieldID = Guid.NewGuid();
            FieldName = fieldName;
            FieldHectares = 0;
            FieldRunTimeInMinutes = 0;
            FieldDegrees = 0;
        }

        public Field_Model(Guid farmID, Guid fieldID, string fieldName)
        {
            FarmID = farmID;
            FieldID = fieldID;
            FieldName = fieldName;
            FieldHectares = 0;
            FieldRunTimeInMinutes = 0;
            FieldDegrees = 0;
        }
        
    }
}
