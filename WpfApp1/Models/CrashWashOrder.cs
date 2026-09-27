using WpfApp1.Models;

namespace AutopesulaApp.Models
{
    public class CarWashOrder
    {
        public int Id { get; set; }

        public VehicleType VehicleType { get; set; }

        public WashProgram WashProgram { get; set; }

        public decimal Price { get; set; }

        public int Duration { get; set; }
    }
}