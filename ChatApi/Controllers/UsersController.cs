using AutoMapper;
using ChatApi.DTOs;
using ChatApi.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
  
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UsersController(IUserRepository userRepository, IMapper mapper)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }


        [HttpGet]

        public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
        {
            var users = await _userRepository.GetMembersAsync();

            var usersToReturn = _mapper.Map<IEnumerable<MemberDto>>(users);
           
            return Ok(usersToReturn);
        }

        [HttpGet("{username}")]
        public async Task<ActionResult<MemberDto>> GetUser(string username)
        {
          var user = await _userRepository.GetMemberAsync(username);

            return _mapper.Map<MemberDto>(user);
        }
    }
}
