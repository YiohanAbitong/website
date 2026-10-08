using Microsoft.AspNetCore.Mvc;
using website.Data;
using website.Models;

namespace website.Controllers
{
public class CartController : Controller
{
private readonly ApplicationDbContext _db;

    public CartController(ApplicationDbContext db)
    {
        _db = db;
    }

    public IActionResult Index()
    {
        var cartItems = _db.CartItems.ToList();

        return View(cartItems);
    }

    public IActionResult AddToCart(int id)
    {
        var product = _db.Products.Find(id);

        if (product == null)
        {
            return NotFound();
        }

        var existingItem = _db.CartItems
            .FirstOrDefault(c => c.ProductId == id);

        if (existingItem != null)
        {
            existingItem.Quantity++;
        }
        else
        {
            var cartItem = new CartItem
            {
                ProductId = product.Id,
                ProductName = product.Name,
                Price = product.Price,
                Quantity = 1
            };

            _db.CartItems.Add(cartItem);
        }

        _db.SaveChanges();

        return RedirectToAction("Index", "Products");
    }

    public IActionResult Remove(int id)
    {
        var item = _db.CartItems.Find(id);

        if (item != null)
        {
            _db.CartItems.Remove(item);
            _db.SaveChanges();
        }

        return RedirectToAction("Index");
    }

    public IActionResult UpdateQuantity(int id, int quantity)
    {
        var item = _db.CartItems.Find(id);

        if (item != null)
        {
            if (quantity <= 0)
            {
                _db.CartItems.Remove(item);
            }
            else
            {
                item.Quantity = quantity;
            }

            _db.SaveChanges();
        }

        return RedirectToAction("Index");
    }
}

}
