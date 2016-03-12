using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CO5027.user
{
    public partial class _default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string customerId = User.Identity.GetUserId();
            DatabaseCO5027Entities db = new DatabaseCO5027Entities();
            var orders = db.Orders.Where(o => o.CustomerId == customerId && o.Cancelled == false).OrderByDescending(o => o.DateStamp).ToList();

            rptOrders.DataSource = orders;
            rptOrders.DataBind();
        }
    }
}