using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Automated_Wedding_Application.Models;
using Automated_Wedding_Application.Data;
using Microsoft.AspNetCore.Identity;
using Automated_Wedding_Application.Areas.Identity.Data;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Text;
using Stripe;
using BlazorNavigationManagerExtensions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.WebUtilities;


namespace Automated_Wedding_Application.Controllers
{
    public class PlannerController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly Services.IEmailSender _emailSender;

        [Inject]
        public NavigationManager navMan { get; set; }
        public PlannerController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, Services.IEmailSender emailSender, SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;

        }
        public async Task<ActionResult> PastOrders()
        {
            var user = await _userManager.FindByEmailAsync(User.Identity.Name);
            PlannerModel planner = _context.Planner.Where(x => x.ApplicationUserId == user.Id).FirstOrDefault();
            List<PlannerModel> plannerModels = new List<PlannerModel>();
            var bookings = _context.CustomerCarts.Where(x => x.PlannerId == planner.PlannerId.ToString() && x.Checkout == "Y" && x.Deliverystatus == "Delivered").ToList();
            foreach (var customercart in bookings)
            {
                PlannerModel plannerModel = _context.Planner.Where(x => x.PlannerId == Convert.ToInt32(customercart.PlannerId)).FirstOrDefault();
                plannerModel.ServicesModel = _context.plannerservices.Where(x => x.ServiceId == Convert.ToInt32(customercart.ServiceId)).FirstOrDefault();
                if(plannerModel.ServicesModel!= null)
                {

              
                if (plannerModel.ServicesModel.Serviceimage != null)
                {
                    string url = Convert.ToBase64String(plannerModel.ServicesModel.Serviceimage, 0, plannerModel.ServicesModel.Serviceimage.Length);
                    plannerModel.ServicesModel.Serviceimageurl = "data:image/jpg;base64," + url;

                }
                }
                plannerModel.customerCart = customercart;


                plannerModels.Add(plannerModel);

            }
            return View(plannerModels);

        }
            // GET: PlannerController
            public async Task<ActionResult> Index(string? Status, int? Cartid)
        {
            if(Status!= null && Cartid != null)
            {
                var cartdetails = _context.CustomerCarts.Where(x => x.CartId == Cartid).FirstOrDefault();
                cartdetails.Deliverystatus = Status;
                ApplicationUser userinfo = await _userManager.FindByIdAsync(cartdetails.UserId);
                await _emailSender.SendEmailAsync(
                  userinfo.Email,
                  "Order Update",
                  $" Hi {userinfo.FirstName}  Your {((PlannerServices)Int32.Parse(cartdetails.ServiceName))} service order status is changed to {Status}");
                

                _context.Update(cartdetails);
                await _context.SaveChangesAsync();

                
            }
            var user = await _userManager.FindByEmailAsync(User.Identity.Name);
            ViewData["Userid"] = user.Id;
            PlannerModel planner = _context.Planner.Where(x => x.ApplicationUserId == user.Id).FirstOrDefault();
            List<PlannerModel> plannerModels = new List<PlannerModel>();
            if (planner == null)
            {
                ViewData["status"] = "PlannerNotUpdated";
            }
            else
            {
                ServicesModel servicesModel = _context.plannerservices.Where(x => x.PlannerModelPlannerId == planner.PlannerId).FirstOrDefault();
                if (servicesModel == null)
                {
                    ViewData["status"] = "ServicesNotUpdated";
                }
                else
                {
                    if(user.userstripeId== null)
                    {
                        ViewData["status"] = "paymentnotset";

                    }
                    else
                    {
                        ViewData["status"] = "Allupdated";
                        var bookings = _context.CustomerCarts.Where(x => x.PlannerId == planner.PlannerId.ToString() && x.Checkout == "Y" && x.Deliverystatus!= "Delivered").ToList();
                        foreach (var customercart in bookings)
                        {
                            PlannerModel plannerModel = _context.Planner.Where(x => x.PlannerId == Convert.ToInt32(customercart.PlannerId)).FirstOrDefault();
                            plannerModel.ServicesModel = _context.plannerservices.Where(x => x.ServiceId == Convert.ToInt32(customercart.ServiceId)).FirstOrDefault();
                            if (plannerModel.ServicesModel!= null)
                            {

                           
                            if (plannerModel.ServicesModel.Serviceimage != null)
                            {
                                string url = Convert.ToBase64String(plannerModel.ServicesModel.Serviceimage, 0, plannerModel.ServicesModel.Serviceimage.Length);
                                plannerModel.ServicesModel.Serviceimageurl = "data:image/jpg;base64," + url;

                            }
                            }
                            plannerModel.customerCart = customercart;


                            plannerModels.Add(plannerModel);

                        }
                    }
                   
                }
            }
            
            return View(plannerModels);
        }

        // GET: PlannerController/Details/5

        public async Task<IActionResult> Details(int? id)
        {
            var user = await _userManager.FindByEmailAsync(User.Identity.Name);


            var plannerModel = await _context.Planner
                .FirstOrDefaultAsync(m => m.ApplicationUserId == user.Id);
            if (plannerModel == null)
            {
                return NotFound();
            }

            return View(plannerModel);
        }

        // GET: PlannerController/Create
        public async Task<ActionResult> Profile()
        {
            var user = await _userManager.FindByEmailAsync(User.Identity.Name);
            ViewData["Userid"] = user.Id;
            PlannerModel planner = _context.Planner.Where(x => x.ApplicationUserId == user.Id).FirstOrDefault();
            if (planner == null)
            {
                ViewData["planner"] = "PlannerNotUpdated";

                return View();
            }

            ViewData["planner"] = "PlannerUpdated";

            var plannerModel = await _context.Planner
           .FirstOrDefaultAsync(m => m.ApplicationUserId == user.Id);
            if (plannerModel == null)
            {
                return NotFound();
            }
            if (planner.Plannerimage != null)
            {
                string url = Convert.ToBase64String(plannerModel.Plannerimage, 0, plannerModel.Plannerimage.Length);
                plannerModel.Imageurl = "data:image/jpg;base64," + url;
            }



            return View(plannerModel);
        }

        // POST: PlannerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Profile(PlannerModel plannerModel, IFormFile imagefile)
        {
            using (var dataStream = new MemoryStream())
            {
                await imagefile.CopyToAsync(dataStream);
                plannerModel.Plannerimage = dataStream.ToArray();
            }
            plannerModel.Rating = 4; //Default rating
            var user = await _userManager.FindByEmailAsync(User.Identity.Name);
            ViewData["Userid"] = user.Id;
            plannerModel.ApplicationUserId = user.Id;
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(plannerModel);
                    _context.SaveChanges();
                    ViewData["planner"] = "PlannerUpdated";
                    return RedirectToAction(nameof(Profile));
                }
                else
                {
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: PlannerController/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plannerModel = await _context.Planner.FindAsync(id);
            if(plannerModel.Plannerimage!= null) {
                plannerModel.Imageurl = Convert.ToBase64String(plannerModel.Plannerimage, 0, plannerModel.Plannerimage.Length);
            }
            

            if (plannerModel == null)
            {
                return NotFound();
            }
            return View(plannerModel);
        }

        // POST: PlannerModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PlannerId,PlannerName,About,Location,ApplicationUserId,Imageurl")] PlannerModel plannerModel, IFormFile file)
        {
            if (id != plannerModel.PlannerId)
            {
                return NotFound();
            }


            if (file != null)
            {
                using (var dataStream = new MemoryStream())
                {
                    await file.CopyToAsync(dataStream);
                    plannerModel.Plannerimage = dataStream.ToArray();
                }
            }
           if(plannerModel.Imageurl != null)
            {
                plannerModel.Plannerimage = Convert.FromBase64String(plannerModel.Imageurl);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(plannerModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlannerModelExists(plannerModel.PlannerId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Profile));
            }
            return View(plannerModel);
        }
        // GET: PlannerController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PlannerController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Contact()
        {
            return View();

        }
        private bool PlannerModelExists(int id)
        {
            return _context.Planner.Any(e => e.PlannerId == id);
        }
        public async Task<IActionResult> AddPayment()
        {
            ApplicationUser currentUser = await _userManager.FindByEmailAsync(User.Identity.Name);
            return View(currentUser);
        }
        public async Task<IActionResult> ConnecttoStrip()
        {
            var state = Guid.NewGuid();
            string baseUrl = "https://connect.stripe.com/express/oauth/authorize";
            string redirectUri = "https://localhost:44348/Planner/VerifyStripeConnect";

            string clientId = StripeConfiguration.ClientId;
            string businessType = "individual";

            HttpContext.Session.SetString("stripeState", state.ToString());

            var currentUser = await _userManager.FindByEmailAsync(User.Identity.Name);

           
            string stripeAuthUrl = $"{baseUrl}?" +
                $"redirect_uri={redirectUri}&" +
                $"client_id={clientId}&" +
                $"stripe_user[business_type]={businessType}&" +
                $"stripe_user[email]={currentUser.Email}&" +
                $"stripe_user[first_name]={currentUser.FirstName}&" +
                $"stripe_user[last_name]={currentUser.LastName}&" +
                $"state={state}";

           return Redirect(stripeAuthUrl);
            //navMan.NavigateTo(stripeAuthUrl, true);
            
        }
       
        public async Task<IActionResult> VerifyStripeConnect(string code, string state)
        {
            var expectedState = HttpContext.Session.GetString("stripeState");

            ApplicationUser currentUser = await _userManager.FindByEmailAsync(User.Identity.Name);


            //ApplicationUser user=  

            if (state == expectedState)
            {
                HttpContext.Session.Remove("stripeState");

                if (code!= null)
                {
                    var stripeId =  GetStripeIdFromAuthCode(code);
                    currentUser.userstripeId = stripeId;
                    _context.Update(currentUser);
                    _context.SaveChanges();
                    
                }
            }
            return View();
        }

        public string GetStripeIdFromAuthCode(string authCode)
        {
            var options = new OAuthTokenCreateOptions
            {
                GrantType = "authorization_code",
                Code = authCode,
            };

            var service = new OAuthTokenService();
            var response = service.Create(options);

            return response.StripeUserId;
        }
    }
}
