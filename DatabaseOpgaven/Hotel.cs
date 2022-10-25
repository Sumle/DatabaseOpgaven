using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseOpgaven
{
    public class Hotel
    {
        public int HotelNo { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }   

        public override string ToString()
        {
            return $"ID: {HotelNo}, Name: {Name}, Address: {Address}.";
        }
    }
}
