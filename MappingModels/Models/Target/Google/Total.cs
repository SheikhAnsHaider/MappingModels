using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MappingModels.Models.Target.Google
{
    /// <summary>
    /// This Class has some properties which tells about the Total Amount.
    /// </summary>
    public class Total
    {
        public decimal Sum { get; set; }
        public string? Currency { get; set; }
    }
}
