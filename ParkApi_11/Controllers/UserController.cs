using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ParkApi_11.Models;
using ParkApi_11.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkApi_11.Controllers
{
    [Route("api/User")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        [HttpPost("register")]
        public IActionResult Register([FromBody]User user)
        {
            if (ModelState.IsValid)
            {
                var isUniqueUser = _userRepository.IsUniqueUser(user.UserName);
                if (!isUniqueUser) return BadRequest("UserName in use");
                var UserInfo = _userRepository.Register(user.UserName, user.Password);
                if (UserInfo == null) return BadRequest();
            }
            return Ok();
        }
        [HttpPost("Authenticate")]
        public IActionResult Authenticate([FromBody]UserVM userVM)
        {
            var User = _userRepository.Authenticate(userVM.UserName, userVM.Password);
            if (User == null)
                return BadRequest("wrong user/password");
            return Ok(User);
        }
            


    }
}
