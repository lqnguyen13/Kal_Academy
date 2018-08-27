using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CartAPI.Model
{
 public   interface ICartRepository
    {
        Task<Cart> GetCartAsync(string cartId);
        IEnumerable<string> GetUser();
        Task<Cart> UpdateCartAsync(Cart basket);
        Task<bool> DeleteCartAsync(string id);

    }
}
