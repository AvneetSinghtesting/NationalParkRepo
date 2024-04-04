using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using NationalPark2._0.Data;
using NationalPark2._0.Models;
using NationalPark2._0.Models.DTO;
using NationalPark2._0.Repository;
using NationalPark2._0.Repository.IRepository;
using System.Collections;

namespace NationalPark2._0.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class NationalParkController : ControllerBase
    {
        private readonly INationalParkRepository _nationalPark;
        private readonly IMapper _mapper;
        public NationalParkController(INationalParkRepository nationalPark,IMapper mapper)
        {
            _nationalPark = nationalPark;
            _mapper = mapper;
        }
        //[Route("GetNationalParks")]
        [HttpGet]
        public IActionResult GetNationalParks()
        {
            var nationalParkFrmDB=_nationalPark.GetAllNationalParks().ToList();
            var nationalParkDtoList = new List<NationalParkDTO>();
            if(nationalParkFrmDB != null)
            {
                foreach(var nationalPark in nationalParkFrmDB)
                {
                    nationalParkDtoList.Add(_mapper.Map<NationalPark,NationalParkDTO>(nationalPark));
                }
                return Ok(nationalParkDtoList);
            }
            return StatusCode(StatusCodes.Status400BadRequest);
        }
        //[Route("CreateNationalPark")]
        [HttpPost]
        public IActionResult CreateNationalPark([FromBody]NationalParkDTO nationalParkDTO )
        { 
            if(nationalParkDTO == null)
                return StatusCode(StatusCodes.Status400BadRequest);
            if(_nationalPark.nationalParkExists(nationalParkDTO.Name))
            {
                ModelState.AddModelError("", "National Park already exist.");
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            var nationalParktoDB=_mapper.Map<NationalParkDTO,NationalPark>(nationalParkDTO);
            if(!_nationalPark.CreateNationalPark(nationalParktoDB))
            {
                ModelState.AddModelError("", "Some internal server issue found while Creating NationalPark");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return StatusCode(StatusCodes.Status200OK);
        }
        //[Route("DeleteNationalPark")]
        [HttpDelete]
        public IActionResult DeleteNationalPark(int nationalParkId)
        {
            if(nationalParkId == 0)
                return StatusCode(StatusCodes.Status400BadRequest);
            if(!_nationalPark.nationalParkExists(nationalParkId))
            {
                ModelState.AddModelError("", $"There is no National Park available with the Id= {nationalParkId}");
                return StatusCode(StatusCodes.Status404NotFound);
            }
            if(!_nationalPark.DeleteNationalPark(_nationalPark.GetNationalPark(nationalParkId)))
                return BadRequest(ModelState);
            return StatusCode(StatusCodes.Status200OK);
        }
        //[Route("UpdateNationalPark")]
        [HttpPatch]
        public IActionResult UpdatedNationalPark(NationalParkDTO nationalParkDto) 
        {
            if(nationalParkDto == null)
                return BadRequest(ModelState);
            var nationalParkToDB=_mapper.Map<NationalParkDTO,NationalPark>(nationalParkDto);
            if(!_nationalPark.nationalParkExists(nationalParkToDB.Id))
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            if (!_nationalPark.UpdateNationalPark(nationalParkToDB))
                return BadRequest(ModelState);
            return Ok(StatusCodes.Status200OK);
        }
        //[Route("DeleteNationalParkDifferent")]
        [HttpDelete]
        public IActionResult DeleteNationalParkDifferent(int nationalParkId)
        {
            if(nationalParkId == 0||!_nationalPark.nationalParkExists(nationalParkId)) return BadRequest(ModelState);
            if(!_nationalPark.DeleteNationalPark(_nationalPark.GetNationalPark(nationalParkId))) return BadRequest(ModelState);
            return StatusCode(StatusCodes.Status200OK);
        }
    }
}
