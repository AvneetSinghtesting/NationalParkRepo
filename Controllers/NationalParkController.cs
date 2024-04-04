using Microsoft.AspNetCore.Mvc;
using NationalPark_Front;
using NationalParkFrontEnd.Models;
using NationalParkFrontEnd.Repository;
using NationalParkFrontEnd.Repository.IRepository;

namespace NationalParkFrontEnd.Controllers
{
    public class NationalParkController : Controller
    {
        private readonly INationalParkRepository _nationalParkRepository;
        public NationalParkController(INationalParkRepository nationalParkRepository)
        {
            _nationalParkRepository = nationalParkRepository;
        }
        public async Task<IActionResult> Index()
        {
            var NPList = await _nationalParkRepository.GetAllAsync(SD.APIBaseUrl+SD.NationalParkAPI);
            return View(NPList);
        }
        public async Task<IActionResult> GetAll()
        {
            return Json(new { data = await _nationalParkRepository.GetAllAsync(SD.NationalParkAPI) });
        }
        [HttpGet]
        public async Task<IActionResult> Upsert(int Id)
        {
            NationalPark nationalPark=new NationalPark();
            if(Id == 0)
            {
                return View(nationalPark);
            }
            else
            {
                nationalPark= await _nationalParkRepository.GetAsync(SD.NationalParkAPI, Id);
                if(nationalPark == null)
                    return NotFound();
                return View(nationalPark);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Upsert(NationalPark nationalPark)
        {
            if(ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                if(files.Count>0)
                {
                    byte[] p1=null;
                    using(var fs1 = files[0].OpenReadStream())
                    {
                        using(var ms1=new MemoryStream())
                        {
                            fs1.CopyTo(ms1);
                            p1= ms1.ToArray();
                        }
                        nationalPark.Picture = p1;
                    }
                }
                else
                {
                    var objFrmBackEnd = await _nationalParkRepository.GetAsync(SD.NationalParkAPI, nationalPark.Id);
                    nationalPark.Picture= objFrmBackEnd.Picture;
                    if(nationalPark.Id==0)
                    {
                        await _nationalParkRepository.CreateAsync(SD.NationalParkAPI, nationalPark);
                    }
                    else
                    {
                        await _nationalParkRepository.UpdateAsync(SD.NationalParkAPI,nationalPark);
                    }
                }
            }
            return View(nameof(Index));
        }

    }
}
