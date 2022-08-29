using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using JobPortal.Web.Areas.JobView.ViewModels;
using JobPortal.Web.Data;
using JobPortal.Web.Models;
using Microsoft.AspNetCore.Authorization;

namespace JobPortal.Web.Areas.JobView.Controllers
{
    [Area("JobView")]
    

    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }



        //[HttpGet]
        //public async IActionResult Index(string SearchString)
        //{
        //    var products = from m in _context.JobDetails select m;

        //    if (!String.IsNullOrEmplty(SearchString))
        //    {
        //        products = products.Where(s => s.JobDiscription!.Contains(SearchString));
        //    }
        //    return View(await products.ToListAsync());
        //}



        public IActionResult Index()
        {
            // Populate the data for the drop-down select list
            List<SelectListItem> categories = new List<SelectListItem>();
            categories.Add(new SelectListItem { Selected = true, Value = "", Text = "-- select a job category --" });
            categories.AddRange(new SelectList(_context.JobCategories, "JobCategoryId", "JobCategoryName"));
            ViewData["JobCategoryId"] = categories.ToArray();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index([Bind("JobCategoryId")] ShowJobViewModel model)
        {
            // Retrieve the Jobs for the selected category
            var items = _context.JobDetails.Where(m => m.JobCategoryId == model.JobCategoryId);

            // Populate the data into the viewmodel object
            model.JobDetail = items.ToList();

            // Populate the data for the drop-down select list
            ViewData["JobCategoryId"] = new SelectList(_context.JobCategories, "JobCategoryId", "JobCategoryName");

            // Display the View
            return View("Index", model);
        }






    }
}
