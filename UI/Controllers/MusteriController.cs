using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UI.Models;
using UI.Models.DAL;
using UI.Models.UI;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace UI.Controllers
{
    public class MusteriController : Controller
    {
        private readonly ECommerceContext _context;
        private IHttpContextAccessor _httpContextAccessor;
        public MusteriController(ECommerceContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        public IActionResult Index()
        {

            return View();
        }

        public IActionResult Login()
        {
             return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string uname, string upass)
        {
            LoginModel model = new LoginModel(uname, upass, _context);

            if (model.status.Key)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,model.getCookieData())
                };
                var uIdentity = new ClaimsIdentity(claims, "login");
                ClaimsPrincipal principal = new ClaimsPrincipal(uIdentity);
                await HttpContext.SignInAsync(principal);
                string returnUrl = Request.Query["ReturnUrl"];
                if (string.IsNullOrEmpty(returnUrl))
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return Redirect(returnUrl);
                }
            }
            ViewData["msg"] = model.status.Value;
            return View(model);
        }

        [Authorize]
        public async void Logout()
        {
            await HttpContext.SignOutAsync();
        }

        public IActionResult Signin()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Signin(SignInModel model, IFormFile file)
        {
            string eUsername = SecurityModel.Encrypt(model.Uname);
            string ePassword = SecurityModel.Encrypt(model.Upass);
            string seoUrl = Tools.toSlug(model.Uname);
            string aCode = Tools.generateActivationCode();
            string imageUrl = null;
            if (file != null && file.Length > 0)
            {
                try
                {
                    string newFileName = seoUrl + DateTime.Now.ToString("_ddMMyyyy_HHmmss") + "." + file.FileName.Split('.').LastOrDefault();

                    var originalDirectory = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\img\profileImages", newFileName);

                    using (var stream = new FileStream(originalDirectory, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    imageUrl = "/img/profileImages/" + newFileName;
                }
                catch (Exception ex)
                {
                }
            }
            Clients client = new Clients()
            {
                name = model.Name,
                desc = model.Desc,
                is_active = false,
                is_delete = false,
                is_corparate = model.Is_corparate,
                tcno = model.Tcno,
                uname = eUsername,
                upass = ePassword,
                c_date = DateTime.Now,
                seo_url = seoUrl,
                activation_code = aCode,
                image_url = imageUrl,
                email = model.Email
            };
            _context.Add(client);
            _context.SaveChanges();
            string localHost = _httpContextAccessor.HttpContext.Request.Host.Value;
            string aCodeMailBody = "<a href ='http://" + localHost +
                "/musteri/activateclient/?activation_code=" + aCode +
                "&username=" + model.Uname + "'>Aktive Edin</a>";
            Tools.sendMail(model.Email, "Aktivasyon Bilgisi", aCodeMailBody);
            return View(model);
        }

        public IActionResult Signout()
        {
            return View();
        }

        public IActionResult ForgetPassword()
        {
            return View();
        }

        public IActionResult Profile(int CID)
        {
            return View();
        }

        public IActionResult ActivateClient(string activation_code, string username)
        {
            Clients temp = _context.Client
                .Where(x =>
                    x.uname.Equals(username) &&
                    x.activation_code.Equals(activation_code))
                .FirstOrDefault();

            if (temp != null)
            {
                temp.is_active = true;
                temp.activation_code = Tools.generateActivationCode();
                _context.Update(temp);
                _context.SaveChanges();
            }
            return RedirectToAction("Login");
        }
    }
}
