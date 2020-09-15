using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OauthClient.Models
{
    public class Weather
    {
        
        public DateTime Date { get; set; }
        public decimal TemperatureC {get;set;}
        public decimal TemperatureF { get; set; }
        public string Summary { get; set; }
    }
}
