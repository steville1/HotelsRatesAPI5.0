using HotelsRatesAPI5._0.Data.Interfaces;
using HotelsRatesAPI5._0.Data.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelsRatesAPI5._0.Repositories.common
{
    public class HotelsRatesRepository : IHotelsRates
    {
        IConfiguration _configuration { get; }
        private readonly ILogger<HotelsRatesRepository> _logger;
        public HotelsRatesRepository(IConfiguration configuration, ILogger<HotelsRatesRepository> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public HotelsRatesModel GetHotelRates(int HotelId, DateTime ArrivalDate)
        {
            try
            {
                
                //get the Json filepath  
                string filepath = _configuration.GetSection("ReadFiles").GetSection("Path").Value;
                string file = Path.Combine(filepath);
                //Read all the text
                string Json = System.IO.File.ReadAllText(file);
                //deserialize JSON from file  
                var hotels = JsonConvert.DeserializeObject<List<HotelsRatesModel>>(Json);
                //Filtered the result from the Json Data
                var filterHotelResult = hotels.Where(a => a.hotel.hotelID == HotelId).FirstOrDefault();
                if (filterHotelResult == null)
                {
                    return new HotelsRatesModel
                    {
                        responseMessage = "The Hotel Id Is Not Found"
                    };
                }
                var filterHotelRateResult = filterHotelResult.hotelRates.Where(a => a.targetDay.Date == ArrivalDate).ToList();

                
                if (filterHotelRateResult.Count != 0)
                {
                    return new HotelsRatesModel
                    {
                        hotelRates = filterHotelRateResult,
                        responseMessage = "Success"
                    };
                }
                
                else 
                 {
                        return new HotelsRatesModel
                        {
                            responseMessage = "There Is No Hotel Rate Found"
                        };
                 }
                              
                
               
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.InnerException.ToString());
                return null;
            }
        }
    }
}
