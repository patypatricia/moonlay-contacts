using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moonlay.Contacts.Models
{
    public class ContactDto
    {
        public IReadOnlyList<string> Names { get; set; }
        public int Id { get; set; }
    }
}
