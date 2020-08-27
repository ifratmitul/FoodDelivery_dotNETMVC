using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Spice.Data;
using Spice.Models;

namespace Spice.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CatagoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CatagoryController(ApplicationDbContext db)
        {
            _db = db;

        }

        //Get Action method.
        public async Task<IActionResult> Index()
        {
            return View(await _db.Catagory.ToListAsync());
        }

        public IActionResult Create()
        {
            return View();

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Catagory category)
        {
            if (ModelState.IsValid)
            {
                _db.Catagory.Add(category);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }

            return View(category);
        }
        //EDIT GET
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();

            }

            var category = await _db.Catagory.FindAsync(id);

            if(category == null)
            {
                return NotFound();
            }
            return View(category);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Catagory category)
        {
            if (ModelState.IsValid)
            {
                _db.Update(category);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }
            return View(category);
        }

        //Delete GET
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();

            }

            var category = await _db.Catagory.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }
            return View(category);

        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            

            var category = await _db.Catagory.FindAsync(id);
            if (category == null) return NotFound();
            
            _db.Catagory.Remove(category);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //Get Details

        public async Task<IActionResult> Details (int? id)
        {


            if (id == null)
            {
                return NotFound();

            }

            var category = await _db.Catagory.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }
            return View(category);

        }


    }
}
