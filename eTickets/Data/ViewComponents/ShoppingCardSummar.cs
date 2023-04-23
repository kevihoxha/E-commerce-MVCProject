using eTickets.Data.Card;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace eTickets.Data.ViewComponents
{
    [ViewComponent]
    public class ShoppingCardSummar:ViewComponent
    {
        private readonly ShoppingCard _shoppingCard;
        public ShoppingCardSummar(ShoppingCard shoppingCard)
        {
            _shoppingCard= shoppingCard;
        }
        public IViewComponentResult Invoke()
        {
            var items= _shoppingCard.GetShoppingCardItems();
            return View(items.Count);
        }
    }
}
