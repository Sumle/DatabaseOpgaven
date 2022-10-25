﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseOpgaven
{
    public class Hotel
    {
        public int HotelNumber { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }   

        public override string ToString()
        {
            return $"ID: {HotelNumber}, Name: {Name}, Address: {Address}.";
        }
    }
}
