using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;
using CO5027.Models;

namespace CO5027
{
    public partial class checkout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            switch (Request.QueryString["action"])
            {
                case "confirm":
                    pnlBasket.Visible = false;
                    pnlCheckout.Visible = true;

                    SetupCheckout();
                    break;

                default:
                    SetupBasket();
                    break;
            }
        }

        private void SetupBasket()
        {
            string customerId = User.Identity.GetUserId();
            DatabaseCO5027Entities db = new DatabaseCO5027Entities();
            var basket = db.Baskets.Where(b => b.CustomerId == customerId).ToList();

            decimal totalCost = 0;

            var basketToDisplay = new List<BasketDisplay>();

            foreach (var item in basket)
            {
                BasketDisplay basketItem = new BasketDisplay();

                basketItem.CustomerId = item.CustomerId;
                basketItem.Id = item.Id;
                basketItem.ProductId = item.ProductId;
                basketItem.Qty = item.Qty;
                basketItem.ProductName = item.Product.Name;
                basketItem.ProductDescription = item.Product.Description;
                basketItem.Price = (decimal)item.Product.Price;

                var image = db.Images.Single(p => p.ProductId == item.ProductId && p.SizeId == 3);

                basketItem.ImageHeight = image.Height;
                basketItem.ImageWidth = image.Width;

                basketToDisplay.Add(basketItem);
                totalCost += (decimal)item.Product.Price;
            }

            // TODO: convert list to format useful for front-end table

            rptBasket.DataSource = basketToDisplay;
            rptBasket.DataBind();
        }
        private void SetupCheckout()
        {
            // TODO: display summary of products and order details
        }

        protected void btnContinue_Click(object sender, EventArgs e)
        {
            string customerId = User.Identity.GetUserId();
            DatabaseCO5027Entities db = new DatabaseCO5027Entities();
            var basket = db.Baskets.Where(b => b.CustomerId == customerId);

            var order = new Order();

            order.DateStamp = DateTime.Now;
            order.CustomerId = customerId;
            order.TotalCost = 0; // TODO: calculate total cost

            db.Orders.Add(order);
            db.SaveChanges();

            var orderId = order.Id;

            decimal totalCost = 0;

            foreach (var item in basket)
            {
                OrderedProduct orderedProduct = new OrderedProduct();
                orderedProduct.OrderId = orderId;
                orderedProduct.ProductId = item.ProductId;
                orderedProduct.DownloadsAllowed = 0; // set once payment complete
                db.OrderedProducts.Add(orderedProduct);
                db.Baskets.Remove(item);
                totalCost += (decimal)item.Product.Price;
            }

            order.TotalCost = totalCost;
            db.SaveChanges();

            // TODO: add PayPal stuff
        }
        protected void btnConfirmOrder_Click(object sender, EventArgs e)
        {
            // fetch data from form

            string customerName = "bob"; //TODO: fetch fro db
            string customerEmailAddress = "customer email address";

            // format email for admin

            string emailToAdmin = "Email Sumbitted:" + Environment.NewLine;
            emailToAdmin += "FROM: " + customerName + Environment.NewLine;
            emailToAdmin += "REPLY TO: " + customerEmailAddress + Environment.NewLine;
            emailToAdmin += Environment.NewLine;
            emailToAdmin += "MESSAGE:" + Environment.NewLine;
            emailToAdmin += "Thank you for your order" + Environment.NewLine; // add proper message
            emailToAdmin += Environment.NewLine;
            emailToAdmin += "Message sent though StunningSnaps website";

            string subjectToAdmin = "New message from: " + customerName;

            // Send email to admin
            Email.sendEmail("stunningsnaps@wilk.tech", customerEmailAddress, subjectToAdmin, emailToAdmin);
        }
    }
}