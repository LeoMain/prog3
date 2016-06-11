using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace myapi.Models
{
    public class WatercraftDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string WaterCraftType { get; set; }
        public string WaterCraftCondition { get; set; }
        public decimal CostRate { get; set; }
    }
}