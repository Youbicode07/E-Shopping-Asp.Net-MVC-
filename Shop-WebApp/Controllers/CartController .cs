using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop_WebApp.Repository;

namespace Shop_WebApp.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartRepository _cartRepo;

        public CartController(ICartRepository cartRepo)
        {
            _cartRepo = cartRepo;
        }

        public async Task<IActionResult> AddItem(int productId, int qty = 1, int redirect = 0)
        {
            try
            {
                var cartCount = await _cartRepo.AddItem(productId, qty);
                return redirect == 0 ? Ok(cartCount) : RedirectToAction("GetUserCart");
            }
            catch (Exception ex)
            {
                // Log the exception
                return BadRequest($"Error adding item to the cart: {ex.Message}");
            }
        }

        public async Task<IActionResult> RemoveItem(int productId)
        {
            try
            {
                var cartCount = await _cartRepo.RemoveItem(productId);
                return RedirectToAction("GetUserCart");
            }
            catch (Exception ex)
            {
                // Log the exception
                return BadRequest($"Error removing item from the cart: {ex.Message}");
            }
        }

        public async Task<IActionResult> GetUserCart()
        {
            try
            {
                var cart = await _cartRepo.GetUserCart();
                return View(cart);
            }
            catch (Exception ex)
            {
                // Log the exception
                return BadRequest($"Error retrieving user cart: {ex.Message}");
            }
        }

        public async Task<IActionResult> GetTotalItemInCart()
        {
            try
            {
                int cartItem = await _cartRepo.GetCartItemCount();
                return Ok(cartItem);
            }
            catch (Exception ex)
            {
                // Log the exception
                return BadRequest($"Error retrieving total items in the cart: {ex.Message}");
            }
        }

        public async Task<IActionResult> Checkout()
        {
            try
            {
                bool isCheckedOut = await _cartRepo.DoCheckout();
                if (!isCheckedOut)
                {
                    // Log the issue
                    return BadRequest("Checkout failed.");
                }

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                // Log the exception
                return BadRequest($"Error during checkout: {ex.Message}");
            }
        }
    }
}
