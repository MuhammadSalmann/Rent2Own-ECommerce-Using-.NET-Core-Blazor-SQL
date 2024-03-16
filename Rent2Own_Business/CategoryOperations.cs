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
    public class CategoryOperations
    {
        public static void Create(Category obj)
        {
            SqlConnection con = DBHelper.GetConnection();
            con.Open();
            SqlCommand cmd = new SqlCommand("CP_SaveCategory", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Name", obj.Name);
            obj.CreatedDate = DateTime.Now;
            cmd.Parameters.AddWithValue("@CreatedDate", obj.CreatedDate);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public static void Delete(int id)
        {
            SqlConnection con = DBHelper.GetConnection();
            con.Open();
            SqlCommand cmd = new SqlCommand("CP_DeleteCategory", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", id);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public static Category Get(int id)
        {
            SqlConnection con = DBHelper.GetConnection();
            con.Open();
            SqlCommand cmd = new SqlCommand("CP_GetCategory", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", id);

            SqlDataReader reader = cmd.ExecuteReader();

            Category category = null;

            if (reader.Read())
            {
                category = new Category();
                category.Id = Convert.ToInt32(reader["Id"]);
                category.Name = reader["Name"].ToString();
                category.CreatedDate = Convert.ToDateTime(reader["CreatedDate"]);
            }

            con.Close();
            return category;
        }

        public static List<Category> GetAll()
        {
            SqlConnection con = DBHelper.GetConnection();
            con.Open();
            SqlCommand cmd = new SqlCommand("CP_GetCategories", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader reader = cmd.ExecuteReader();

            List<Category> listcategory = new List<Category>();
            while (reader.Read())
            {
                Category c = new Category();
                c.Id = Convert.ToInt32(reader["Id"]);
                c.Name = reader["Name"].ToString();
                c.CreatedDate = Convert.ToDateTime(reader["CreatedDate"]);
                listcategory.Add(c);
            }
            con.Close();
            return listcategory;
        }

        public static void Update(Category obj)  //int? id
        {
            //A.AloID = Convert.ToInt32(id);
            SqlConnection con = DBHelper.GetConnection();
            con.Open();
            SqlCommand cmd = new SqlCommand("CP_UpdateCategory", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", obj.Id);
            cmd.Parameters.AddWithValue("@Name", obj.Name);
            obj.CreatedDate = DateTime.Now;
            cmd.Parameters.AddWithValue("@CreatedDate", obj.CreatedDate);
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
}
