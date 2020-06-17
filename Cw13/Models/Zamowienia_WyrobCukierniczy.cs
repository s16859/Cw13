using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cw13.Models
{
    public class Zamowienia_WyrobCukierniczy
    {
        
        public int IdWyrobuCukierniczego { get; set; }
        public WyrobCukierniczy wyrobCukierniczy { get; set; }
        public int IdZamowienia { get; set; }
        public Zamowienie zamowienie { get; set; }
        [Required]
        public int Ilosc { get; set; }
        [MaxLength(300)]
        public String Uwagi { get; set; }
    }
}
