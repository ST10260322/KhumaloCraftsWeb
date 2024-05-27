using Microsoft.AspNetCore.Mvc;
using KhumaloCraftsWeb.Models;
using System.Data.SqlClient;

namespace KhumaloCraftsWeb.Controllers
{
    public class ProductController : Controller
    {
        public ProductTable prdTable = new ProductTable();

        [HttpPost]
        public ActionResult Product(ProductTable prd)
        {
            using (SqlConnection con = new SqlConnection(ProductTable.con_string))
            {
                con.Open();
                using (SqlTransaction transaction = con.BeginTransaction())
                {
                    try
                    {
                        var result = prdTable.insert_Product(prd, con, transaction);
                        transaction.Commit();
                        return RedirectToAction("Index", "Home");
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        return View("Error");
                    }
                }
            }
        }

        [HttpGet]
        public ActionResult Product()
        {
            return View(prdTable);
        }
    }
}
