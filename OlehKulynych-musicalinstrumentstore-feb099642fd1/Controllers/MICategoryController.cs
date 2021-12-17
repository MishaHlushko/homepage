using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Musical_Instrument_Store.Data.Interfaces;
using Musical_Instrument_Store.ViewModels;

namespace Musical_Instrument_Store.Controllers
{
    public class MICategoryController : Controller
    {
        private readonly IMICategoryService _mICategoryService;

        public MICategoryController(IMICategoryService mICategoryService)
        {
            _mICategoryService = mICategoryService;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _mICategoryService.GetMICategories());
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(string nameMI)
        { 
            if(ModelState.IsValid)
            {
                TempData["AlertMessage"] = "Категорію успішно додано!";
                await _mICategoryService.Create(nameMI);
                return RedirectToAction("Index", "MICategory");
            }
            else
            {
                return RedirectToAction("Index", "MICategory");
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            return View(await _mICategoryService.GetMICategoryByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(MICategoryViewModel mICategoryViewModel)
        {
            try
            {
                TempData["AlertMessage"] = "Категорію успішно змінено!";
                await _mICategoryService.UpdateCategoryAsync(mICategoryViewModel.Id, mICategoryViewModel.categoryName);
                return RedirectToAction("Index", "MICategory");
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "MICategory");
            }
        }


        public async Task<IActionResult> DeleteCategory(int id)
        {
            try
            {
                TempData["AlertMessage"] = "Категорію успішно видалено!";
                await _mICategoryService.DeleteCategory(id);
                return RedirectToAction("Index", "MICategory");
            }
            catch
            {
                return RedirectToAction("Index", "MICategory");
            }
        }
    }
}
