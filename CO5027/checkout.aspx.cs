using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;
using CO5027.Models;
using PayPal.Api;

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

                case "cancel":
                    pnlBasket.Visible = false;
                    pnlCancel.Visible = true;
                    DatabaseCO5027Entities db = new DatabaseCO5027Entities();
                    Order order = db.Orders.Single(o => o.Id == 1); //todo fetch order id from PayPal redirect
                    db.Orders.Remove(order);
                    litCancelMessage.Text = "<p>Invalid payment. You have not been charged. Please reorder the required products.</p>";
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

            if (basket.Count < 1)
            {
                pnlBasketItems.Visible = false;
                litBasketMessage.Text = "You have no items in your basket.";
                return;
            }

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
            order.TotalCost = 0; // calculated in foreach loop

            db.Orders.Add(order);
            db.SaveChanges();

            var orderId = order.Id;

            decimal totalCost = 0;

            var products = new List<OrderedProduct>();

            foreach (var item in basket)
            {
                OrderedProduct orderedProduct = new OrderedProduct();
                orderedProduct.OrderId = orderId;
                orderedProduct.ProductId = item.ProductId;
                orderedProduct.DownloadsAllowed = 0; // set once payment complete
                db.OrderedProducts.Add(orderedProduct);
                products.Add(orderedProduct);
                totalCost += (decimal)item.Product.Price;
                db.Baskets.Remove(item);
            }

            order.TotalCost = totalCost;
            db.SaveChanges();

            PayPalPayment(db, order, products);
        }
        protected void PayPalPayment(DatabaseCO5027Entities db, Order order, List<OrderedProduct> products)
        {
            string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority;

            var config = ConfigManager.Instance.GetProperties();
            var accessToken = new OAuthTokenCredential(config).GetAccessToken();
            var apiContext = new APIContext(accessToken);

            List<Item> items = new List<Item>();

            foreach (OrderedProduct product in products)
            {
                Item item = new Item
                {
                    name = product.Product.Name,
                    currency = "GBP",
                    price = ((decimal)product.Product.Price).ToString("0.00"),
                    quantity = "1",
                    sku = product.ProductId.ToString()
                };
                items.Add(item);
            };
            ItemList productsItemList = new ItemList
            {
                items = items
            };

            var payment = Payment.Create(apiContext, new Payment
            {
                intent = "sale",
                payer = new Payer
                {
                    payment_method = "paypal"
                },
                transactions = new List<Transaction>
                {
                    new Transaction
                    {
                        description = "Order from StunningSnaps",
                        invoice_number = order.Id.ToString(),
                        amount = new Amount
                        {
                            currency = "GBP",
                            total = order.TotalCost.ToString("0.00"),
                            details = new Details
                            {
                                tax = "0",
                                shipping = "0",
                                subtotal = order.TotalCost.ToString("0.00")
                            }
                        },
                        item_list = productsItemList
                    }
                },
                redirect_urls = new RedirectUrls
                {
                    return_url = baseUrl + ResolveUrl("~/checkout.aspx?action=confirm"),
                    cancel_url = baseUrl + ResolveUrl("~/checkout.aspx?action=cancel")
                }
            });

            order.PaymentId = payment.id;
            db.SaveChanges();

            foreach (var link in payment.links)
            {
                if (link.rel.ToLower().Trim().Equals("approval_url"))
                {
                    Response.Redirect(link.href);
                }
            }
        }
        protected void PayPalConfirmation(string paymentId, string payerId)
        {
            var config = ConfigManager.Instance.GetProperties();
            var accessToken = new OAuthTokenCredential(config).GetAccessToken();
            var apiContext = new APIContext(accessToken);

            var payment = new Payment() { id = paymentId };
            var paymentExecution = new PaymentExecution() { payer_id = payerId };

            payment.Execute(apiContext, paymentExecution);
        }
        protected void btnConfirmOrder_Click(object sender, EventArgs e)
        {
            DatabaseCO5027Entities db = new DatabaseCO5027Entities();

            string paymentId = Request.QueryString["paymentId"];
            string payerId = Request.QueryString["payerId"];

            Order order = db.Orders.Single(o => o.PaymentId == paymentId);

            if (String.IsNullOrEmpty(paymentId) || String.IsNullOrEmpty(payerId))
            {
                litConfirmMessage.Text = "<p>Payment aborted. You have not been charged. Please reorder the required products.</p>";
                btnConfirmOrder.Visible = false;
                db.Orders.Remove(order);
                return;
            }

            PayPalConfirmation(paymentId, payerId);

            order.PaymentId = paymentId;
            order.PayerId = payerId;
            order.AmountPaid = order.TotalCost;

            var products = db.OrderedProducts.Where(op => op.OrderId == order.Id);

            foreach (var product in products)
            {
                product.DownloadsAllowed += 5;
            }
            db.SaveChanges();

            string customerId = User.Identity.GetUserId();
            var customer = db.UserDetails.Single(c => c.UserId == customerId);

            string customerName = customer.FirstName + " " + customer.Surname;
            string customerEmailAddress = customer.Email;

            SendEmailToAdmin(customer, order, products.ToList());
            SendEmailToCustomer(customer, order, products.ToList());

            Response.Redirect("~/");
        }
        protected void SendEmailToAdmin(UserDetail customer, Order order, List<OrderedProduct> products)
        {
            string customerName = customer.FirstName + " " + customer.Surname;

            // format email for admin

            string emailBody = "Email Sumbitted:" + Environment.NewLine;
            emailBody += "FROM: " + customerName + Environment.NewLine;
            emailBody += "REPLY TO: " + customer.Email + Environment.NewLine;
            emailBody += Environment.NewLine;
            emailBody += "MESSAGE:" + Environment.NewLine;
            emailBody += "Thank you for your order" + Environment.NewLine; // todo: add proper message
            emailBody += Environment.NewLine;
            emailBody += "Message sent though StunningSnaps website";

            string subject = "New order: " + order.Id + " from: " + customerName;

            // Send email to admin
            Email.sendEmail("stunningsnaps@wilk.tech", customer.Email, subject, emailBody);
        }
        protected void SendEmailToCustomer(UserDetail customer, Order order, List<OrderedProduct> orderedProducts)
        {
            string customerName = customer.FirstName + " " + customer.Surname;
            string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority;

            // format email for admin

            string emailBody = customer.FirstName + "," + Environment.NewLine;
            emailBody += Environment.NewLine;
            emailBody += "Thank you for your recient order at StunningSnaps." + Environment.NewLine;
            emailBody += "For your records, please retain a copy of this email. " + Environment.NewLine;
            emailBody += Environment.NewLine;
            emailBody += "You have purchaced:" + Environment.NewLine;
            emailBody += Environment.NewLine;
            emailBody += "----------" + Environment.NewLine;

            foreach (OrderedProduct orderedProduct in orderedProducts)
            {
                emailBody += orderedProduct.Product.Name + " (" + orderedProduct.Product.InitialHeight + " x " + orderedProduct.Product.InitialWidth + ")" + Environment.NewLine;
                emailBody += "£" + ((decimal)orderedProduct.Product.Price).ToString("0.00") + Environment.NewLine;
                emailBody += "Download: " + baseUrl + ResolveUrl("~/user/download.aspx?id=" + orderedProduct.ProductId) + Environment.NewLine;
                emailBody += "----------" + Environment.NewLine;
            }

            emailBody += Environment.NewLine;
            emailBody += "Each photo can be downloaded 5 times." + Environment.NewLine;
            emailBody += "If you have trouble downloading, please contact us." + Environment.NewLine;
            emailBody += baseUrl + ResolveUrl("~/contact.aspx") + Environment.NewLine;
            emailBody += Environment.NewLine;
            emailBody += Environment.NewLine;
            emailBody += "Total Cost: £" + ((decimal)order.TotalCost).ToString("0.00") + Environment.NewLine;
            emailBody += "Payment recieved with thanks" + Environment.NewLine;
            emailBody += Environment.NewLine;
            emailBody += "Thank you for your order" + Environment.NewLine;
            emailBody += Environment.NewLine;
            emailBody += "Message sent though StunningSnaps website";

            string subject = "Thank you for your order at StunningSnaps (" + order.Id + ")";

            // Send email to admin
            Email.sendEmail(customer.Email, "stunningsnaps@wilk.tech", subject, emailBody);
        }
        protected void rptBasket_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            string idString = e.CommandArgument.ToString();

            int id = int.Parse(idString);

            DatabaseCO5027Entities db = new DatabaseCO5027Entities();
            var basketItem = db.Baskets.Single(b => b.Id == id);
            db.Baskets.Remove(basketItem);

            db.SaveChanges();
            SetupBasket();
        }
    }
}