using CarSalesDataAccessLayer;
using Microsoft.AspNetCore.Mvc;

namespace CarSalesApi.Controllers
{
    public class CarSalesController : Controller
    {

        CarSalesRepository carSalesRepository;

        public CarSalesController(CarSalesRepository carSalesRepository)
        {
            this.carSalesRepository = carSalesRepository;
        }

        [HttpGet]
        [Route("api/buyersdetails")]
        public IActionResult GetBuyerDetails()
        {
            var buyers = carSalesRepository.GetBuyerDetails();
            return Json(buyers);
        }

        [HttpGet]
        [Route("api/orders")]
        public async Task<IActionResult> GetOrders(DateTime dateFrom, DateTime dateTo, List<int> customerIds, List<int> statusIds, bool? isActive)
        {
            var orders = await carSalesRepository.GetOrders(dateFrom, dateTo, customerIds, statusIds, isActive);
            return Json(orders);
        }


    }
}
