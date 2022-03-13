using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RitechWeb.Models.Model
{
    [Table("Portfolyo")]
    public class Portfolyo
    {
        [Key]
        public int PortfolyoId { get; set; }
        [DisplayName("Portfolyo Başlık")]
        [Required,StringLength(100,ErrorMessage ="100 Karekter Olmalıdır.")]
        public string Baslik { get; set; }
        [DisplayName("Portfolyo Logo")]
        public string LogoURL { get; set; }
    }
}