using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIsProject.Errors;
using Talabat.Core.Entities;
using Talabat.Core.Repositories;

namespace Talabat.APIsProject.Controllers
{
    public class BasketsController : APIBaseController
    {
        private readonly IBasketRepository _basketRepo;

        public BasketsController(IBasketRepository basketRepo) {
            _basketRepo = basketRepo;
        }

        //Get     Or ReCreate ??
        [HttpGet("{BasketId}")]
        public async Task<CustomerBasket> GetCustomerBasket(string BasketId)
        {
            var Basket = await _basketRepo.GetBasketAsync(BasketId);
            return Basket is null ? new CustomerBasket(BasketId) : (Basket);
        }


        //Update  Or Create new basket
        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasket customerBasket)
        {
            var CreatedOrUpdated = await _basketRepo.UpdateBasketAsync(customerBasket);
            return CreatedOrUpdated is null
               ? BadRequest(new ApiResponses(StatusCodes.Status400BadRequest))
               : Ok(CreatedOrUpdated);

        }
        [HttpDelete("{BasketId}")]
        public async Task<ActionResult<bool>> DeleteBasket(string BasketId)
        {
            return await _basketRepo.DeleteBasketAsync(BasketId);
        }
    }
}
