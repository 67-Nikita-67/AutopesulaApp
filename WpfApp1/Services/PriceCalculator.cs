using AutopesulaApp.Models;
using WpfApp1.Models;

namespace AutopesulaApp.Services
{
    public static class PriceCalculator
    {
        public static decimal CalculatePrice(
            VehicleType vehicleType,
            WashProgram washProgram)
        {
            decimal price = washProgram switch
            {
                WashProgram.Basic => 10m,
                WashProgram.Standard => 15m,
                WashProgram.Premium => 25m,
                _ => 0m
            };

            if (vehicleType == VehicleType.SUV)
            {
                price += 5m;
            }
            else if (vehicleType == VehicleType.Van)
            {
                price += 10m;
            }

            return price;
        }


        public static int CalculateDuration(WashProgram washProgram)
        {
            return washProgram switch
            {
                WashProgram.Basic => 15,
                WashProgram.Standard => 25,
                WashProgram.Premium => 40,
                _ => 0
            };
        }
    }
}