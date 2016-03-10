using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CO5027.user
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string action = Request.QueryString["action"];
            switch (action)
            {
                case "register":
                    pnlLogin.Visible = false;
                    pnlRegister.Visible = true;
                    goto default;
                case "logout":
                    Logout();
                    break;
                default:
                    //TODO redirect user if already logged in
                    break;
            }

            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                pnlLogin.Visible = false;
                pnlEdit.Visible = true;
            }

        }
        protected void btnLogin_Click(object sender, EventArgs e)
        {

            var userStore = new UserStore<IdentityUser>();
            var userManager = new UserManager<IdentityUser>(userStore);
            var user = userManager.Find(txtLoginUsername.Text, txtLoginPassword.Text);

            if (user != null)
            {
                Login(user, userManager);
            }
            else
            {
                litError.Text = "Incorrect username or password";
            }
        }
        protected void btnRegisterShow_Click(object sender, EventArgs e)
        {
            pnlLogin.Visible = false;
            pnlRegister.Visible = true;
        }
        protected void btnRegister_Click(object sender, EventArgs e)
        {
            var userStore = new UserStore<IdentityUser>();
            var manager = new UserManager<IdentityUser>(userStore);
            var user = new IdentityUser() { UserName = txtRegisterUsername.Text };
            IdentityResult result = manager.Create(user, txtRegisterPassword.Text);
            if (result.Succeeded)
            {
                Login(user, manager);
            }
            else
            {
                litError.Text = result.Errors.FirstOrDefault();
            }
        }
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            // TODO: change password
        }
        protected void Login(IdentityUser user, UserManager<IdentityUser> manager)
        {
            var authenticationManager = HttpContext.Current.GetOwinContext().Authentication;
            var userIdentity = manager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);

            authenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = false }, userIdentity);

            // add items to basket if necessary
            if (Session["basketProductId"] != null && Session["basketQty"] != null) // TODO: ask Andrew
            {
                DatabaseCO5027Entities db = new DatabaseCO5027Entities();
                var basketEntry = new Basket();
                basketEntry.CustomerId = User.Identity.GetUserId();
                basketEntry.ProductId = (int)Session["basketProductId"];
                basketEntry.Qty = (int)Session["basketQty"];
                db.Baskets.Add(basketEntry);
                db.SaveChangesAsync();

                Session.Remove("basketQty");
                Session.Remove("basketProductId");
            }

            if (Request.QueryString["ReturnUrl"] == null)
            {
                Response.Redirect("~/user/default.aspx");
            }
        }
        protected void Logout()
        {
            var authenticationManager = HttpContext.Current.GetOwinContext().Authentication;
            authenticationManager.SignOut();
            Response.Redirect("~/");
        }
    }
}