using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MappingModels.BusinessLogic
{
    public static class CustomExtensionsMethods
    {
        /// <summary>
        /// The Reason why we wrote 2 Extension Mehtods for Address Because there is Possiblity that
        /// Google Model Address and DIRS21 Model Address have different properties.
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        // Helper method to map DIRS21 Address to Google Address
        public static Models.Target.Google.Address ToGoogleAddress(this Models.Source.DIRS21.Address address)
        {
            return new Models.Target.Google.Address
            {
                Street = address.Street,
                Zip = string.Empty
                // Map other fields as needed, e.g. Zip = address.SomeProperty
            };
        }
        public static Models.Source.DIRS21.Address ToDIRS21Address(this Models.Target.Google.Address address)
        {
            return new Models.Source.DIRS21.Address
            {
                Street = address.Street,
                Type = string.Empty
                // Map other fields as needed, e.g., Zip = address.SomeProperty
            };
        }
    }
}
