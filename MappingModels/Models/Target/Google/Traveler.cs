using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MappingModels.Models.Target.Google
{
    /// <summary>
    /// This Class which represents information about the Traveler/Guest.
    /// </summary>
    public class Traveler
    {
        public string? Firstname { get; set; }
        public string? LastName { get; set; }
        public Address? Address { get; set; }
    }
}
