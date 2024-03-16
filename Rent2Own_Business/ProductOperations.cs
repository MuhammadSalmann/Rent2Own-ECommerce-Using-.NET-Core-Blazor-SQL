using Rent2Own_DataAccess;
using Rent2Own_Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rent2Own_Business
{
    public class ProductOperations
    {
        public static void Create(Product obj)
        {
            SqlConnection con = DBHelper.GetConnection();
            con.Open();
            SqlCommand cmd = new SqlCommand("CP_SaveProduct", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Name", obj.Name);
            cmd.Parameters.AddWithValue("@Description", obj.Description);
            cmd.Parameters.AddWithValue("@ShopFavorites", obj.ShopFavorites);
            cmd.Parameters.AddWithValue("@CustomerFavorites", obj.CustomerFavorites);
            cmd.Parameters.AddWithValue("@Model", obj.Model);
            cmd.Parameters.AddWithValue("@Company", obj.Company);
            cmd.Parameters.AddWithValue("@ImageUrl", obj.ImageUrl);
            cmd.Parameters.AddWithValue("@CategoryId", obj.CategoryId);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public static void Delete(int id)
        {
            SqlConnection con = DBHelper.GetConnection();
            con.Open();
            SqlCommand cmd = new SqlCommand("CP_DeleteProduct", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", id);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public static Product Get(int id)
        {
            SqlConnection con = DBHelper.GetConnection();
            con.Open();
            SqlCommand cmd = new SqlCommand("CP_GetProduct", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", id);
            SqlDataReader reader = cmd.ExecuteReader();

            Product product = null;

            if (reader.Read())
            {
                product = new Product();
                product.Id = Convert.ToInt32(reader["Id"]);
                product.Name = reader["Name"].ToString();
                product.Description = reader["Description"].ToString();
                product.ShopFavorites = Convert.ToBoolean(reader["ShopFavorites"]);
                product.CustomerFavorites = Convert.ToBoolean(reader["CustomerFavorites"]);
                product.Model = Convert.ToInt32(reader["Model"]);
                product.Company = reader["Company"].ToString();
                product.ImageUrl = reader["ImageUrl"].ToString();
                product.CategoryId = Convert.ToInt32(reader["CategoryId"]);
                product.Category = CategoryOperations.Get(product.CategoryId);
                product.ProductPrices = ProductPriceOperations.GetAll(product.Id);
            }
            con.Close();
            return product;
        }

        public static List<Product> GetAll()
        {
            SqlConnection con = DBHelper.GetConnection();
            con.Open();
            SqlCommand cmd = new SqlCommand("CP_GetProducts", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader reader = cmd.ExecuteReader();

            List<Product> listproduct = new List<Product>();
            while (reader.Read())
            {
                Product p = new Product();
                p.Id = Convert.ToInt32(reader["Id"]);
                p.Name = reader["Name"].ToString();
                p.Description = reader["Description"].ToString();
                p.ShopFavorites = Convert.ToBoolean(reader["ShopFavorites"]);
                p.CustomerFavorites = Convert.ToBoolean(reader["CustomerFavorites"]);
                p.Model = Convert.ToInt32(reader["Model"]);
                p.Company = reader["Company"].ToString();
                p.ImageUrl = reader["ImageUrl"].ToString();
                p.CategoryId = Convert.ToInt32(reader["CategoryId"]);
                p.Category = CategoryOperations.Get(p.CategoryId);
                p.ProductPrices = ProductPriceOperations.GetAll(p.Id);

                listproduct.Add(p);
            }
            con.Close();
            return listproduct;
        }

        public static void Update(Product obj)
        {
            SqlConnection con = DBHelper.GetConnection();
            con.Open();
            SqlCommand cmd = new SqlCommand("CP_UpdateProduct", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", obj.Id);
            cmd.Parameters.AddWithValue("@Name", obj.Name);
            cmd.Parameters.AddWithValue("@Description", obj.Description);
            cmd.Parameters.AddWithValue("@ShopFavorites", obj.ShopFavorites);
            cmd.Parameters.AddWithValue("@CustomerFavorites", obj.CustomerFavorites);
            cmd.Parameters.AddWithValue("@Model", obj.Model);
            cmd.Parameters.AddWithValue("@Company", obj.Company);
            cmd.Parameters.AddWithValue("@ImageUrl", obj.ImageUrl);
            cmd.Parameters.AddWithValue("@CategoryId", obj.CategoryId);
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
}
