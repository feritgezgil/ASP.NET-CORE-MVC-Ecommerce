using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UI.Models.DAL;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace UI.Models.UI
{
    public class CartModel
    {
        private readonly ECommerceContext _context = new ECommerceContext(); 
        private int clientid;
        private readonly IHttpContextAccessor httpContextAccessor = new HttpContextAccessor();
        public CartModel()
        {
            var currentUser = httpContextAccessor.HttpContext.User;
            this.clientid = Int32.Parse(currentUser.Identity.Name.Split(",")[1]);
        }

        public void AddItemToCart(int PID)
        {
            var Cart = this.CreateCart();

            CartItems product;
            try
            {
                product = Cart.CartItems.Where(x => x.ProductId == PID).FirstOrDefault();
            }
            catch (Exception e)
            {
                product = null;
            }

            if (product != null)
            {
                product.ProductQuantity += 1;
                _context.Update(product);
            }
            else
            {
                CartItems item = new CartItems()
                {
                    name = Cart.name,
                    is_active = true,
                    is_delete = false,
                    c_date = DateTime.Now,
                    seo_url = Cart.seo_url,
                    ProductId = PID,
                    ProductQuantity = 1,
                    CartId = Cart.id
                };
                _context.Add(item);
            }
            _context.SaveChanges();
        }

        public bool RemoveItemFromCart(int CIID)
        {
            var temp = _context.CartItem.Find(CIID);
            if (temp != null)
            {
                /*Ürünü sepetten tamamen siler.*/
                //_context.Remove(temp);

                /*Ürünün sepetteki silindi ayarını true yapar.*/
                temp.is_delete = true;
                _context.Update(temp);

                _context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool UpdateCartItems(List<CartItems> cartItems)
        {
            if (cartItems != null)
            {
                _context.UpdateRange(cartItems);
                _context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public string PurchaseCart()
        {
            var cart = this.CreateCart();
            cart.status = (int)Status.Purchased;
            _context.Update(cart);
            _context.SaveChanges();
            this.CreateCart();
            return cart.id + ". Cart Purchased";
        }

        public string DeleteCart()
        {
            var cart = this.CreateCart();
            cart.is_delete = true;
            cart.status = (int)Status.Deleted;
            _context.Update(cart);
            _context.SaveChanges();
            this.CreateCart();
            return cart.id + ". Cart Deleted";
        }

        public string RejectCart()
        {
            var cart = this.CreateCart();
            cart.is_delete = true;
            cart.status = (int)Status.Rejected;
            _context.Update(cart);
            _context.SaveChanges();
            this.CreateCart();
            return cart.id + ". Cart Rejected";
        }

        public Carts CreateCart()
        {
            if (this.clientid > 0)
            {
                var temp = _context.Cart.Include(x => x.CartItems).ThenInclude(ci=>ci.Products).ThenInclude(p=>p.Taxes).Include(x => x.Clients).Where(x => x.ClientId == this.clientid && x.status == (int)Status.Pending);
                if (temp.Count() == 0)
                {
                    Carts cart = new Carts()
                    {
                        name = "Alışveriş Arabası",
                        c_date = DateTime.Now,
                        is_active = true,
                        is_delete = false,
                        seo_url = this.clientid.ToString(),
                        ClientId = this.clientid,
                        status = (int)Status.Pending
                    };
                    _context.Add(cart);
                    _context.SaveChanges();
                    return cart;
                }
                else
                {
                    return temp.FirstOrDefault();
                }
            }
            else
            {
                return null;
            }
        }

    }

    public class MiniCartModel
    {
        public Carts CurrentCart { get; set; }
        public double TotalPrice { get; set; }
        public List<Products> Products { get; set; }

        public MiniCartModel()
        {
            this.Products = new List<Products>();
            this.TotalPrice = 0;
            this.CurrentCart = new CartModel().CreateCart();
            foreach (var item in CurrentCart.CartItems)
            {
                item.Products.quantity = item.ProductQuantity;
                this.Products.Add(item.Products);
                double SubTotalPrice = (item.Products.is_in_camp) ? (item.Products.camp_price * item.ProductQuantity) : (item.Products.price * item.ProductQuantity);
                TotalPrice += SubTotalPrice;
            }
        }
    }
}
