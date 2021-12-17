using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Musical_Instrument_Store.Data.Models
{
    public class Order
    {
        [BindNever]
        public int Id { get; set; }

        [Display(Name ="Ім'я:")]
        [StringLength(25)]
        [Required(ErrorMessage ="Довжина імені не більше 25 символів")]
        public string nameClient { get; set; }

        [Display(Name = "Прізвище:")]
        [StringLength(35)]
        [Required(ErrorMessage = "Довжина прізвища не більше 35 символів")]
        public string surnameClient { get; set; }

        [Display(Name = "Адрес:")]
        [StringLength(50)]
        [Required(ErrorMessage = "Довжина адресу не більше 50 символів")]
        public string addressClient { get; set; }

        [Display(Name = "Номер телефону:")]
        [StringLength(12)]
        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage = "Довжина номеру телефону не більше 12 символів")]
        public string phoneClient { get; set; }

        [Display(Name = "Email:")]
        [DataType(DataType.EmailAddress)]
        [StringLength(50)]
        [Required(ErrorMessage = "Довжина не більше 50 символів")]
        public string emailClient { get; set; }

        [BindNever]
        [ScaffoldColumn(false)]
        public DateTime orderTime { get; set; }

        public int statusOrderId { set; get; }

        public string userId { get; set; }
        public virtual StatusOrder StatusOrder { set; get; }
        public List<OrderDetail> orderDetails { get; set; }
    }
}
