using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MappingModels.Models.Source.DIRS21;
using MappingModels.Models.Target.Google;
using Riok.Mapperly.Abstractions;
using MappingModels.BusinessLogic;
namespace MappingModels
{
    [Mapper(PropertyNameMappingStrategy = PropertyNameMappingStrategy.CaseInsensitive)]
    public partial class ModelMapper
    {
        // Maps GoogleModel to DIRS21Model or vice versa.
        // It Auto-map all identical property names BUT you can override them and For Complex Types
        // Custom Methods.
        // You can also use Partial Classes to split the Mapping Logic into multiple files.
        // You can also use Custom Mapping Methods to map Complex Types.
        //In this Class You have to define which Model you want to map to which Model. And then their Complex Properties. Properties 
        //with same name will be auto mapped. You can also override their behavior if you want to add certain logic in there. 
        

        
        [MapProperty(nameof(Models.Source.DIRS21.DIRS21Model.Guests), nameof(Models.Target.Google.GoogleModel.Travelers))]
        [MapProperty(nameof(Models.Source.DIRS21.DIRS21Model.Products), nameof(Models.Target.Google.GoogleModel.Rooms))]
        public static partial GoogleModel MapToGoogleModel(DIRS21Model dirs21Model);

        [MapProperty(nameof(GoogleModel.Travelers), nameof(DIRS21Model.Guests))]
        [MapProperty(nameof(GoogleModel.Rooms), nameof(DIRS21Model.Products))]
        public static partial DIRS21Model MapToDRIS21Model(GoogleModel googleModel);
        /// <summary>
        /// This Method Maps Travelers From GoogleModel to DIRS21Model's Guests.
        /// </summary>
        /// <param name="GuestsTOTravelersMapping">Guests TO Travelers Mapping</param>
        /// <returns></returns>
        public static List<Traveler> MapGuestsToTravelers(List<Guest> guests) =>
           guests?.Select(guest => new Traveler
           {
               Firstname = guest.Firstname,
               LastName = guest.LastName,
               Address = guest.Addresses?.FirstOrDefault()?.ToGoogleAddress()
           })
           .ToList() ?? new List<Traveler>();
        /// <summary>
        /// This Method Maps Rooms Rates From GoogleModel to DIRS21Model's Products Tarrifs.
        /// </summary>
        /// <param name="Rooms/Products">This Basically Mapping Rates/Traffis </param>
        /// <returns></returns>
        public static List<Room> MapRatesToTariff(List<Product> product) =>
        product?.Select(guest => new Room
        {
            _id = guest._id,
            Name = guest.Name,
            Type = guest.Type,
            Rates = guest.Tariff != null ? new List<Rate>
              {
                        new Rate
                        {
                             _id = guest.Tariff._id,
                             Name = guest.Tariff.Name,
                             Type = guest.Tariff.Type
                        }
              } : new List<Rate>()
        })
       .ToList() ?? new List<Room>();

        /// <summary>
        /// This Method Maps Travelers From GoogleModel to DIRS21Model's Guests.
        /// </summary>
        /// <param name="TravelersTOGuestsMapping">Travelers TO Guests Mapping</param>
        /// <returns></returns>
        public static List<Guest> MapTravelersToGuests(List<Traveler> travelers) =>
        travelers?.Select(traveler => new Guest
        {
            Firstname = traveler.Firstname,
            LastName = traveler.LastName,
            Addresses = traveler.Address != null ? new List<Models.Source.DIRS21.Address> 
            { traveler.Address.ToDIRS21Address() } : null
        })
        .ToList() ?? new List<Guest>();
        /// <summary>
        /// This Method Maps DIRS21Model's Products Tarrifs to Rooms Rates From GoogleModel.
        /// </summary>
        /// <param name="Rooms/Products">This Basically Mapping Traffis/Rates </param>
        /// <returns></returns>
        public static List<Product> MapTariffToRates(List<Room> rooms) =>
        rooms?.Select(room => new Product
        {
            _id = room._id,
            Name = room.Name,
            Type = room.Type,
            Tariff = room.Rates?.FirstOrDefault() != null ? new Tariff
            {
                _id = room.Rates.First()._id,
                Name = room.Rates.First().Name,
                Type = room.Rates.First().Type
            } : null
        })
        .ToList() ?? new List<Product>();
    }
}
