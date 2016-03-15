using CO5027.Models;
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
                    break;
                case "logout":
                    Logout();
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
                DatabaseCO5027Entities db = new DatabaseCO5027Entities();
                var userDetails = new UserDetail();
                userDetails.UserId = user.Id;
                userDetails.FirstName = txtRegisterFirstName.Text;
                userDetails.Surname = txtRegisterSurname.Text;
                userDetails.Email = txtRegisterEmail.Text;
                db.UserDetails.Add(userDetails);
                db.SaveChanges();
                db.Dispose();

                SendWelcomeEmailToCustomer(user);
                SendWelcomeEmailToAdmin(user);
                Login(user, manager);
            }
            else
            {
                litError.Text = result.Errors.FirstOrDefault();
            }
        }
        protected void SendWelcomeEmailToCustomer(IdentityUser user)
        {
            DatabaseCO5027Entities db = new DatabaseCO5027Entities();
            var userDetails = db.UserDetails.Single(u => u.UserId == user.Id);
            var email = userDetails.Email;
            var firstName = userDetails.FirstName;
            var lastName = userDetails.Surname;
            string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority;

            string emailBody = "";
            emailBody += "Hello " + firstName + "," + Environment.NewLine;
            emailBody += Environment.NewLine;
            emailBody += "Thank you for signing up for an account with StunningSnaps!" + Environment.NewLine;
            emailBody += Environment.NewLine;
            emailBody += "Your username is: " + user.UserName + Environment.NewLine;
            emailBody += "Your password was set during account creation. If you need to reset your password, please follow the reset password instructions on the Login page." + Environment.NewLine;
            emailBody += Environment.NewLine;
            emailBody += "Message sent though StunningSnaps website" + Environment.NewLine;
            emailBody += baseUrl + ResolveUrl("~/");

            Email.sendEmail(email, "stunningsnaps@wilk.tech", "Welcome to StunningSnaps!", emailBody);

        }
        protected void SendWelcomeEmailToAdmin(IdentityUser user)
        {
            DatabaseCO5027Entities db = new DatabaseCO5027Entities();
            var userDetails = db.UserDetails.Single(u => u.UserId == user.Id);
            var email = userDetails.Email;
            var firstName = userDetails.FirstName;
            var lastName = userDetails.Surname;

            string emailBody = "";
            emailBody += "New user created: " + firstName + " " + lastName + Environment.NewLine;
            emailBody += "(Username: " + user.UserName + ", Email:" + email + ")" + Environment.NewLine;

            Email.sendEmail("stunningsnaps@wilk.tech", "stunningsnaps@wilk.tech", "New Account Created: " + user.UserName, emailBody);
        }
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            var userStore = new UserStore<IdentityUser>();
            var manager = new UserManager<IdentityUser>(userStore);
            var user = manager.FindById(User.Identity.GetUserId());
            try
            {
                manager.ChangePassword(User.Identity.GetUserId(), txtEditOldPassword.Text, txtEditPassword.Text);
                litEditStatus.Text = "Password changed successfully.";
                pnlEditInput.Visible = false;
            }
            catch
            {
                litEditStatus.Text = "Existing password is incorrect, please try again.";
            }
        }
        protected void Login(IdentityUser user, UserManager<IdentityUser> manager)
        {
            var authenticationManager = HttpContext.Current.GetOwinContext().Authentication;
            var userIdentity = manager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);

            authenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = false }, userIdentity);

            // add items to basket if necessary
            if (Session["basketProductId"] != null)
            {
                DatabaseCO5027Entities db = new DatabaseCO5027Entities();
                var basketEntry = new Basket();
                basketEntry.CustomerId = user.Id;
                basketEntry.ProductId = (int)Session["basketProductId"];
                db.Baskets.Add(basketEntry);
                db.SaveChanges();

                Session.Remove("basketProductId");

                Response.Redirect("~/checkout.aspx");
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