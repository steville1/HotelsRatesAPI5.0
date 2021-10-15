using HotelsRatesAPI5._0.BusinessServices.Interfaces;
using HotelsRatesAPI5._0.BusinessServices.Services;
using HotelsRatesAPI5._0.Controllers;
using HotelsRatesAPI5._0.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;


namespace HotelsRatesAPI5._0.UnitTests
{
    public class HotelRateServiceTests
    {
        private List<HotelsRatesModel> _hotelRatesModels;
        private HotelsRatesService _service;
       // private JsonMapper _mapper;

        [SetUp]
        public void Setup()
        {
            _hotelRatesModels = new List<HotelsRatesModel>{
                new HotelsRatesModel
                {
                    hotel = new Hotel { classification = 1, hotelID = 1, name = "Test Hotel", reviewscore = 1.2 },
                    hotelRates = new List<HotelRate> {
                        new HotelRate {
                            adults=2,
                            los=1,
                            price= new Price {currency = "EUR", numericFloat=325.5, numericInteger=32550},
                            rateDescription = "Test Description",
                            rateID = "_TESTID",
                            rateName = "My Test RateName",
                            rateTags = new List<RateTag>{new RateTag{name = "Myrate", shape = true}},
                            targetDay = DateTime.Now.AddDays(5).Date
                            },
                        new HotelRate {
                            adults=2,
                            los=1,
                            price= new Price {currency = "EUR", numericFloat=325.5, numericInteger=32550},
                            rateDescription = "Test Description",
                            rateID = "_TESTID",
                            rateName = "My Test RateName",
                            rateTags = new List<RateTag>{new RateTag{name = "Myrate", shape = true}},
                            targetDay = DateTime.Now.AddDays(4).Date
                        },
                        new HotelRate {
                            adults=2,
                            los=1,
                            price= new Price {currency = "EUR", numericFloat=325.5, numericInteger=32550},
                            rateDescription = "Test Description",
                            rateID = "_TESTID",
                            rateName = "My Test RateName",
                            rateTags = new List<RateTag>{new RateTag{name = "Myrate", shape = true}},
                            targetDay = DateTime.Now.AddDays(3).Date
                        }
                    }

                },

                new HotelsRatesModel
                {
                    hotel = new Hotel { classification = 1, hotelID = 2, name = "Test2 Hotel", reviewscore = 5.2 },
                    hotelRates = new List<HotelRate> {
                        new HotelRate {
                            adults=2,
                            los=1,
                            price= new Price {currency = "EUR", numericFloat=425.5, numericInteger=42550},
                            rateDescription = "Test2 Description",
                            rateID = "_TESTID2",
                            rateName = "My Test2 RateName",
                            rateTags = new List<RateTag>{new RateTag{name = "Myrate2", shape = true}},
                            targetDay = DateTime.Now.AddDays(5).Date
                            },
                        new HotelRate {
                            adults=2,
                            los=1,
                            price= new Price {currency = "EUR", numericFloat=425.5, numericInteger=42550},
                            rateDescription = "Test2 Description",
                            rateID = "_TESTID2",
                            rateName = "My Test2 RateName",
                            rateTags = new List<RateTag>{new RateTag{name = "Myrate2", shape = true}},
                            targetDay = DateTime.Now.AddDays(4).Date
                            }
                    }
                }

            };
        }

        [Test]
        public void GetHotelRates_Returns_Correct_Number_Of_Results()
        {
            //Arrange
            var mockService = new Mock<IHotelsRatesService>();
            

            var filteredHotel = _hotelRatesModels.FirstOrDefault(h => h.hotel.hotelID == 1);
            var expectedFilteredRates = filteredHotel.hotelRates
                .Where(r => r.targetDay.Date == DateTime.Now.AddDays(4).Date);

            var hotelModel = new HotelsRatesModel { hotel = filteredHotel.hotel, hotelRates = expectedFilteredRates.ToList() };

            mockService
                .Setup(h => h.GetHotelRates(It.IsAny<int>(), It.IsAny<DateTime>()))
                .Returns(() => hotelModel);

            var controller = new HotelsRatesController(mockService.Object);

            //act
            IActionResult responseObject = controller.GetHotelsRates(1, DateTime.Now.AddDays(4));
            var response = responseObject as OkObjectResult;

            //assert
            Assert.NotNull(response);
            Assert.AreEqual(StatusCodes.Status200OK, response.StatusCode);
            Assert.IsInstanceOf<HotelsRatesModel>(response.Value);
            Assert.AreEqual(expectedFilteredRates.Count(), ((HotelsRatesModel)response.Value).hotelRates.Count);
        }
    }
}
