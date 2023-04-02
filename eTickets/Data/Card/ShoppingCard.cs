using eTickets.Models;
using eTickets.Models.Data;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Data.Card
{
    public class ShoppingCard
    {
        public AppDbContext _context { get; set; }
        public string ShoppingCardId { get; set; }
        public List<ShoppingCardItem> ShoppingCardItems { get; set; }
        public ShoppingCard(AppDbContext context)
        {
            _context= context;
        }
        public static ShoppingCard GetShoppingCard(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            var context = services.GetService<AppDbContext>();
            string cardId = session.GetString("CardId") ?? Guid.NewGuid().ToString();
            session.SetString("CardId", cardId);
            return new ShoppingCard(context) { ShoppingCardId = cardId };
        }
        public void AddItemToCard(Movie movie)
        {
            var ShoppingCardItem = _context.ShoppingCardItems.FirstOrDefault(n=>n.Movie.Id== movie.Id&& n.ShoppingCardId==ShoppingCardId);
            if(ShoppingCardItem == null)
            {
                ShoppingCardItem = new ShoppingCardItem()
                {
                    ShoppingCardId = ShoppingCardId,
                    Movie = movie,
                    Amount = 1

                };
                _context.ShoppingCardItems.Add(ShoppingCardItem);
            }
            else
            {
                ShoppingCardItem.Amount++;
            }
            _context.SaveChanges();
        }
        public void RemoveItemFromCard(Movie movie)
        {
            var ShoppingCardItem = _context.ShoppingCardItems.FirstOrDefault(n => n.Movie.Id == movie.Id && n.ShoppingCardId == ShoppingCardId);
            if (ShoppingCardItem != null)
            {
                if (ShoppingCardItem.Amount > 1)
                {
                    ShoppingCardItem.Amount--;
                }
                else
                {
                    _context.ShoppingCardItems.Remove(ShoppingCardItem);
                }
            }
            _context.SaveChanges();
        }
        public List<ShoppingCardItem> GetShoppingCardItems()
        {
            return ShoppingCardItems ?? (ShoppingCardItems = _context.ShoppingCardItems.Where(n=> n.ShoppingCardId == ShoppingCardId).Include(n=>n.Movie).ToList());
        }
        public double GetShoppingCardTotal()
        {
            var total = _context.ShoppingCardItems.Where(n => n.ShoppingCardId == ShoppingCardId).Select(n=> n.Movie.Price * n.Amount).Sum();
            return total;
        }
        public async  Task ClearShoppingCardAsync()
        {
            var items = await _context.ShoppingCardItems.Where(n => n.ShoppingCardId == ShoppingCardId).ToListAsync();
            _context.ShoppingCardItems.RemoveRange(items);
            await _context.SaveChangesAsync();


        }
    }
}
