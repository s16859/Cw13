using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cw13.Models
{
    public class Zamowienie
    {
        [Key]
        public int IdZamowienie { get; set; }
        [Required]
        public DateTime DataPrzyjecia { get; set; }
        public DateTime DataRealizacji { get; set; }
        [MaxLength(300)]
        public String Uwagi { get; set; }
        public int IdKlient { get; set; }
        public Klient klient { get; set; }
        public int IdPracownik { get; set; }
        public Pracownik pracownik { get; set; }
        public List<Zamowienia_WyrobCukierniczy> zamowienia_WyrobCukiernicze { get; set; }

    }
}
