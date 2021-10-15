using HotelsRatesAPI5._0.BusinessServices.Interfaces;
using HotelsRatesAPI5._0.Data.Interfaces;
using HotelsRatesAPI5._0.Data.Models;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HotelsRatesAPI5._0.BusinessServices.Services
{
    public class HotelsRatesService : IHotelsRatesService
    {
       
        private readonly ILogger<HotelsRatesService> _logger;
        private readonly IHotelsRates _hotelsRates;
        public HotelsRatesService(ILogger<HotelsRatesService> logger, IHotelsRates hotelsRates)
        {
         
            _logger = logger;
            _hotelsRates = hotelsRates;
        }
        public HotelsRatesModel GetHotelRates(int HotelId, DateTime ArrivalDate)
        {
            try
            {
                
                var result = _hotelsRates.GetHotelRates(HotelId, ArrivalDate);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.InnerException.ToString());
                return null;
            }

        }
    }
}
