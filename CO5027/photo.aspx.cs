using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CO5027
{
    public partial class photo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string idString = Request.QueryString["id"];

            // TODO: parse string to int, then fetch record from db.

            int id = 1;

            imgPhoto.ImageUrl = "~/files/images/watermarked/" + id.ToString() + "-lg.jpg";
            imgPhoto.AlternateText = "x";
            imgPhoto.Width = 800;  // TODO: use sizes from db
            imgPhoto.Height = 800;
        }
    }
}