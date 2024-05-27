

using System.Data.SqlClient;

namespace KhumaloCraftsWeb.Models
{
    public class userTable
    {


        public static string con_string = "Server=tcp:khumalocraftsweb.database.windows.net,1433;Initial Catalog=khumaloCraftsWeb;Persist Security Info=False;User ID=st10260322;Password=Birdzone33;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30";
        public static SqlConnection con = new SqlConnection(con_string);



        public string Name { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }





        public int insert_User(userTable m)
        {

            try
            {
                string sql = "INSERT INTO userTable (userName,userSurname,userEmail) VALUES (@Name, @Surname, @Email)";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@Name", m.Name);
                cmd.Parameters.AddWithValue("@Surname", m.Surname);
                cmd.Parameters.AddWithValue("@Email", m.Email);
                con.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                con.Close();
                return rowsAffected;
            }
            catch (Exception ex)
            {
                
                throw ex;
            }


        }

    }

}
