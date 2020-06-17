using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cw13.Models
{
    public class WyrobCukierniczy
    {
        [Key]
        public int IdWyrobuCukierniczego { get; set; }
        [Required]
        [MaxLength(200)]
        public String Nazwa { get; set; }
        [Required]
        public float CenaZaSzt { get; set; }
        [Required]
        [MaxLength(40)]
        public String Typ { get; set; }
        public List<Zamowienia_WyrobCukierniczy> zamowienia_WyrobCukiernicze { get; set; }
    }
}
