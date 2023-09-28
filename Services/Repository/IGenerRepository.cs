
using FinalApi.Dto;
using FinalApi.Models;
using FinalApi.Request;
using System.Linq.Expressions;

namespace FinalApi.Services.Repository
{
    public interface IGenerRepository<T> where T : class
    {
        IEnumerable<T> GetOrders();

        T GetOrderById(int id);

        T CreateOrder(T entity);
        void RemoveItemFromOrder(int orderId, int ItemId);

        IEnumerable<T> GetOrderByItem(string Name);
        Task<IEnumerable<T>> CreateCustomerVipAsync();
    }
}
