using eTickets.Data.Card;
using eTickets.Data.Services;
using eTickets.Data.Static;
using eTickets.Data.ViewModels;
using eTickets.Models.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace eTickets.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly IMoviesService _moviesService;
        private readonly ShoppingCard _shoppingCard;
        private readonly IOrdersService _ordersService;


        public OrdersController(IMoviesService moviesService,ShoppingCard shoppingCard, IOrdersService ordersService)
        {
            _moviesService = moviesService;
            _shoppingCard = shoppingCard;
            _ordersService = ordersService;
        }
        public async Task  <IActionResult> Index()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string userRole = User.FindFirstValue(ClaimTypes.Role);
            var orders =  await _ordersService.GetOrdersByUserIdAndRoleAsync(userId ,userRole);
            return View(orders);
        }
        public IActionResult ShoppingCard()
        {
            var items = _shoppingCard.GetShoppingCardItems();
            _shoppingCard.ShoppingCardItems = items;

            var response = new ShoppingCardVM()
            {
                ShoppingCard = _shoppingCard,
                ShoppingCardTotal = _shoppingCard.GetShoppingCardTotal(),
            };
            return View(response);
        }
        public async Task<IActionResult> AddToShoppingCard(int id)
        {
            var item = await _moviesService.GetMovieByIdAsync(id);
            if(item != null)
            {
                _shoppingCard.AddItemToCard(item);
            }
            return RedirectToAction (nameof(ShoppingCard));
        }
        public async Task<IActionResult> RemoveItemFromShoppingCard(int id)
        {
            var item = await _moviesService.GetMovieByIdAsync(id);

            if (item != null)
            {
                _shoppingCard.RemoveItemFromCard(item);
            }
            return RedirectToAction(nameof(ShoppingCard));
        }
        public async Task<IActionResult> CompleteOrder()
        {
             var items = _shoppingCard.GetShoppingCardItems();
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier); 
            string userEmailAdress = User.FindFirstValue(ClaimTypes.Email); 
            await _ordersService.StoreOrderAsync(items, userId, userEmailAdress);
            await _shoppingCard.ClearShoppingCardAsync();
            return View("OrderCompleted");

        }
    }
    
}
