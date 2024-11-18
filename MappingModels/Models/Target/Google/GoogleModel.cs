using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MappingModels.Models.Target.Google
{
    /// <summary>
    /// Represents a Google Model.
    /// Rest of the Properties are self-explanatory.
    /// </summary>
    public class GoogleModel
    {
        public int _id { get; set; }
        public List<Traveler>? Travelers { get; set; }
        public List<Room>? Rooms { get; set; }
        public Total? Total { get; set; }
    }









}
