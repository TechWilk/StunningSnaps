using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;

namespace CO5027
{
    public partial class checkout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string customerId = User.Identity.GetUserId();
            DatabaseCO5027Entities db = new DatabaseCO5027Entities();
            var basket = db.Baskets.Where(b => b.CustomerId == customerId).ToList();

            // TODO: convert list to format useful for front-end table

            rptBasket.DataSource = basket;
            rptBasket.DataBind();
        }
    }
}