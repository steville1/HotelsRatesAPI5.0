using HotelsRatesAPI5._0.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelsRatesAPI5._0.Data.Interfaces
{
    public interface IHotelsRates
    {
        //Task<string> GetFiles(string HotelId, string ArrivalDate);
        HotelsRatesModel GetHotelRates(int HotelId, DateTime ArrivalDate);
    }
}
