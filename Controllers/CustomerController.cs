using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Automated_Wedding_Application.Areas.Identity.Data;
using Automated_Wedding_Application.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Automated_Wedding_Application.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;


using Microsoft.EntityFrameworkCore;
using Automated_Wedding_Application.Payment;

namespace Automated_Wedding_Application.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly Services.IEmailSender _emailSender;

        public CustomerController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, Services.IEmailSender emailSender, SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;

        }
        public async Task<IActionResult> Detail(string sortOrder,
            string searchString,
            string currentFilter,
            int? pageNumber , string? id ,string? status)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["CurrentFilter"] = searchString;
            ViewData["id"] = id;
            if (id != null)
            {
                ViewData["search"] = "services";
                if(status== "updated")
                {
                    ViewData["Status"] = "Added";
                }
                else
                {
                    ViewData["Status"] = "NotAdded";

                }
                var users = from u in _context.Users
                            select u;

                var plannerusers = users.Where(x => x.userstripeId != null && x.UserType == "Planner").Select(x=>x.Id);
                List<int> plannerids = new List<int>();
                foreach(var planners in plannerusers)
                {
                    int Plannerinfo = _context.Planner.Where(x => x.ApplicationUserId == planners).Select(x => x.PlannerId).FirstOrDefault();
                    plannerids.Add(Plannerinfo);

                }
                var plannerServices = from t in _context.plannerservices
                                      where plannerids.Contains(t.PlannerModelPlannerId) && t.ServiceName== id
                                      select t;

                List<PlannerModel>  plannerModels = new List<PlannerModel>();

                foreach (var service in plannerServices)
                {
                    if (service.Serviceimage != null)
                    {
                        string url = Convert.ToBase64String(service.Serviceimage, 0, service.Serviceimage.Length);
                        service.Serviceimageurl = "data:image/jpg;base64," + url;
                        PlannerModel plannerModels1 = _context.Planner.Where(x => x.PlannerId == service.PlannerModelPlannerId).FirstOrDefault();

                        plannerModels1.ServicesModel = service;
                        plannerModels.Add(plannerModels1);

                    }
                    else
                    {
                        PlannerModel plannerModels1 = _context.Planner.Where(x => x.PlannerId == service.PlannerModelPlannerId).FirstOrDefault();

                        plannerModels1.ServicesModel = service;
                        plannerModels.Add(plannerModels1);
                    }
                }
                return View(plannerModels);
            }
            else
            {
                ViewData["search"] = "Planner";

                ViewData["Status"] = "NotAdded";
                if (searchString != null)
                {
                    pageNumber = 1;
                }
                else
                {
                    searchString = currentFilter;
                }
                var Users = _context.Users.Where(x => x.UserType == "Planner" && x.userstripeId!=null).ToList();

                List<PlannerModel> plannerModels = new List<PlannerModel>();

                foreach (var user in Users)
                {
                    var planners = _context.Planner.Where(x => x.ApplicationUserId == user.Id).FirstOrDefault();

                    if (planners != null)
                    {
                        List<ServicesModel> services = _context.plannerservices.Where(x => x.PlannerModelPlannerId == planners.PlannerId).ToList();
                        if (services.Count != 0)
                        {
                            planners.services = services;

                        }
                        if (planners.Plannerimage != null)
                        {
                            string url = Convert.ToBase64String(planners.Plannerimage, 0, planners.Plannerimage.Length);
                            planners.Imageurl = "data:image/jpg;base64," + url;

                        }
                        plannerModels.Add(planners);


                    }

                }
                var plannermodel = from t in plannerModels
                                   select t;

                switch (sortOrder)
                {
                    case "location":
                        plannermodel = plannermodel.OrderBy(s => s.Location);
                        break;
                    case "rating":
                        plannermodel = plannermodel.OrderByDescending(s => s.ServicesCost);
                        break;
                    case "lowtohigh":
                        plannermodel = plannermodel.OrderBy(s => s.ServicesCost);
                        break;
                    case "hightolow":
                        plannermodel = plannermodel.OrderByDescending(s => s.ServicesCost);
                        break;
                    default:
                        plannermodel = plannermodel.OrderBy(s => s.PlannerName);
                        break;
                }


                if (!string.IsNullOrEmpty(searchString))
                {
                    plannermodel = plannermodel.Where(s => (s.Location.ToUpper().Contains(searchString.ToUpper()) || s.PlannerName.ToUpper().Contains(searchString.ToUpper())));
                }


                //int pageSize = 1;
                //return View(await PaginatedList<PlannerModel>.CreateAsync(plannermodel, pageNumber ?? 1, pageSize));
                return View(plannermodel.ToList());
            }
        }
       
        public ActionResult PlannerInfo(int Plannerid, string? status)
        {
            var planner = _context.Planner.Where(x => x.PlannerId == Plannerid).FirstOrDefault();

            var services = _context.plannerservices.Where(x => x.PlannerModelPlannerId == planner.PlannerId).ToList();

            
            if (planner.Plannerimage != null)
            {
                string url = Convert.ToBase64String(planner.Plannerimage, 0, planner.Plannerimage.Length);
                planner.Imageurl = "data:image/jpg;base64," + url;

            }
            List<ServicesModel> services1 = new List<ServicesModel>();
            foreach(var service in services)
            {
                if(service.Serviceimage!= null)
                {
                    string url = Convert.ToBase64String(service.Serviceimage, 0, service.Serviceimage.Length);
                    service.Serviceimageurl = "data:image/jpg;base64," + url;
                }
                services1.Add(service);
                
            }
            planner.services = services1;
            if (status == "updated")
            {
                ViewData["status"] = "added";
            }
            else
            {
                ViewData["status"] = "Notadded";

            }

            return View(planner);
        }

        public ActionResult PastOrders()
        {
            var userid = HttpContext.Session.GetString("UserId");
            List<CustomerCart> customerCarts = _context.CustomerCarts.Where(x => x.UserId == userid && x.Checkout == "Y" && x.PaymentStatus == "Delivered").ToList();
            List<PlannerModel> plannerModels = new List<PlannerModel>();
            foreach (var customercart in customerCarts)
            {
                PlannerModel plannerModel = _context.Planner.Where(x => x.PlannerId == Convert.ToInt32(customercart.PlannerId)).FirstOrDefault();
                plannerModel.ServicesModel = _context.plannerservices.Where(x => x.ServiceId == Convert.ToInt32(customercart.ServiceId)).FirstOrDefault();
                if (plannerModel.ServicesModel.Serviceimage != null)
                {
                    string url = Convert.ToBase64String(plannerModel.ServicesModel.Serviceimage, 0, plannerModel.ServicesModel.Serviceimage.Length);
                    plannerModel.ServicesModel.Serviceimageurl = "data:image/jpg;base64," + url;

                }
                plannerModel.customerCart = customercart;


                plannerModels.Add(plannerModel);

            }
            return View(plannerModels);
        }
        public ActionResult Bookings()
        {
            var userid = HttpContext.Session.GetString("UserId");
            List<CustomerCart> customerCarts = _context.CustomerCarts.Where(x => x.UserId == userid && x.Checkout == "Y" && x.PaymentStatus=="Success").ToList();
            List<PlannerModel> plannerModels = new List<PlannerModel>();
            foreach (var customercart in customerCarts)
            {
                PlannerModel plannerModel = _context.Planner.Where(x => x.PlannerId == Convert.ToInt32(customercart.PlannerId)).FirstOrDefault();
                plannerModel.ServicesModel = _context.plannerservices.Where(x => x.ServiceId == Convert.ToInt32(customercart.ServiceId)).FirstOrDefault();
                if (plannerModel.ServicesModel.Serviceimage != null)
                {
                    string url = Convert.ToBase64String(plannerModel.ServicesModel.Serviceimage, 0, plannerModel.ServicesModel.Serviceimage.Length);
                    plannerModel.ServicesModel.Serviceimageurl = "data:image/jpg;base64," + url;

                }
                plannerModel.customerCart = customercart;
                

                plannerModels.Add(plannerModel);

            }
            return View(plannerModels);
        }

        public ActionResult ServicesList(string id)
        {

            var plannerServices = _context.plannerservices.Where(x => x.ServiceName == id);

            List<PlannerModel> plannerModels = new List<PlannerModel>();

            foreach(var service in plannerServices)
            {
                if (service.Serviceimage != null)
                {
                    string url = Convert.ToBase64String(service.Serviceimage, 0, service.Serviceimage.Length);
                    service.Serviceimageurl = "data:image/jpg;base64," + url;
                    PlannerModel plannerModels1 = _context.Planner.Where(x => x.PlannerId == service.PlannerModelPlannerId).FirstOrDefault();

                    plannerModels1.ServicesModel = service;
                    plannerModels.Add(plannerModels1);

                }
                else
                {
                    PlannerModel plannerModels1 = _context.Planner.Where(x => x.PlannerId == service.PlannerModelPlannerId).FirstOrDefault();

                    plannerModels1.ServicesModel = service;
                     plannerModels.Add(plannerModels1);
                }
         }
         return PartialView("_Plannerservices",plannerModels);
        }
        [HttpPost]
        public async Task<IActionResult> Addtocart(PlannerModel cart)
        {



            string userid = HttpContext.Session.GetString("UserId");
            var checkcart = _context.CustomerCarts.Where(x => x.PlannerId == cart.PlannerId.ToString() && x.ServiceId == cart.ServiceId.ToString() && x.UserId == userid && x.Checkout == "N" && x.DeliveryDate == cart.DeliveryDate).FirstOrDefault();
            CustomerCart customerCart = new CustomerCart();

            if (checkcart == null)
            {

                double totalcost = (double)(cart.ServicesCost * cart.Quantity);

                customerCart.PlannerId =cart.PlannerId.ToString();
                customerCart.ServiceName = cart.ServicesModel.ServiceName;
                customerCart.UserId = HttpContext.Session.GetString("UserId");
                customerCart.ServiceId = cart.ServiceId.ToString();
                customerCart.ServiceCost = totalcost;
                customerCart.Quantity = cart.Quantity;
                customerCart.DeliveryDate = cart.DeliveryDate;
               
                customerCart.Checkout = "N";



                _context.Add(customerCart);
                await _context.SaveChangesAsync();

            }
            else
            {


                double totalcost = checkcart.ServiceCost + (double)(cart.ServicesCost * cart.Quantity);
                int updatequantity = checkcart.Quantity + cart.Quantity;
                checkcart.Quantity = updatequantity;
                checkcart.ServiceCost = totalcost;
                if (ModelState.IsValid)
                {
                    _context.Update(checkcart);
                    await _context.SaveChangesAsync();
                }



            }



            if (cart.flag == "Y")
            {
                return RedirectToAction("PlannerInfo", new { Plannerid = cart.PlannerId, status = "updated" });
            }
            else
            {


                return RedirectToAction("Detail", new { id = cart.ServicesModel.ServiceName, status = "updated" });

            }


        
    }

        public ActionResult GetCartDetails()
        {
            var userid = HttpContext.Session.GetString("UserId");
            List<CustomerCart> customerCarts = _context.CustomerCarts.Where(x => x.UserId == userid && x.Checkout == "N").ToList();
            List<PlannerModel> plannerModels = new List<PlannerModel>();
            double TotalCartcost=0;
            foreach(var customercart in customerCarts)
            {
                PlannerModel plannerModel = _context.Planner.Where(x => x.PlannerId == Convert.ToInt32(customercart.PlannerId)).FirstOrDefault();
                plannerModel.ServicesModel = _context.plannerservices.Where(x => x.ServiceId == Convert.ToInt32(customercart.ServiceId)).FirstOrDefault();
                if (plannerModel.ServicesModel.Serviceimage != null)
                {
                    string url = Convert.ToBase64String(plannerModel.ServicesModel.Serviceimage, 0, plannerModel.ServicesModel.Serviceimage.Length);
                    plannerModel.ServicesModel.Serviceimageurl = "data:image/jpg;base64," + url;

                }
               plannerModel.customerCart = customercart;
                TotalCartcost = TotalCartcost + customercart.ServiceCost;

                plannerModels.Add(plannerModel);

            }
            ViewData["Totalcost"] = TotalCartcost;
            return View(plannerModels);
        }

        public ActionResult Checkout(string? TotalCost)
        {
            if(ViewData["status"]== null)
            {
                ViewData["status"] = "initial";
            }
            ViewData["Totalcost"] = TotalCost;
            return View();
        }
        [Route("pay")]
        public async Task<dynamic> Pay(PayModel payModel)
        {
            var userid = HttpContext.Session.GetString("UserId");
            var user = await _userManager.FindByIdAsync(userid);
            var planners = _context.CustomerCarts.Where(x => x.UserId == userid && x.Checkout == "N").ToList();
            var plannerdetails = from t in _context.CustomerCarts
                                 where t.UserId == userid && t.Checkout == "N"
                                 group t by t.PlannerId into g
                                 select new
                                 {
                                     plannerid = g.Key,
                                     cost = g.Sum(t=>t.ServiceCost)

                                 };
            List<PayoutModel> payoutModels = new List<PayoutModel>();
            foreach(var plannerdetail in plannerdetails)
            {
                var applicationuserid = _context.Planner.Where(x => x.PlannerId == Convert.ToInt32(plannerdetail.plannerid)).Select(x => x.ApplicationUserId).FirstOrDefault();
                ApplicationUser users = await _userManager.FindByIdAsync(applicationuserid);

                var stripaccountno = users.userstripeId;


                PayoutModel payoutModel = new PayoutModel();
                payoutModel.plannerid = plannerdetail.plannerid;
                payoutModel.Stripaccountno = stripaccountno;
                payoutModel.Amount = (double)plannerdetail.cost;

                payoutModels.Add(payoutModel);
            }
            if (planners.Count != 0)
            {
                foreach(var planner in planners) { }
            }
            if (ModelState.IsValid)
            {
                var result = await ProcessPayment.PayAsync(payModel,payoutModels,user.Email);

                    if (result.status == "Success")
                    {
                    await _emailSender.SendEmailAsync(
                   user.Email,
                   "Payment Confirmation",
                   $" Hi {user.FirstName}," +
                   $"" +
                   $"Thanks for Booking services with us !!" +
                   $"" +
                   $"Your Transaction is Successfull with the amount of  {payModel.Amount}. ");

                    foreach (var plannerdetail in plannerdetails)
                    {
                        var userinfo = _context.Planner.Where(x => x.PlannerId == Convert.ToInt32(plannerdetail.plannerid)).FirstOrDefault();
                        var planneremail = await _userManager.FindByIdAsync(userinfo.ApplicationUserId);
                        await _emailSender.SendEmailAsync(
                   planneremail.Email,
                   "New Order",
                   $"Hi ," +
                   $"" +
                   $"New Orders are placed with you and Payment Transaction for that services is successfull." +
                   $" Please check your account for order details");

                    }
                    var plannerpaymentref = result.payoutModels;


                    var plannersinfo = from t in _context.CustomerCarts
                                      where t.UserId == userid && t.Checkout == "N"
                                      select t;
                        
                        foreach(var plannerinfo in plannersinfo)
                        {
                                plannerinfo.Checkout = "Y";
                                plannerinfo.PaymentStatus = "Success";
                        plannerinfo.Deliverystatus = "Ordered";
                                foreach(PayoutModel planneridref in plannerpaymentref)
                                    {
                                        if(planneridref.plannerid == plannerinfo.PlannerId)
                                        {
                                            plannerinfo.plannerpaymentid = planneridref.plannerpaymentid;
                                        }

                                    }
                                plannerinfo.Chargeid = result.chargeid;
                                _context.Update(plannerinfo);
                        }
                    _context.SaveChanges();
                  


                    ViewData["status"] = "Success";
                        return RedirectToAction("Bookings");
                    }
                else
                {
                    ViewData["status"] = "Error";
                    return RedirectToAction("Error", new { error= result.error });
                }
            }
            return RedirectToAction("Detail");
        }

       public async Task<dynamic> CancelOrder(int plannerid, int cartid)
        {
            var userid = HttpContext.Session.GetString("UserId");
            var user = await _userManager.FindByIdAsync(userid);
            CustomerCart cartvalues = _context.CustomerCarts.Where(x => x.CartId == cartid).FirstOrDefault();

            var applicationuserid = _context.Planner.Where(x => x.PlannerId == plannerid).Select(x => x.ApplicationUserId).FirstOrDefault();
            ApplicationUser planner = await _userManager.FindByIdAsync(applicationuserid);
            var result = await ProcessPayment.refundAsync(cartvalues, planner.userstripeId);
            if(result== "Success")
            {
                await _emailSender.SendEmailAsync(
                  user.Email,
                  "Refund Confirmation",
                  $"Your Order is cancelled and amount is refunded back to payment made card. for any queries please contact us ");

                await _emailSender.SendEmailAsync(
                  planner.Email,
                  "Order cancelled ",
                  $"An order got cancelled and amount is refunded to customer please login into your account to check details ");

                ViewData["status"] = "refund";
                _context.Remove(cartvalues);
                _context.SaveChanges();
                return RedirectToAction("Bookings");
            }
            else
            {
                ViewData["status"] = "Error";
                return RedirectToAction("Error", new { error = result });
            }

        }

        public IActionResult Contact() {
            return View();
        }
        public IActionResult EditDeleteCartItems(int cartid)
        {
            PlannerModel plannerModel = new PlannerModel();
            var cartdetails = _context.CustomerCarts.Where(x => x.CartId == cartid).FirstOrDefault();
            var plannerdetails = _context.Planner.Where(x => x.PlannerId == Convert.ToInt32(cartdetails.PlannerId)).FirstOrDefault();
            var servicedetails = _context.plannerservices.Where(x => x.ServiceId == Convert.ToInt32(cartdetails.ServiceId)).FirstOrDefault();
            plannerModel.PlannerName = plannerdetails.PlannerName;
            if (servicedetails.Serviceimage != null)
            {
                string url = Convert.ToBase64String(servicedetails.Serviceimage, 0, servicedetails.Serviceimage.Length);
                servicedetails.Serviceimageurl = "data:image/jpg;base64," + url;

            }
            plannerModel.ServicesModel = servicedetails;
            plannerModel.customerCart = cartdetails;
            return View(plannerModel);
        }

        public IActionResult UpdateCartDetails(int cartid,int quantity, DateTime deliverydate)
        {
            var cartdetails = _context.CustomerCarts.Where(x => x.CartId == cartid).FirstOrDefault();
            cartdetails.Quantity = quantity;
            cartdetails.DeliveryDate = deliverydate;
            _context.Update(cartdetails);
            _context.SaveChanges();
            return RedirectToAction("GetCartDetails");
        }
        public IActionResult RemoveCartDetails(int cartid)
        {
            var cartdetails = _context.CustomerCarts.Where(x => x.CartId == cartid).FirstOrDefault();
            _context.Remove(cartdetails);
            _context.SaveChanges();
            return RedirectToAction("GetCartDetails");
        }

        public IActionResult Error(string error)
        {
            ViewData["error"] = error;
            return View();
        }
    }
}
