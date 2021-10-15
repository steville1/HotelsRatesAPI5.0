using HotelsRatesAPI5._0.BusinessServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelsRatesAPI5._0.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HotelsRatesController  : ControllerBase
    {

        private readonly IHotelsRatesService _hotelsRatesService;
        private readonly ILogger<HotelsRatesController> _logger;
        public HotelsRatesController(IHotelsRatesService hotelsRatesService)
        {
            _hotelsRatesService = hotelsRatesService;
            
        }
        [Route("api/GetHotelsRates/{HotelId}/{ArrivalDate}")]
        [HttpGet]
        public IActionResult GetHotelsRates(int HotelId, DateTime ArrivalDate)
        {
            try
            {
               var results = _hotelsRatesService.GetHotelRates(HotelId, ArrivalDate);
               return Ok(results);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return null;
            }

        }
        
    }
}
