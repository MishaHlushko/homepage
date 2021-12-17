using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Musical_Instrument_Store.Data.Models;

namespace Musical_Instrument_Store.ViewModels
{
    public class MusicalInstrumentViewModel
    {

        [HiddenInput(DisplayValue = false)]
        public int Id { set; get; }

        [Required(ErrorMessage = "Поле Назва є обов'язкове.")]
        [Display(Name = "Назва")]
        public string nameMI { set; get; }

        [Required(ErrorMessage = "Поле Опис є обов'язкове.")]
        [Display(Name = "Опис")]
        public string descMI { set; get; }

        [Required(ErrorMessage = "Кількість є обов'язковою.")]
        [Display(Name = "Кількість")]
        public int quantity { set; get; }

        [Required(ErrorMessage = "Поле Ціна є обов'язкове.")]
        [Display(Name = "Ціна")]
        public ushort price { set; get; }

        [Required(ErrorMessage = "Зображення є обов'язкове.")]
        [Display(Name = "Зображення")]
        public string img { set; get; }

        public bool IsDeleted { set; get; }
        public bool available { set; get; }

        [Required(ErrorMessage = "Оберіть категорію")]
        [Display(Name = "Категорія")]
        public int categoryId { set; get; }
        public IEnumerable<SelectListItem> categories { get; set; }

        public MusicalInstrumentViewModel() { }

        public MusicalInstrumentViewModel(MusicalInstrument musicalInstrument)
        {
            Id = musicalInstrument.Id;
            nameMI = musicalInstrument.nameMI;
            descMI = musicalInstrument.descMI;
            quantity = musicalInstrument.quantity;
            price = musicalInstrument.price;
            img = musicalInstrument.img;
            IsDeleted = musicalInstrument.IsDeleted;
            available = musicalInstrument.available;
            categoryId = musicalInstrument.MICategoryId;
        }

    }
}
