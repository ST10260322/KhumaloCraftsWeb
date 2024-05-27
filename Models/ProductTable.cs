using System.Data.SqlClient;

namespace KhumaloCraftsWeb.Models
{
    public class ProductTable
    {
        public static string con_string = "Server=tcp:khumalocraftsweb.database.windows.net,1433;Initial Catalog=khumaloCraftsWeb;Persist Security Info=False;User ID=st10260322;Password=Birdzone33;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30";

        public int productID { get; set; }
        public string productName { get; set; }
        public string productPrice { get; set; }
        public string Category { get; set; }
        public string availability { get; set; }

        public int insert_Product(ProductTable m, SqlConnection con, SqlTransaction transaction)
        {
            try
            {
                string sql = "INSERT INTO ProductTable (Name, Price, Category, Availability) VALUES (@productName, @productPrice, @Category, @availability)";
                using (SqlCommand cmd = new SqlCommand(sql, con, transaction))
                {
                    cmd.Parameters.AddWithValue("@productName", m.productName);
                    cmd.Parameters.AddWithValue("@productPrice", m.productPrice);
                    cmd.Parameters.AddWithValue("@Category", m.Category);
                    cmd.Parameters.AddWithValue("@availability", m.availability);
                    return cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<ProductTable> GetAllProducts()
        {
            List<ProductTable> products = new List<ProductTable>();

            using (SqlConnection con = new SqlConnection(con_string))
            {
                string sql = "SELECT * FROM ProductTable";
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    con.Open();
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            ProductTable product = new ProductTable();
                            product.productID = Convert.ToInt32(rdr["ID"]);
                            product.productName = rdr["Name"].ToString();
                            product.productPrice = rdr["Price"].ToString();
                            product.Category = rdr["Category"].ToString();
                            product.availability = rdr["Availability"].ToString();
                            products.Add(product);
                        }
                    }
                }
            }

            return products;
        }
    }
}
