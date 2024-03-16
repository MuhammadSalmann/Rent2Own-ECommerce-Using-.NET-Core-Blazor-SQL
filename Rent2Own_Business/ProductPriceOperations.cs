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
    public class ProductPriceOperations
    {
        public static void Create(ProductPrice obj)
        {
            SqlConnection con = DBHelper.GetConnection();
            con.Open();
            SqlCommand cmd = new SqlCommand("CP_SaveProductPrice", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ProductId", obj.ProductId);
            cmd.Parameters.AddWithValue("@Duration", obj.Duration);
            cmd.Parameters.AddWithValue("@Price", obj.Price);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public static void Delete(int id)
        {
            SqlConnection con = DBHelper.GetConnection();
            con.Open();
            SqlCommand cmd = new SqlCommand("CP_DeleteProductPrice", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", id);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public static ProductPrice Get(int id)
        {
            SqlConnection con = DBHelper.GetConnection();
            con.Open();
            SqlCommand cmd = new SqlCommand("CP_GetProductPrice", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", id);

            SqlDataReader reader = cmd.ExecuteReader();

            ProductPrice price = null;

            if (reader.Read())
            {
                price = new ProductPrice();
                price.Id = Convert.ToInt32(reader["Id"]);
                price.ProductId = Convert.ToInt32(reader["ProductId"]);
                price.Duration = reader["Duration"].ToString();
                price.Price = Convert.ToDouble(reader["Price"]);
                //price.Product = ProductOperations.Get(price.ProductId);
            }
            con.Close();
            return price;
        }

        public static List<ProductPrice> GetAll(int? id = null)
        {
            SqlConnection con = DBHelper.GetConnection();
            con.Open();
            SqlCommand cmd = new SqlCommand("CP_GetProductPrices", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader reader = cmd.ExecuteReader();

            List<ProductPrice> listprices = new List<ProductPrice>();
            while (reader.Read())
            {
                ProductPrice price = new ProductPrice();
                price.Id = Convert.ToInt32(reader["Id"]);
                price.ProductId = Convert.ToInt32(reader["ProductId"]);
                price.Duration = reader["Duration"].ToString();
                price.Price = Convert.ToDouble(reader["Price"]);
                //price.Product = ProductOperations.Get(price.ProductId);

                if (id == null || price.ProductId == id)
                {
                    listprices.Add(price);
                }
            }
            con.Close();
            return listprices;
        }

        public static void Update(ProductPrice obj)
        {
            SqlConnection con = DBHelper.GetConnection();
            con.Open();
            SqlCommand cmd = new SqlCommand("CP_UpdateProductPrice", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", obj.Id);
            cmd.Parameters.AddWithValue("@ProductId", obj.ProductId);
            cmd.Parameters.AddWithValue("@Duration", obj.Duration);
            cmd.Parameters.AddWithValue("@Price", obj.Price);
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
}
