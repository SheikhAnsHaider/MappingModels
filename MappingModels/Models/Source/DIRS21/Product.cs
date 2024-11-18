using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MappingModels.Models.Source.DIRS21
{
    /// <summary>
    /// This Class tells about the Room Information, Tariff etc.
    /// </summary>
    public class Product
    {
        public int _id { get; set; }
        public Guid? _uuid { get; set; }
        public string? Name { get; set; }
        public string? Type { get; set; }
        public Tariff? Tariff { get; set; }
    }
}
