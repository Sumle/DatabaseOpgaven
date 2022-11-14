using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseOpgaven
{
    public class FacilityHotel
    {

        public int FacilityNo { get; set; }

        public int Hotel_No { get; set; }

        public string Floor { get; set; }

        public override string ToString()
        {
            return $"ID: {FacilityNo}, ID: {Hotel_No}";
        }
    }
}
