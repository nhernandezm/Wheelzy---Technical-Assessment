using System.Linq;
using CarSalesDataAccessLayer.Models;
using CarSalesDataAccessLayer.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace CarSalesDataAccessLayer
{
    public class CarSalesRepository
    {
        CarSalesContext carSalesContext;

        public CarSalesRepository()
        {
            carSalesContext = new CarSalesContext();
        }

        public List<ResultPotentialBuyer> GetBuyerDetails()
        {
            var detailBuyerQuery = carSalesContext.PotentialBuyers
                .Join(carSalesContext.Buyers, pb => pb.BuyerId, b => b.BuyerId, (pb, b) => new { pb, b })
                .Join(carSalesContext.Statuses, temp => temp.pb.StatusId, s => s.StatusId, (temp, s) => new { temp.pb, temp.b, s })
                .Join(carSalesContext.Cars, temp => temp.pb.CarId, c => c.CarId, (temp, c) => new { temp.pb, temp.b, temp.s, c })
                .Join(carSalesContext.Models, temp => temp.c.ModelId, m => m.ModelId, (temp, m) => new { temp.pb, temp.b, temp.s, temp.c, m })
                .Join(carSalesContext.Makes, temp => temp.c.MakeId, mk => mk.MakeId, (temp, mk) => new ResultPotentialBuyer
                {
                    BuyerName = temp.b.FirstName + " " + temp.b.LastName,
                    Amount = temp.pb.Amount,
                    StatusName = temp.s.StatusName,
                    RegistrationNumber = temp.c.RegistrationNumber,
                    ModelName = temp.m.ModelName,
                    MakeName = mk.MakeName
                });

            return detailBuyerQuery.ToList();
        }

        //
        public async Task<List<OrderDTO>> GetOrders(DateTime dateFrom, DateTime dateTo, List<int> customerIds, List<int> statusIds, bool? isActive)
        {
            var query = carSalesContext.Orders.AsQueryable();

            // Apply filters based on the provided parameters
            if (dateFrom != default(DateTime))
            {
                query = query.Where(o => o.OrderDate >= dateFrom);
            }

            if (dateTo != default(DateTime))
            {
                query = query.Where(o => o.OrderDate <= dateTo);
            }

            if (customerIds != null && customerIds.Any())
            {
                query = query.Where(o => customerIds.Contains(o.CustomerID));
            }

            if (statusIds != null && statusIds.Any())
            {
                query = query.Where(o => statusIds.Contains(o.StatuID));
            }

            if (isActive.HasValue)
            {
                query = query.Where(o => o.IsActive == isActive.Value);
            }

            // Execute the query and return the result
            var orders = await query.ToListAsync();

            // Map the result to the OrderDTO model if needed
            var orderDTOs = orders.Select(o => new OrderDTO
            {
                CustomerID = o.CustomerID,
                IsActive = o.IsActive,
                OrderDate = o.OrderDate,
                OrderID = o.OrderID,
                StatuID = o.StatuID
            }).ToList();

            return orderDTOs; 
        }

    }
}
