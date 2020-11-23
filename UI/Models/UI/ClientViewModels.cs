using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UI.Models.DAL;

namespace UI.Models.UI
{
    public class LoginModel
    {
        private string username;
        private string password;
        private int clientId;

        public string getCookieData()
        {
            return this.username + "," + clientId;
        }
        public KeyValuePair<bool,string> status { get; set; }

        private readonly ECommerceContext _context;
        public LoginModel(string u, string p, ECommerceContext context)
        {
            _context = context;
            this.username = u;
            this.password = p;
            this.status = TryLogin();
        }

        private KeyValuePair<bool,string> TryLogin()
        {
            string eUserName = SecurityModel.Encrypt(this.username);
            string ePassword = SecurityModel.Encrypt(this.password);
            KeyValuePair<bool,string> result;

            var temp = _context.Client.Where(x =>
                x.uname.Equals(eUserName) &&
                x.upass.Equals(ePassword)).FirstOrDefault();
            if (temp==null)
            {
                result = new KeyValuePair<bool,string>(false, "Kullanıcı bulunamadı!!!");
            }
            else if(temp!= null && !temp.is_active)
            {
                result = new KeyValuePair<bool, string>(false, "Kullanıcı aktif değil!!!");
            }
            else if(temp!=null && temp.is_delete)
            {
                result = new KeyValuePair<bool, string>(false, "Kullanıcı silinmiş!!!");
            }
            else
            {
                this.clientId = temp.id;
                result = new KeyValuePair<bool, string>(true, "Giriş başarılı...");
            }

            return result;
        }
    }

    public class SignInModel
    {
        private string name;
        private string desc;
        private string image_url;
        private bool is_corparate;
        private string tcno;
        private string uname;
        private string upass;
        private string email;

        public string Name { get => name; set => name = value; }
        public string Desc { get => desc; set => desc = value; }
        public string Image_url { get => image_url; set => image_url = value; }
        public bool Is_corparate { get => is_corparate; set => is_corparate = value; }
        public string Tcno { get => tcno; set => tcno = value; }
        public string Uname { get => uname; set => uname = value; }
        public string Upass { get => upass; set => upass = value; }
        public string Email { get => email; set => email = value; }
    }

    public class ProfileModel
    {

    }

    public class AccountUtils
    {

    }
}
