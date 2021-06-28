using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Automated_Wedding_Application.Data;
using Automated_Wedding_Application.Models;
using Microsoft.AspNetCore.Identity;
using Automated_Wedding_Application.Areas.Identity.Data;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace Automated_Wedding_Application.Controllers
{
    public class ServicesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public ServicesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Services
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.FindByEmailAsync(User.Identity.Name);
            var planner = _context.Planner.Where(x => x.ApplicationUserId == user.Id).FirstOrDefault();
            var services = await _context.plannerservices.Where(x => x.PlannerModelPlannerId == planner.PlannerId).ToListAsync();
            List<ServicesModel> servicesmodel = new List<ServicesModel>();
            foreach(var service in services)
            {
                if (service.Serviceimage != null)
                {
                    string url = Convert.ToBase64String(service.Serviceimage, 0, service.Serviceimage.Length);
                    service.Serviceimageurl = "data:image/jpg;base64," + url;
                    servicesmodel.Add(service);
                }
                else
                {
                    servicesmodel.Add(service);
                }
            }
           
            return View(servicesmodel);
        }

        // GET: Services/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var servicesModel = await _context.plannerservices
                .FirstOrDefaultAsync(m => m.ServiceId == id);
            if (servicesModel == null)
            {
                return NotFound();
            }

            return View(servicesModel);
        }

        // GET: Services/Create
        public IActionResult Create()
        {
            ViewData["Services update"] = "NotPresent";
            return View();
        }

        // POST: Services/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ServiceId,ServiceName,Cost")] ServicesModel servicesModel, IFormFile imagefile)
        {
            var user = await _userManager.FindByEmailAsync(User.Identity.Name);
            PlannerModel planner = _context.Planner.Where(x => x.ApplicationUserId == user.Id).FirstOrDefault();
            planner.ServicesCost = planner.ServicesCost + servicesModel.Cost;
            //Default Rating
            if (imagefile != null)
            {
                using (var dataStream = new MemoryStream())
                {
                    await imagefile.CopyToAsync(dataStream);
                    servicesModel.Serviceimage = dataStream.ToArray();
                }
            }
            
            servicesModel.PlannerModelPlannerId = planner.PlannerId;
            var services = _context.plannerservices.Where(x => x.PlannerModelPlannerId == planner.PlannerId && x.ServiceName == servicesModel.ServiceName).FirstOrDefault();
            if(services != null)
            {
                ViewData["Services update"] = "AlreadyPresent";
                return View(servicesModel);
            }
            if (ModelState.IsValid)
            {
                ViewData["Services update"] = "NotPresent";
                _context.Add(servicesModel);
                
                _context.Update(planner);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(servicesModel);
        }

        // GET: Services/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var servicesModel = await _context.plannerservices.FindAsync(id);
            if (servicesModel.Serviceimage != null)
            {
                servicesModel.Serviceimageurl = Convert.ToBase64String(servicesModel.Serviceimage, 0, servicesModel.Serviceimage.Length);
            }
            if (servicesModel == null)
            {
                return NotFound();
            }
            return View(servicesModel);
        }

        // POST: Services/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ServiceId,ServiceName,Cost,Serviceimageurl")] ServicesModel servicesModel, IFormFile imagefile)
        {
            var user = await _userManager.FindByEmailAsync(User.Identity.Name);
            PlannerModel planner = _context.Planner.Where(x => x.ApplicationUserId == user.Id).FirstOrDefault();
            planner.ServicesCost = planner.ServicesCost + servicesModel.Cost;
            servicesModel.PlannerModelPlannerId = planner.PlannerId;
           
           PlannerServices s = (PlannerServices)Enum.Parse(typeof(PlannerServices), servicesModel.ServiceName);
            //servicesModel.ServiceName= 
            if (id != servicesModel.ServiceId)
            {
                return NotFound();
            }

            if (imagefile != null)
            {
                using (var dataStream = new MemoryStream())
                {
                    await imagefile.CopyToAsync(dataStream);
                    servicesModel.Serviceimage = dataStream.ToArray();
                }
            }
            if(servicesModel.Serviceimageurl!= null)
            {
               servicesModel.Serviceimage = Convert.FromBase64String(servicesModel.Serviceimageurl);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(servicesModel);
                    await _context.SaveChangesAsync();
                    _context.Update(planner);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServicesModelExists(servicesModel.ServiceId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(servicesModel);
        }

        // GET: Services/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var servicesModel = await _context.plannerservices
                .FirstOrDefaultAsync(m => m.ServiceId == id);
            if (servicesModel == null)
            {
                return NotFound();
            }

            return View(servicesModel);
        }

        // POST: Services/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var servicesModel = await _context.plannerservices.FindAsync(id);
            _context.plannerservices.Remove(servicesModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ServicesModelExists(int id)
        {
            return _context.plannerservices.Any(e => e.ServiceId == id);
        }
    }
}
