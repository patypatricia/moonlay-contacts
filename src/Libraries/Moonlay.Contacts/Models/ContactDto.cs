using System.Collections.Generic;

namespace Moonlay.Contacts.Models
{
    public class ContactDto
    {
        public IReadOnlyList<string> Names { get; set; }
        public int Id { get; set; }
    }
}