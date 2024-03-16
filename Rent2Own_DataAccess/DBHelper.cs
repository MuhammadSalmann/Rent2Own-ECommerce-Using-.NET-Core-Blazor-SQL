using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Rent2Own_Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rent2Own_DataAccess
{
    public class DBHelper : IdentityDbContext
    {
        public DBHelper(DbContextOptions<DBHelper> options) : base(options)
        {

        }
        public static SqlConnection GetConnection()
        {
            SqlConnection conn = new SqlConnection("Data Source=SALMAN\\SQLEXPRESS;Initial Catalog=Rent2Own;Integrated Security=True");
            return conn;
        }
        // Code First Set
        //public DbSet<Category> Categories { get; set; }
        //public DbSet<Product> Products { get; set; }
        //public DbSet<ProductPrice> ProductPrices { get; set; }
        public DbSet<OrderHeader> OrderHeaders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        
    }
}
