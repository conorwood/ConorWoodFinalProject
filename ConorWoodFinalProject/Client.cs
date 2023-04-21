using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConorWoodFinalProject
{
    public class RootObject
    {
        public Location? location { get; set; }
        public Current? current { get; set; }
    }

    public class Location
    {
        public Location() { }
        public string? name { get; set; }
        public string? region { get; set; }
        public string? country { get; set; }

        public double lat { get; set; }
        public double lon { get; set; }

        public string? tz_id { get; set; }
        public long localtime_epoch { get; set; }
        public string? localtime { get; set; }

    }

    public class Current
    {
        public Current() { }
        public double temp_f { get; set; }
        public double feelslike_f { get; set; }

        public int is_day { get; set; }
    }

}
