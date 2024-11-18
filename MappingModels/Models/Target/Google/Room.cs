using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MappingModels.Models.Target.Google
{
    /// <summary>
    /// This Class Tells about Room Details.
    /// </summary>
    public class Room
    {
        public int _id { get; set; }
        public string? Name { get; set; }
        public string? Type { get; set; }
        public List<Rate>? Rates { get; set; }
    }
}
