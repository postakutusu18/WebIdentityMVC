using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace WebIdentityMVC.Models
{
    public class UserUpdateViewModel
    {
        [Display(Name = "Email :")]
        [Required(ErrorMessage = "Email alanı gereklidir")]
        [EmailAddress(ErrorMessage = "Lütfen geçerli bir email adresi giriniz")]
        public string Email { get; set; }

        [Display(Name = "Telefon :")]
        public string PhoneNumber { get; set; }


        public string PictureUrl { get; set; }

        public IFormFile Picture { get; set; }
        [Display(Name = "İsim :")]
        [Required(ErrorMessage = "Name alanı gereklidir")]
        public string Name { get; set; }
        [Display(Name = "Soyisim :")]
        [Required(ErrorMessage = "Name alanı gereklidir")]
        public string SurName { get; set; }
    }
}
