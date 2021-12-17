using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Musical_Instrument_Store.Data.Interfaces;
using Musical_Instrument_Store.ViewModels;
using Musical_Instrument_Store.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Musical_Instrument_Store.Controllers
{
    public class MusicalInstrumentController : Controller
    {
        private readonly IMusicalInstrumentService _musicalInstrumentService;
        private readonly IMICategoryService _mICategoryService;

        public MusicalInstrumentController(IMusicalInstrumentService iMusicalInstrumentService, IMICategoryService iMICategoryService)
        {
            _musicalInstrumentService = iMusicalInstrumentService;
            _mICategoryService = iMICategoryService;
        }


        public async Task<IActionResult> Index(int? categoryId, string searchString = null)
        {
            if (categoryId != null && categoryId != 0)
            {
                return View(await _musicalInstrumentService.FilterAndSearchByCategoryAsync(categoryId, searchString));
            }
            
            else if (!string.IsNullOrEmpty(searchString))
            {
                return View(await _musicalInstrumentService.SearchMusicalInstrumentByStringAsync(searchString));
            }
            else
            {
              
                
                return View(await _musicalInstrumentService.GetMusicalInstrumentAsync());
            }
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View(await _musicalInstrumentService.GetMusicalInstrumentViewModelAsync());
        }

        [HttpPost]

        public async Task<IActionResult> Create(MusicalInstrumentViewModel model, IFormFile uploadedImage)
        {

            try
            {
                TempData["AlertMessage"] = "Товар успішно додано!";
                await _musicalInstrumentService.AddMusicalInstrumentAsync(model, uploadedImage);
                return RedirectToAction("Index", "MusicalInstrument");
            }
            catch
            {
                TempData["AlertMessage"] = "При додаванні товару виникла помилка";
                return RedirectToAction("Create", "MusicalInstrument");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int miId)
        {
            try
            {
                TempData["AlertMessage"] = "Товар успішно видалено!";
                await _musicalInstrumentService.DeleteMusicalInstrumentAsync(miId);
                return RedirectToAction("Index", "MusicalInstrument");
            }
            catch
            {
                return RedirectToAction("Index", "MusicalInstrument");
            }
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            return View(await _musicalInstrumentService.GetMusicalInstrumentViewModelAsync(id));
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(MusicalInstrumentViewModel model, IFormFile uploadedImage)
        {
            try
            {
                TempData["AlertMessage"] = "Товар успішно змінено!";
                await _musicalInstrumentService.EditAsync(model, uploadedImage);
                return RedirectToAction("Index", "MusicalInstrument");
            }
            catch
            {
                return RedirectToAction("Index", "MusicalInstrument");
            }
            
        }
        
    }

}
