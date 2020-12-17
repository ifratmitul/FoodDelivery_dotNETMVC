using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Spice.Data;
using Spice.Models;
using Spice.Models.ViewModels;

namespace Spice.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SubCategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        public SubCategoryController(ApplicationDbContext db)
        {
            _db = db;

        }

        public async Task<IActionResult> Index()
        {
            var subcategory = await _db.SubCategory.Include(s => s.Category).ToListAsync();
            return View(subcategory);
        }

        //GET create

        public async Task<IActionResult> Create()
        {
            SubCategoryAndCategoryViewModel model = new SubCategoryAndCategoryViewModel()
            {
                CategoryList = await _db.Catagory.ToListAsync(),
                SubCategory = new Models.SubCategory(),
                subCategoryList = await _db.SubCategory.OrderBy(p => p.Name).Select(p => p.Name).Distinct().ToListAsync()


            };
            return View(model);
        
        }

        //post-create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create ( SubCategoryAndCategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                var doesSubcategoryExists = _db.SubCategory.Include(s => s.Category)
                                            .Where(s => s.Name == model.SubCategory.Name && s.Category.Id == model.SubCategory.CategoryId);
                if(doesSubcategoryExists.Count() > 0)
                {
                    //eror
                }
                else
                {
                    _db.SubCategory.Add(model.SubCategory);
                    await _db.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            
            }

            SubCategoryAndCategoryViewModel modelVM = new SubCategoryAndCategoryViewModel()
            {
                CategoryList = await _db.Catagory.ToListAsync(),
                SubCategory = model.SubCategory,
                subCategoryList = await _db.SubCategory.OrderBy(p => p.Name)
                                                        .Select(p => p.Name)
                                                        .ToListAsync()
            };
            return View(modelVM);
        }
    }
}
