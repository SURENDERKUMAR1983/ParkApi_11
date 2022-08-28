using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ParkApi_11.Dtos;
using ParkApi_11.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkApi_11.Controllers
{
    [Route("api/NationalPark")]
    [ApiController]
   /* [Authorize]*/
    public class NationalParkController : ControllerBase
    {
        private readonly INationalParkRepository _nationalParkRepository;
        private readonly IMapper _mapper;
        public NationalParkController(INationalParkRepository nationalParkRepository, IMapper mapper)
        {
            _nationalParkRepository = nationalParkRepository;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetNationalParks() //for display
        {
            var nationalParkListDto = _nationalParkRepository.GetNationalParks().
                ToList().Select(_mapper.Map<NationalPark, NationalParkDto>);
            return Ok(nationalParkListDto);  //200
        }
        [HttpGet("{nationalParkId:int}", Name = "GetNationalPark")]   //for find
        public IActionResult GetNationalPark(int nationalParkId)
        {
            var nationalPark = _nationalParkRepository.GetNationalPark(nationalParkId);
            if (nationalPark == null)
                return NotFound();  //404
            var nationalparkDto = _mapper.Map<NationalParkDto>(nationalPark);
            return Ok(nationalparkDto); //200
        }
        [HttpPost]   // for Save
        public IActionResult CreateNationalPark([FromBody] NationalParkDto nationalParkDto)
        {
            if (nationalParkDto == null)
                return BadRequest(ModelState); //400
            if (_nationalParkRepository.NationalParkExists(nationalParkDto.Name))
            {
                ModelState.AddModelError(",", "National Park Already in DB");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var nationalPark = _mapper.Map<NationalParkDto, NationalPark>(nationalParkDto);
            if (!_nationalParkRepository.CreateNationalPark(nationalPark))
            {
                ModelState.AddModelError(",", $"Something went wrong while Save data{nationalPark.Name}");
                return StatusCode(StatusCodes.Status500InternalServerError, ModelState);

            }
            //return Ok();
            return CreatedAtRoute("GetNationalPark", new { nationalParkId = nationalPark.Id }, nationalPark);  //201
        }
        [HttpPut]    //for update
        public IActionResult UpdateNationalPark([FromBody]NationalParkDto nationalParkDto)
        {
            if (nationalParkDto == null) return BadRequest(ModelState);
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var nationalPark = _mapper.Map<NationalPark>(nationalParkDto);
            if (!_nationalParkRepository.UpdateNationalPark(nationalPark))
            {
                ModelState.AddModelError(",", $"Something went wrong while update data{nationalPark.Name}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            //return Ok(); //204
            return NoContent();            
        }
        [HttpDelete("{nationalParkId:int}")]
        public IActionResult DeleteNationalPark(int nationalParkId)
        {
            if(!_nationalParkRepository.NationalParkExists( nationalParkId))
            {
                NotFound();
            }
            var nationalPark = _nationalParkRepository.GetNationalPark(nationalParkId);
            if (nationalPark == null) return NotFound();
            if (!_nationalParkRepository.DeleteNationalPark(nationalPark))
            {
                ModelState.AddModelError(",", $"Something went wrong while Delete data{nationalPark.Name}");
                return StatusCode(StatusCodes.Status500InternalServerError, ModelState);
            }
            return Ok();

        }
    }    
   
}
