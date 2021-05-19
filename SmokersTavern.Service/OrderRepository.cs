using SmokersTavern.Data;
using SmokersTavern.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmokersTavern.Service
{
    public class OrderRepository : IOrderRepository
    {
        private ApplicationDbContext _datacontext = null;
        private readonly IRepositoryService<Order> _orderRepository;

        public OrderRepository()
        {
            _datacontext = new ApplicationDbContext();
            _orderRepository = new RepositoryService<Order>(_datacontext);
        }

        public Order GetById(int id)
        {
            return _orderRepository.GetById(id);
        }

        public List<Order> GetAll()
        {
            return _orderRepository.GetAll().ToList();
        }

        public void Insert(Order model)
        {
            _orderRepository.Insert(model);
        }

        public void Update(Order model)
        {
            _orderRepository.Update(model);
        }

        public void Delete(Order model)
        {
            _orderRepository.Delete(model);
        }

        public IEnumerable<Order> Find(Func<Order, bool> predicate)
        {
            return _orderRepository.Find(predicate).ToList();
        }

        public void Dispose()
        {
            _datacontext.Dispose();
            _datacontext = null;
        }
    }
}
