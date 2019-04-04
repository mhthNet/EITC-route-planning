using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EITC_route_planning.Models
{
    public class Section
    {
        public Section() { }

        public Section(City cityFrom, City cityTo, int length, TransportationType transportType)
        {
            From = cityFrom;
            To = cityTo;
            Length = length;
            TransportationType = transportType;
        }
        public City From { get; set; }
        public City To { get; set; }
        public int Length { get; set; }
        public TransportationType TransportationType { get; set; }
    }
}