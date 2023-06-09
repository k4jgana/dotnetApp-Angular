﻿using DutchTreat.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DutchTreat.Data
{
	public class DutchRepository : IDutchRepository
	{
		private readonly DutchContext ctx;
        private readonly ILogger<DutchRepository> logger;

        public DutchRepository(DutchContext ctx,ILogger<DutchRepository> logger)
		{
			this.ctx = ctx;
            this.logger = logger;
        }

        public void AddEntity(object model)
        {
            ctx.Add(model);
        }

        public IEnumerable<Order> GetAllOrdersByUser(string username, bool includeItems)
        {
            if (includeItems)
            {

                return ctx.Orders
                 .Where(o=>o.User.UserName==username)
                .Include(o => o.Items)
                .ThenInclude(i => i.Product)
                .ToList();
            }
            else { return ctx.Orders.Where(o => o.User.UserName == username).ToList(); }

        }

        public Order GetOrderById(string username, int id)
        {
			return ctx.Orders
                .Include(o => o.Items)
				.ThenInclude(i => i.Product)
                .Where(o => o.Id == id && o.User.UserName==username)
				.FirstOrDefault();
        }

        public IEnumerable<Order> GetOrders(bool includeItems)
        {
			if (includeItems) 
			{

                return ctx.Orders
                .Include(o => o.Items)
                .ThenInclude(i => i.Product)
                .ToList();
            }
			else { return ctx.Orders.ToList(); }
            
        }

        public IEnumerable<Product> GetProducts()
		{
			logger.LogInformation("GetProducts was called");
			return ctx.Products
				.OrderBy(p => p.Title)
				.ToList();
		}

		public IEnumerable<Product> GetProductsByCategory(string Category)
		{
			return ctx.Products.Where(p => p.Category == Category).ToList();
		}


		public bool SaveAll() 
		{
			return ctx.SaveChanges() > 0;
		}

	}
}
