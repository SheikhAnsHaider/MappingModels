using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MappingModels.Models.Source.DIRS21
{
    /// <summary>
    /// This Class Tells about Total Amount and Currency.
    /// </summary>
    public class Total
    {
        public decimal Sum { get; set; }
        public string? Currency { get; set; }
    }
}
