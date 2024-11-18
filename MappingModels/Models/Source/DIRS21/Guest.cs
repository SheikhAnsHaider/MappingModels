using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MappingModels.Models.Source.DIRS21
{
    /// <summary>
    /// This class tells about the Guest/Traveler Details.
    /// </summary>
    public class Guest
    {
        public string? Firstname { get; set; } = string.Empty;
        public string? LastName { get; set; } = string.Empty;
        public List<Address>? Addresses { get; set; }
    }
}
