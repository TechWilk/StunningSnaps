using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CO5027.admin
{
    public partial class orders : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindRepeater();
            }
        }

        private void BindRepeater()
        {
            DatabaseCO5027Entities db = new DatabaseCO5027Entities();
            var orders = db.Orders.OrderByDescending(o => o.DateStamp).ToList();

            rptOrders.DataSource = orders;
            rptOrders.DataBind();
        }

        protected void rptOrders_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            string idString = e.CommandArgument.ToString();

            int id = int.Parse(idString);
            DatabaseCO5027Entities db = new DatabaseCO5027Entities();

            var order = db.Orders.Single(o => o.Id == id);
            order.Cancelled = true;
            foreach (var item in order.OrderedProducts)
            {
                item.DownloadsAllowed = 0;
            }

            db.SaveChanges();
            BindRepeater();
        }
        protected void rptOrderedProducts_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            string idString = e.CommandArgument.ToString();

            int id = int.Parse(idString);
            DatabaseCO5027Entities db = new DatabaseCO5027Entities();

            var orderedProduct = db.OrderedProducts.Single(op => op.Id == id);
            orderedProduct.DownloadsAllowed += 1;

            db.SaveChanges();
            BindRepeater();
        }
    }
}