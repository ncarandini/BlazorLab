using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorLab.Models
{
    public class Ticket
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public decimal Price { get; set; }

        public DateTime Date { get; set; }
    }
}
