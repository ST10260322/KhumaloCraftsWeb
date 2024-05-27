using System.Data.SqlClient;

namespace KhumaloCraftsWeb.Models
{
    public class LoginModel
    {
        public static string con_string = "Server=tcp:khumalocraftsweb.database.windows.net,1433;Initial Catalog=khumaloCraftsWeb;Persist Security Info=False;User ID=st10260322;Password=Birdzone33;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30";


        public int SelectUser(string email, string name)
        {
            int userId = -1; // Default value if user is not found
            using (SqlConnection con = new SqlConnection(con_string))
            {
                string sql = "SELECT userID FROM userTable WHERE userEmail = @userEmail AND userName = @userName";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@userEmail", email);
                cmd.Parameters.AddWithValue("@userName", name);
                try
                {
                    con.Open();
                    object result = cmd.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        userId = Convert.ToInt32(result);
                    }
                }
                catch (Exception ex)
                {
                    
                    throw ex;
                }
            }
            return userId;
        }

    }

}
