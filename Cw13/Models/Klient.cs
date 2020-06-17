using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cw13.Models
{
    public class Klient
    {
        [Key]
        public int IdKlient { get; set; }
        [MaxLength(50)]
        [Required]
        public String Imie { get; set; }
        [MaxLength(60)]
        [Required]
        public String Nazwisko { get; set; }
        public List<Zamowienie> zamowienia { get; set; }
    }
}
