using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MappingModels.Models.Target.Google
{
    /// <summary>
    /// This Class represents Rate/s of a certin Room.
    /// </summary>
    public class Rate
    {
        public int _id { get; set; }
        public string? Name { get; set; }
        public string? Type { get; set; }
    }
}
