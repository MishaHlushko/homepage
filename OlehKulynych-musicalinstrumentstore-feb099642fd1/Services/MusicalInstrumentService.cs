using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Musical_Instrument_Store.Data.Interfaces;
using Musical_Instrument_Store.Data.Models;
using Musical_Instrument_Store.ViewModels;

namespace Musical_Instrument_Store.Data.Services
{
    public class MusicalInstrumentService : IMusicalInstrumentService
    {
        private readonly IMusicalInstrumentRepository _musicalInstrumentRepository;
        private readonly IMICategoryRepository _mICategoryRepository;
        private readonly IWebHostEnvironment _appEnvironment;
        public MusicalInstrumentService(IMusicalInstrumentRepository musicalInstrumentRepository, IWebHostEnvironment appEnvironment, IMICategoryRepository mICategoryRepository)
        {
            _mICategoryRepository = mICategoryRepository;
            _musicalInstrumentRepository = musicalInstrumentRepository;
            _appEnvironment = appEnvironment;
        }

       
        public async Task<MusicalInstrumentListViewModel> GetMusicalInstrumentAsync()
        {
            return new MusicalInstrumentListViewModel { ListMusicalInstruments = await _musicalInstrumentRepository.GetMusicalInstrument(), categories = (SelectList)await _musicalInstrumentRepository.GetCategoriesForDropdownForFilterAsync() }; /*_musicalInstrumentRepository.GetMusicalInstrument();*/
        }


        public async Task<MusicalInstrumentViewModel> GetMIbyIdAsync(int miId)
        {
            MusicalInstrument musicalInstrument = await _musicalInstrumentRepository.GetMIbyIdAsync(miId);
            if (musicalInstrument != null)
            {
                return new MusicalInstrumentViewModel(musicalInstrument);
            }

            return null;
        }
       
        public async Task AddMusicalInstrumentAsync(MusicalInstrumentViewModel musicalInstrumentViewModel, IFormFile uploadedImage)
        {

            if(uploadedImage != null)
            {
                
                string path = "/img/" + uploadedImage.FileName;
                
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await uploadedImage.CopyToAsync(fileStream);
                }
                var musicalInstrument = new MusicalInstrument()
                {
                    nameMI = musicalInstrumentViewModel.nameMI,
                    descMI = musicalInstrumentViewModel.descMI,
                    quantity = musicalInstrumentViewModel.quantity,
                    price = musicalInstrumentViewModel.price,
                    img = path,
                    available = musicalInstrumentViewModel.available,
                    IsDeleted = false,
                    MICategoryId = musicalInstrumentViewModel.categoryId

                };
                await _musicalInstrumentRepository.AddMusicalInstrumentAsync(musicalInstrument);
            }

            
        }

        public async Task DeleteMusicalInstrumentAsync(int id)
        {
            MusicalInstrument musicalInstrument = await _musicalInstrumentRepository.GetMIbyIdAsync(id);


            if (musicalInstrument != null)
            {
                await _musicalInstrumentRepository.DeleteMusicalInstrumentAsync(musicalInstrument);
            }

        }

        public async Task EditAsync(MusicalInstrumentViewModel model, IFormFile uploadedImage)
        {

            MusicalInstrument musicalInstrument = new MusicalInstrument
            {
                Id = model.Id,
                nameMI = model.nameMI,
                descMI = model.descMI,
                price = model.price,
                available = model.available,
                IsDeleted = model.IsDeleted,
                quantity = model.quantity,
                MICategoryId = model.categoryId
            };
            if (uploadedImage != null)
            {
                string path = _appEnvironment.WebRootPath + _musicalInstrumentRepository.GetMIbyIdAsync(model.Id).Result.img;
                if (File.Exists(path))
                {
                    File.Delete(path);
                }

                path = "/img/" + uploadedImage.FileName;

                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await uploadedImage.CopyToAsync(fileStream);
                }

                musicalInstrument.img = path;
                
            }
            else
            {
                
                musicalInstrument.img = _musicalInstrumentRepository.GetMIbyIdAsync(model.Id).Result.img;
            }
            await _musicalInstrumentRepository.EditAsync(musicalInstrument);
        }

        public async Task<MusicalInstrumentViewModel> GetMusicalInstrumentViewModelAsync()
        {
            var musicalInstrumentViewModel = new MusicalInstrumentViewModel
            {
                categories = await _musicalInstrumentRepository.GetCategoriesForDropdownAsync()
            };

            return musicalInstrumentViewModel;
        }

        public async Task<MusicalInstrumentViewModel> GetMusicalInstrumentViewModelAsync(int id)
        {
            var musicalInstrument = await _musicalInstrumentRepository.GetMIbyIdAsync(id);
            var musicalInstrumentViewModel = new MusicalInstrumentViewModel
            {
                Id = id,
                nameMI = musicalInstrument.nameMI,
                descMI = musicalInstrument.descMI,
                price = musicalInstrument.price,
                available = musicalInstrument.available,
                img = musicalInstrument.img,
                IsDeleted = musicalInstrument.IsDeleted,
                quantity = musicalInstrument.quantity,
                categoryId = musicalInstrument.MICategoryId,
                categories = await _musicalInstrumentRepository.GetCategoriesForDropdownAsync()
            };

            return musicalInstrumentViewModel;
        }

        public async Task<MusicalInstrumentListViewModel> SearchMusicalInstrumentByStringAsync(string searchString)
        {
            var musicalInstrumentListViewModel = new MusicalInstrumentListViewModel
            {
                ListMusicalInstruments = await _musicalInstrumentRepository.SearchMusicalInstrumentByStringAsync(searchString),
                categories = (SelectList)await _musicalInstrumentRepository.GetCategoriesForDropdownForFilterAsync()
            };


            return musicalInstrumentListViewModel;
        }

        public async Task<MusicalInstrumentListViewModel> FilterAndSearchByCategoryAsync(int? categoryId, string searchString)
        {

            MusicalInstrumentListViewModel musicalInstrumentListViewModel = new MusicalInstrumentListViewModel
            {
                ListMusicalInstruments = await _musicalInstrumentRepository.FilterAndSearchByCategoryAsync(categoryId, searchString),
                categories = (SelectList)await _musicalInstrumentRepository.GetCategoriesForDropdownForFilterAsync()
            };

            return musicalInstrumentListViewModel;
        }

    }
}
