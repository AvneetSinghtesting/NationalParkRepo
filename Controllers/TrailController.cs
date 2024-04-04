using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using NationalPark2._0.Models;
using NationalPark2._0.Models.DTO;
using NationalPark2._0.Repository.IRepository;

namespace NationalPark2._0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrailController : ControllerBase
    {
        private readonly ITrailRepository _trail;
        private readonly IMapper _mapper;
        public TrailController(ITrailRepository trail,IMapper mapper)
        {
            _mapper = mapper;
            _trail = trail;
        }
        [Route("GetTrails")]
        [HttpGet]
        public IActionResult GetTrails()
        {
            var trailToDto = new List<TrailDTO>();
            var TrailFromDB = _trail.GetTrails();
            if (TrailFromDB == null)
                return BadRequest();
            foreach (var trail in TrailFromDB)
            {
                TrailDTO trailitem = _mapper.Map<TrailDTO>(trail);
                trailToDto.Add(trailitem);
            }
            return Ok(trailToDto);
        }
        [Route("CreateTrail")]
        [HttpPost]
        public IActionResult CreateTrail([FromBody]TrailDTO trailDTO)
        {
            if(trailDTO == null) return BadRequest();
            if(!ModelState.IsValid) return BadRequest();
            if(trailDTO.Id != 0 || _trail.TrailExist(trailDTO.Name)) return BadRequest();
            Trail trailToDB = _mapper.Map<TrailDTO, Trail>(trailDTO);
            if (_trail.CreateTrail(trailToDB)) return Ok(StatusCodes.Status200OK);
            return StatusCode(StatusCodes.Status400BadRequest);
        }
        [Route("UpdateTrail")]
        [HttpPatch]
        public IActionResult UpdateTrail([FromBody]TrailDTO trailDTO)
        {
            if (trailDTO == null) return BadRequest();
            if (!ModelState.IsValid) return BadRequest();
            if(!_trail.TrailExist(trailDTO.Id)) return BadRequest();
            Trail trailToDB = _mapper.Map<TrailDTO, Trail>(trailDTO);
            if (_trail.UpdateTrail(trailToDB)) return Ok(StatusCodes.Status200OK);
            return StatusCode(StatusCodes.Status400BadRequest);
        }
        [Route("DeleteTrail")]
        [HttpDelete]
        public IActionResult DeleteTrail(int TrailID)
        {
            if(TrailID== 0) return BadRequest();
            if(!_trail.TrailExist(TrailID)) return BadRequest();
            if(!_trail.DeleteTrail(_trail.GetTrailById(TrailID))) return BadRequest();
            return StatusCode(StatusCodes.Status200OK);
        }
        [Route("GetTrailsByNpId")]
        [HttpGet]
        public IActionResult GetTrailsByNpId(int NpId)
        {
            if (NpId == 0) return BadRequest();
            ICollection<Trail> TrailfrmDB=_trail.GetTrailsByNpId(NpId);
            if(TrailfrmDB != null)
            {
                var TrailToDto = new List<TrailDTO>();
                foreach(var item in TrailfrmDB)
                {
                    TrailDTO trailDTO=_mapper.Map<Trail, TrailDTO>(item);
                    TrailToDto.Add(trailDTO);
                }
                return Ok(TrailToDto);
            }
            return StatusCode(StatusCodes.Status400BadRequest);
        }
    }
}
