using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TodoLegal.Test1.Models
{
    public class Dweet
    {
        public string by { get; set; }
        public string the { get; set; }
        public List<With> with { get; set; }

        public class Content
        {
            public double temperature { get; set; }
            public int humidity { get; set; }
            public double temperature2 { get; set; }
            public int humidity2 { get; set; }
            public double temperature3 { get; set; }
            public int humidity3 { get; set; }
            public string temperature4 { get; set; }
            public int humidity4 { get; set; }
            public int temperature5 { get; set; }
            public int temperature6 { get; set; }
            public double VWC { get; set; }
            public int lux { get; set; }
            public int tensio { get; set; }
            public int R1 { get; set; }
            public int R2 { get; set; }
            public int R3 { get; set; }
            public int R4 { get; set; }
            public int R5 { get; set; }
            public int R6 { get; set; }
            public int R7 { get; set; }
            public int L8 { get; set; }

        }

        public class With
        {
            public string thing { get; set; }
            public DateTime created { get; set; }
            public Content content { get; set; }

        }


    }
}