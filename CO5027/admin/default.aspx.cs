using CO5027.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CO5027.admin
{
    public partial class _default : System.Web.UI.Page
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
            var products = db.Products.ToList();
            rptPhotos.DataSource = products;
            rptPhotos.DataBind();
        }

        protected void btnReprocessImages_Click(object sender, EventArgs e)
        {
            DatabaseCO5027Entities db = new DatabaseCO5027Entities();
            var imageProcessor = new ImageProcessing();
            var products = db.Products.ToList();
            db.Dispose();
            foreach (Product p in products)
            {
                int id = p.Id;
                string path = Server.MapPath("~/files/images/original/" + id.ToString() + p.Extension);
                var img = System.Drawing.Image.FromFile(path);
                imageProcessor.SaveWatermarkedImages(img, id);
            }
        }

        protected void rptPhotos_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            string idString = e.CommandArgument.ToString();

            int id = int.Parse(idString);

            DatabaseCO5027Entities db = new DatabaseCO5027Entities();
            var product = db.Products.Single(p => p.Id == id);
            if (product.Archived)
            {
                product.Archived = false;
            }
            else
            {
                product.Archived = true;
            }

            db.SaveChangesAsync();
            BindRepeater();
        }
    }
}