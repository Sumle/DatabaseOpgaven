using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseOpgaven
{
    public class Facility
    {
        public int FacilityNo { get; set; }

        public string Name { get; set; }

        public override string ToString()
        {
            return $"ID: {FacilityNo}, Name: {Name}";
        }
    }
}
