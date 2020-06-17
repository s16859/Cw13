using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cw13.DTOs
{
    public class ZamowienieDTO
    {
        public String dataPrzyjecia { get; set; }
        public String uwagi { get; set; }
        public WyrobDTO[] wyroby { get; set; }
    }
}
