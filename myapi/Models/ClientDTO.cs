using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace myapi.Models
{
    public class ClientDTO
    {
        public int Id { get; set; }
        public string FIO { get; set; }
        public long Passport { get; set; }
    }
}