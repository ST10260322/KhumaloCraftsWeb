using KhumaloCraftsWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;




namespace KhumaloCraftsWeb.Controllers
{
    public class TransactionController : Controller
    {
        [HttpPost]
        public ActionResult PlaceOrder(int userID, int productID)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ProductTable.con_string))
                {
                    string sql = "INSERT INTO transactionTable (userID, productID) VALUES (@UserID, @ProductID)";
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@UserID", userID);
                        cmd.Parameters.AddWithValue("@ProductID", productID);
                        con.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        con.Close();

                        if (rowsAffected > 0)
                        {
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            return View("OrderFailed");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                
                Debug.WriteLine(ex.Message);
                return View("Error");
            }
        }


        

        [HttpGet]
        public ActionResult EnterUserID()
        {
            return View();
        }

        
         
        [HttpPost]
        public ActionResult GetUserTransactions(int userID)
        {
            Debug.WriteLine($"GetUserTransactions called with userID: {userID}");

            if (!UserExists(userID))
            {
                Debug.WriteLine($"User with userID: {userID} not found");
                return View("UserNotFound");
            }

            List<Transaction> transactions = GetTransactionsForUser(userID);
            if (transactions.Count == 0)
            {
                Debug.WriteLine($"No transactions found for userID: {userID}");
            }

            return View("UserTransactions", transactions);
        }
        
        
        








        
        public bool UserExists(int userID)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ProductTable.con_string))
                {
                    string sql = "SELECT COUNT(*) FROM transactionTable WHERE UserID = @UserID";
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@UserID", userID);
                        con.Open();
                        int count = (int)cmd.ExecuteScalar();
                        Debug.WriteLine($"UserExists result for userID {userID}: {count > 0}");
                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error checking user existence: {ex.Message}");
                return false;
            }
        }

        


        public List<Transaction> GetTransactionsForUser(int userID)
        {
            List<Transaction> transactions = new List<Transaction>();

            try
            {
                using (SqlConnection con = new SqlConnection(ProductTable.con_string))
                {
                    string sql = @"
                SELECT t.TransactionID, t.UserID, t.ProductID, t.Date, p.Name as ProductName
                FROM transactionTable t
                JOIN ProductTable p ON t.ProductID = p.ID
                WHERE t.UserID = @UserID";

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@UserID", userID);
                        con.Open();

                        using (SqlDataReader rdr = cmd.ExecuteReader())
                        {
                            while (rdr.Read())
                            {
                                Transaction transaction = new Transaction
                                {
                                    TransactionID = Convert.ToInt32(rdr["TransactionID"]),
                                    UserID = Convert.ToInt32(rdr["UserID"]),
                                    ProductID = Convert.ToInt32(rdr["ProductID"]),
                                    ProductName = rdr["ProductName"].ToString(),
                                    
                                };
                                transactions.Add(transaction);
                                Debug.WriteLine($"Transaction added: {transaction.TransactionID}, {transaction.UserID}, {transaction.ProductID}, {transaction.ProductName}, {transaction.Date}");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception occurred: {ex.Message}");
            }

            return transactions;
        }

        







    }
}

