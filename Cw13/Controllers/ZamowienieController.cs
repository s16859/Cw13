using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cw13.DTOs;
using Cw13.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Cw13.Controllers
{
    [Route("api/order")]
    [ApiController]
    public class ZamowienieController : ControllerBase
    {

        private readonly CukierniaContext cukierniaContext;

        public ZamowienieController(CukierniaContext context)
        {
            this.cukierniaContext = context;
        }

        [HttpGet]
        public IActionResult GetZamowienia(GetDTO getDTO)
        {
            List<Zamowienie> zamowienia=null;
            List<ZamowienieDTO> zamowieniaDTO = new List<ZamowienieDTO>();

          
            if (getDTO.nazwisko != null)
            {
                Klient klient = cukierniaContext.Klienci.FirstOrDefault(w => w.Nazwisko == getDTO.nazwisko);
                if (klient!=null)
                zamowienia = cukierniaContext.Zamowienia.Where(z => z.IdKlient==klient.IdKlient).ToList();
                else
                zamowienia = cukierniaContext.Zamowienia.ToList();

            }
            else
            {
                zamowienia = cukierniaContext.Zamowienia.ToList();
            }

            if (zamowienia == null)
                return NotFound("Nie znaleziono zamowien");

            


            foreach(Zamowienie z in zamowienia)
            {
                ZamowienieDTO zamowienieDTO = new ZamowienieDTO();
                zamowienieDTO.dataPrzyjecia = z.DataPrzyjecia.ToString();
                zamowienieDTO.uwagi = z.Uwagi;

                List<WyrobDTO> wyrobyDTO = new List<WyrobDTO>();

                

                
                foreach(Zamowienia_WyrobCukierniczy z_wc in cukierniaContext.zamowienia_WyrobCukiernicze.Where(zwc => zwc.IdZamowienia == z.IdZamowienie).ToList())
                {
                    WyrobDTO wyrobDTO = new WyrobDTO();
                    WyrobCukierniczy wyrob = cukierniaContext.wyrobCukiernicze.FirstOrDefault(w => w.IdWyrobuCukierniczego == z_wc.IdWyrobuCukierniczego);
                    wyrobDTO.wyrob = wyrob.Nazwa;
                    wyrobyDTO.Add(wyrobDTO);
                }
                zamowienieDTO.wyroby = wyrobyDTO.ToArray();
                

                zamowieniaDTO.Add(zamowienieDTO);
            }

            

            return Ok(zamowieniaDTO);
        }




        [HttpPost("{id}")]
        public IActionResult NoweZamowienie(int id,ZamowienieDTO noweZamowienieDTO)
        {

            List<Zamowienia_WyrobCukierniczy> zamowienia_WyrobCukiernicze = new List<Zamowienia_WyrobCukierniczy>();
            List<WyrobCukierniczy> wyrobyWZamowieniu = new List<WyrobCukierniczy>();
            
            int newOrderId;
            
            Zamowienie ostatnie = cukierniaContext.Zamowienia.OrderByDescending(z => z.IdZamowienie).FirstOrDefault();
            if (ostatnie == null)
                newOrderId = 1;
            else
            newOrderId=ostatnie.IdZamowienie+1;
            
            


            foreach (WyrobDTO wyrobDTO in noweZamowienieDTO.wyroby)
            {
               WyrobCukierniczy wyrobCukierniczy = cukierniaContext.wyrobCukiernicze.FirstOrDefault(w => w.Nazwa == wyrobDTO.wyrob);

                if (wyrobCukierniczy == null)
                {
                    return NotFound("Nie ma takiego wyrobu: "+wyrobDTO.wyrob);
                }
                wyrobyWZamowieniu.Add(wyrobCukierniczy);
                Zamowienia_WyrobCukierniczy zamowienia_WyrobCukierniczy = new Zamowienia_WyrobCukierniczy();
                zamowienia_WyrobCukierniczy.IdWyrobuCukierniczego = wyrobCukierniczy.IdWyrobuCukierniczego;
                zamowienia_WyrobCukierniczy.IdZamowienia = newOrderId;
                zamowienia_WyrobCukierniczy.Ilosc = Int32.Parse(wyrobDTO.ilosc);
                zamowienia_WyrobCukierniczy.Uwagi = wyrobDTO.uwagi;
                zamowienia_WyrobCukiernicze.Add(zamowienia_WyrobCukierniczy);
            }


            Klient klient = cukierniaContext.Klienci.Find(id);
            if (klient == null)
                return NotFound("Nie znaleziono klienta o id: "+id);

            Pracownik pracownik = cukierniaContext.Pracownicy.Find(1);
            if (pracownik == null)
                return NotFound("Nie znaleziono pracwnika o id: " + 1);


            Zamowienie noweZamowienie = new Zamowienie();
            noweZamowienie.DataPrzyjecia=DateTime.Parse(noweZamowienieDTO.dataPrzyjecia);
            noweZamowienie.Uwagi = noweZamowienieDTO.uwagi;
            noweZamowienie.klient = klient;
            noweZamowienie.pracownik = pracownik;
            noweZamowienie.zamowienia_WyrobCukiernicze = zamowienia_WyrobCukiernicze;

            cukierniaContext.Add(noweZamowienie);
            cukierniaContext.SaveChanges();


            

            return Ok("Stworzono nowe zamowienie");
        }
    }
}
