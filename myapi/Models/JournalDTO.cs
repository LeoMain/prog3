using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace myapi.Models
{
    public class JournalDTO
    {
        public int Id { get; set; }
        public string Client { get; set; }
        public string Water { get; set; }
        public string PayMethod { get; set; }
        public string WaterCraftName { get; set; }
        public string WaterCraftType { get; set; }
        public string InstructionType { get; set; }
        public string Instructor { get; set; }
        public System.DateTime Date { get; set; }
        public decimal Cost { get; set; }
    }
}