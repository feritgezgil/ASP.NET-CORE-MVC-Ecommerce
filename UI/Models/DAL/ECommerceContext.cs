using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace UI.Models.DAL
{
    public class ECommerceContext : DbContext
    {
       


        private string ConnectionString;
        public ECommerceContext(DbContextOptions<ECommerceContext> options) : base(options)
        {

        }

        public ECommerceContext():base()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            var configuration = builder.Build();
            this.ConnectionString = configuration.GetConnectionString("ECommerceConnection");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(this.ConnectionString);
            }
        }

        #region DbSet's Product
        /*--- Product Group Begin ---*/
        public DbSet<Products> Product { get; set; }
        public DbSet<PrelPIs> PrelPI { get; set; }
        public DbSet<PrelCs> PrelC { get; set; }
        public DbSet<ProductImagess> ProductImages { get; set; }
        public DbSet<Categories> Category { get; set; }
        public DbSet<Menseis> Mensei { get; set; }
        public DbSet<Taxes> Tax { get; set; }
        /*--- Product Group End ---*/
        #endregion

        #region DbSet's Client
        /*--- Client Group Begin ---*/
        public DbSet<Clients> Client { get; set; }
        public DbSet<Addresses> Address { get; set; }
        public DbSet<Emails> Email { get; set; }
        public DbSet<Phones> Phone { get; set; }
        /*--- Client Group End ---*/
        #endregion

        #region DbSet's UI
        /*--- UI Group Begin ---*/
        public DbSet<Menus> Menu { get; set; }
        public DbSet<MenuItems> MenuItem { get; set; }
        /*--- UI Group End ---*/
        #endregion

        #region DbSet's Shopping
        /*--- Shopping Group Begin ---*/

        public DbSet<Carts> Cart { get; set; }
        public DbSet<CartItems> CartItem { get; set; }

        /*--- Shopping Group End ---*/
        #endregion

        /*
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Öncelikli anahtarı olmayan tablolar için HasNoKey bildiriminin yapılması gerekmekte,
            //entity yapısı anahtarsız tabloları kullanmamaya özen gösterir.
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<PrelCs>(entity => { entity.HasNoKey(); });
            modelBuilder.Entity<PrelPIs>(entity => { entity.HasNoKey(); });
        }
        */
    }
}
