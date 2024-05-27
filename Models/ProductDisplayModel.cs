using System.Data.SqlClient;

namespace KhumaloCraftsWeb.Models
{
    public class ProductDisplayModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string CategoryPlace { get; set; }
        public bool Availability { get; set; }

        public ProductDisplayModel() { }

        
        public ProductDisplayModel(int id, string name, decimal price, string category, bool availability)
        {
            ID = id;
            Name = name;
            Price = price;
            CategoryPlace = category;
            Availability = availability;
        }

        public static List<ProductDisplayModel> SelectProducts()
        {
            List<ProductDisplayModel> products = new List<ProductDisplayModel>();

            string con_string = "Server=tcp:khumalocraftsweb.database.windows.net,1433;Initial Catalog=khumaloCraftsWeb;Persist Security Info=False;User ID=st10260322;Password=Birdzone33;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30";
            using (SqlConnection con = new SqlConnection(con_string))
            {
                string sql = "SELECT ID,Name,Price,CategoryPlace,Availability FROM ProductTable";
                SqlCommand cmd = new SqlCommand(sql, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ProductDisplayModel product = new ProductDisplayModel();
                    product.ID = Convert.ToInt32(reader["ID"]);
                    product.Name = Convert.ToString(reader["productName"]);
                    product.Price = Convert.ToDecimal(reader["productPrice"]);
                    product.CategoryPlace = Convert.ToString(reader["Category"]);
                    product.Availability = Convert.ToBoolean(reader["availability"]);
                    products.Add(product);
                }
                reader.Close();
            }
            return products;
        }
    }

}
