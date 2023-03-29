using DutchTreat.Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DutchTreat.Data
{
	public interface IDutchRepository
	{
        void AddEntity(object model);
        IEnumerable<Order> GetAllOrdersByUser(string username, bool includeItems);
        Order GetOrderById(string username, int id);
        IEnumerable<Order> GetOrders(bool includeItems);
        IEnumerable<Product> GetProducts();
		IEnumerable<Product> GetProductsByCategory(string Category);

		public bool SaveAll();

	}
}