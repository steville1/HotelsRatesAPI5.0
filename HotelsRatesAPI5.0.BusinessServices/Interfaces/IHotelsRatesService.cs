using HotelsRatesAPI5._0.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelsRatesAPI5._0.BusinessServices.Interfaces
{
    public interface IHotelsRatesService
    {
        HotelsRatesModel GetHotelRates(int HotelId, DateTime ArrivalDate);
    }
}
