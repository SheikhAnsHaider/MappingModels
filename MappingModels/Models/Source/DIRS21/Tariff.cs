using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MappingModels.Models.Source.DIRS21
{
    /// <summary>
    /// This Class Tells about Room Tariff etc.
    /// </summary>
    public class Tariff
    {
        public int _id { get; set; }
        public Guid? _uuid { get; set; }
        public string? Name { get; set; } = string.Empty;
        public string? Type { get; set; } = string.Empty;
    }
}
