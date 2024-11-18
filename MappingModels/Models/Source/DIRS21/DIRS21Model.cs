using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MappingModels.Models.Source.DIRS21
{
    /// <summary>
    /// This Represents a DIRS21 Model.
    /// Rest of the Properties are pretty much self explanatory.
    /// </summary>
    public class DIRS21Model
    {
        public int _id { get; set; }
        public List<Guest>? Guests { get; set; }
        public List<Product>? Products { get; set; }
        public Total? Total { get; set; }
        public Total? Total2 { get; set; }
    }
}
