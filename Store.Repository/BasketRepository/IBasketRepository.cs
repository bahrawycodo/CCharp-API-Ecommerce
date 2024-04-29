using Store.Repository.BasketRepository.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Repository.BasketRepository
{
    public interface IBasketRepository
    {

        Task<CustomerBasket> GetBasketAsync(String basketId);
        Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket);
        Task<bool> DeleteBasketAsync(string basketId);
    } 
}
